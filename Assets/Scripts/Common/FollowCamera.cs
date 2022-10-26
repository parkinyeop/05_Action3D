using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    public float moveSpeed = 2f;
    bool isTargetAlive;

    Vector3 diePosition = Vector3.zero;
    Quaternion dieRotation = Quaternion.identity;

    private void Start()
    {
        Player player = GameManager.Inst.Player;
        if (target == null)
        {
            target = GameManager.Inst.Player.transform;
        }
        offset = transform.position - target.position;
        isTargetAlive = player.IsAlive;
        player.onDie += OnTargetDie;
        
    }

    private void LateUpdate()
    {
        if (isTargetAlive)
        {
            transform.position = Vector3.Slerp(transform.position, 
                target.position + offset, moveSpeed * Time.deltaTime);
        }
        else
        {
            float delta = moveSpeed/3f* Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position,diePosition, delta);
            transform.rotation = Quaternion.Slerp(transform.rotation, dieRotation, delta);
        }
    }

    public void OnTargetDie()
    {
        isTargetAlive = false;
        diePosition = target.position + Vector3.up * 10f;
        dieRotation = Quaternion.LookRotation(-target.up, -target.forward);
        //Vector3 dieCamPos = new Vector3(0, 10, 0);
        //transform.position = Vector3.Slerp(transform.position, 
        //    target.position + dieCamPos, moveSpeed * Time.deltaTime);

        //Quaternion dieCamRotPos = Quaternion.Euler(90, 0, 180);
        //transform.rotation = Quaternion.Slerp(transform.rotation, 
        //    target.rotation * dieCamRotPos, moveSpeed * Time.deltaTime);
    }
}
