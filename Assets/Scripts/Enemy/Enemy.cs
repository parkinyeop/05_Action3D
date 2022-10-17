using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   //필수적으로 필요한 컴포넌트가 있을 때 자동으로 넣어주는 속성
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public WayPoint waypoint;
    public float moveSpeed = 3f;

    Transform moveTarget;
    Vector3 lookDir;
    float moveSpeedPerSecond;

    float waitTime = 1f;
    float waitTimer;
    protected EnemyState state;

    Rigidbody rb;
    Animator animator;

    protected enum EnemyState
    {
        Wait = 0,
        Patrol
    }

    Action StateUpdate;

    protected Transform MoveTarget
    {
        get => moveTarget;
        set
        {
            moveTarget = value;
            lookDir = (moveTarget.position - transform.position).normalized;
        }
    }

    protected EnemyState State
    {
        get => state;
        set
        {
            state = value;
            switch (state)
            {
                case EnemyState.Wait:
                    waitTimer = waitTime;
                    animator.SetTrigger("Stop");
                    StateUpdate = Update_Wait;
                    break;

                case EnemyState.Patrol:
                    animator.SetTrigger("Move");
                    StateUpdate = Update_Patrol;
                    break;

                default:
                    break;
            }
        }
    }

    protected float WaitTimer
    {
        get => waitTimer;
        set
        {
            waitTimer = value;
            if(waitTime < 0)
            {
                State = EnemyState.Patrol;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (waypoint != null)
        {
            MoveTarget = waypoint.Current;
        }
        else
        {
            MoveTarget = transform;
        }

        moveSpeedPerSecond = moveSpeed * Time.fixedDeltaTime;

        state = EnemyState.Wait;
        animator.ResetTrigger("Stop");
    }

    private void FixedUpdate()
    {
        StateUpdate();
    }

    void Update_Patrol()
    {
        rb.MovePosition(transform.position + moveSpeedPerSecond * lookDir);
        rb.rotation = Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(lookDir), 0.2f);

        if ((transform.position - moveTarget.position).sqrMagnitude < 0.01f)
        {
            transform.position = moveTarget.position;
            MoveTarget = waypoint.MoveNext();
            State = EnemyState.Wait;
        }
    }

    void Update_Wait()
    {
        waitTime -= Time.fixedDeltaTime;
    }
}
