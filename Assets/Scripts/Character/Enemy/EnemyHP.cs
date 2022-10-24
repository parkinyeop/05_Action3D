using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    Transform fill;

    private void Awake()
    {
        fill = transform.GetChild(1);

        IHealth target = GetComponentInParent<IHealth>();
        target.onHealthChange += Refresh;
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
        //transform.rotation = Camera.main.transform.rotation;
    }

    private void Refresh(float ratio)
    {
      fill.localScale = new Vector3(ratio, 1,1);
    }
}
