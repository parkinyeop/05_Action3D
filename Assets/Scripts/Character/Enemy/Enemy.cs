using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Rigidbody))]   //필수적으로 필요한 컴포넌트가 있을 때 자동으로 넣어주는 속성
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IHealth, IBattle
{
    [Header("---------Patrol Data")]
    public WayPoint waypoint;
    public float moveSpeed = 3f;
    Transform wayPointTarget;

    [Header("---------Chasing Data")]
    public float sightRange = 10f;
    public float sightHalfAngle = 50;
    Transform chaseTarget;

    [Header("---------Battle Data")]
    public float attackPower = 10f;
    public float defencePower = 3f;
    public float hp = 100f;
    public float maxHp = 100f;

    public GameObject[] dropItemPrefabs;

    Vector3 lookDir;
    //float moveSpeedPerSecond;

    float waitTime = 1f;
    float waitTimer;
    protected EnemyState state = EnemyState.Patrol;

    Rigidbody rb;
    Animator animator;
    NavMeshAgent agent;
    SphereCollider bodyCollider;
    ParticleSystem dieEffect;

    protected enum EnemyState
    {
        Wait = 0,
        Patrol,
        Chase,
        Dead
    }

    Action stateUpdate;

    protected Transform WayPointTarget
    {
        get => wayPointTarget;
        set
        {
            wayPointTarget = value;
            //lookDir = (moveTarget.position - transform.position).normalized;
        }
    }

    protected EnemyState State
    {
        get => state;
        set
        {
            if (state != value)
            {
                state = value;
                switch (state)
                {
                    case EnemyState.Wait:
                        agent.isStopped = true;
                        waitTimer = waitTime;
                        animator.SetTrigger("Stop");
                        stateUpdate = Update_Wait;
                        break;

                    case EnemyState.Patrol:
                        agent.isStopped = false;
                        agent.SetDestination(wayPointTarget.position);
                        animator.SetTrigger("Move");
                        stateUpdate = Update_Patrol;
                        break;

                    case EnemyState.Chase:
                        agent.isStopped = false;
                        animator.SetTrigger("Move");
                        stateUpdate = Update_Chase;
                        break;

                    case EnemyState.Dead:
                        animator.SetTrigger("Die");
                        agent.isStopped = true;         //길찾기 정지
                        stateUpdate = Update_Dead;      //fixedUpdate 에서 실행될 델리게이트 변경


                        StartCoroutine(DieRoutine());
                        break;

                    default:
                        break;
                }
            }
        }
    }

    protected float WaitTimer
    {
        get => waitTimer;
        set
        {
            waitTimer = value;
            if (waypoint != null && waitTimer < 0)
            {
                State = EnemyState.Patrol;
            }
        }
    }

    public float HP
    {
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;

                if (State != EnemyState.Dead && hp < 0)
                {
                    Die();
                }

                hp = Mathf.Clamp(hp, 0.0f, maxHp);

                onHealthChange?.Invoke(hp / maxHp);
            }
        }
    }

    public Action<float> onHealthChange { get; set; }
    public Action onDie { get; set; }
    public float MaxHP => maxHp;
    public float AttackPower => attackPower;
    public float DefencePower => defencePower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        dieEffect = GetComponentInChildren<ParticleSystem>();
        bodyCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        agent.speed = moveSpeed;
        //moveSpeedPerSecond = moveSpeed * Time.fixedDeltaTime;

        if (waypoint != null)
        {
            WayPointTarget = waypoint.Current;
        }
        else
        {
            WayPointTarget = transform;
        }

        State = EnemyState.Wait;
        animator.ResetTrigger("Stop");

        onHealthChange += Test_HP_Change;
        onDie += Test_Die;
    }

    private void FixedUpdate()
    {
        if (State != EnemyState.Dead && SearchPlayer())
        {
            State = EnemyState.Chase;
        }

        stateUpdate();
    }

    void Update_Patrol()
    {
        //rb.MovePosition(transform.position + moveSpeedPerSecond * lookDir);
        //rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(lookDir), 0.2f);

        //도착 확인 
        //if ((transform.position - moveTarget.position).sqrMagnitude < 0.01f)
        //{
        //    //transform.position = moveTarget.position;
        //    MoveTarget = waypoint.MoveNext();
        //    State = EnemyState.Wait;
        //}

        //agnet.pathPending : 경로 계산이 진행중인지 확인. true면 아직 경로 계산 중
        //agent.remainingDistance : 도작지점까지 남아 있는 거리
        //agent.stoppingDistance : 도착지점으로 인정되는 거리
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            WayPointTarget = waypoint.MoveNext();
            State = EnemyState.Wait;
        }
    }

    void Update_Chase()
    {
        if (chaseTarget != null)
        {
            agent.SetDestination(chaseTarget.position);
        }
        else
        {
            State = EnemyState.Wait;
        }
    }

    void Update_Wait()
    {
        WaitTimer -= Time.fixedDeltaTime;
    }

    void Update_Dead()
    {
    }

    bool SearchPlayer()
    {
        bool result = false;
        chaseTarget = null;

        // 레이어 마스크를 통해 오브젝트를 감지하는 물리 구체
        Collider[] collider =
            Physics.OverlapSphere(transform.position, sightRange,
            LayerMask.GetMask("Player"));

        if (collider.Length > 0)
        {
            Vector3 playerPos = collider[0].transform.position;
            Vector3 toPlayerDir = playerPos - transform.position;
            float angle = Vector3.Angle(transform.forward, toPlayerDir);
            if (sightHalfAngle > angle)
            {
                Ray ray = new(transform.position + transform.up * 0.5f, toPlayerDir);
                if (Physics.Raycast(ray, out RaycastHit hit, sightRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        chaseTarget = collider[0].transform;
                        result = true;
                    }
                }
            }
        }
        return result;
    }


    public void Attack(IBattle target)
    {
        target?.Defence(AttackPower);
    }

    public void Defence(float damage)
    {
        if (State != EnemyState.Dead)
        {
            HP -= (damage - DefencePower);
            animator.SetTrigger("Hit");
        }
    }
    public void Die()
    {
        State = EnemyState.Dead;
        onDie?.Invoke();
        MakeDpopItem();

        //Destroy(this.gameObject);
    }

    void MakeDpopItem()
    {
        float percentage = UnityEngine.Random.Range(0, 1.0f);
        int index;

        if (percentage < 0.6f)      //60%
        {
            index = 0;
        }
        else if (percentage < 0.9f) //30%
        {
            index = 1;
        }
        else
        {
            index = 2;
        }
        Instantiate(dropItemPrefabs[index], transform.position, transform.rotation);
    }

    IEnumerator DieRoutine()
    {
        dieEffect.Play();
        dieEffect.transform.parent = null;
        EnemyHP enemyHP = GetComponentInChildren<EnemyHP>();
        //enemyHP.gameObject.SetActive(false);
        Destroy(enemyHP.gameObject, 1f);

        yield return new WaitForSeconds(1.5f);
        // 죽은 상태에서 처리 할 것들
        agent.enabled = false;
        bodyCollider.enabled = false;
        rb.isKinematic = false;
        rb.drag = 10f;

        yield return new WaitForSeconds(2f);
        Destroy(dieEffect.gameObject);
        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.up, sightRange);

        if (SearchPlayer())
        {
            Handles.color = Color.red;
        }

        Vector3 forward = transform.forward * sightRange;
        Quaternion q1 = Quaternion.AngleAxis(-sightHalfAngle, transform.up);
        Quaternion q2 = Quaternion.AngleAxis(sightHalfAngle, transform.up);

        Handles.DrawLine(transform.position, transform.position + q1 * forward);
        Handles.DrawLine(transform.position, transform.position + q2 * forward);

        Handles.DrawWireArc(transform.position, transform.up, q1 * forward, sightHalfAngle * 2, sightRange, 5.0f);
#endif
    }
    public void Test()
    {
        SearchPlayer();
    }
    void Test_HP_Change(float ratio)
    {
        //Debug.Log($"{gameObject.name}의 HP가 {HP}로 변경되었습니다");
    }

    void Test_Die()
    {
        Debug.Log($"{gameObject.name}가 죽었습니다");
    }
}
