using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockEffect : MonoBehaviour
{
    IHealth targetHealth;

    public void SetLockOnTarget(Transform newParent)
    {
        if (targetHealth != null)
        {
            targetHealth.onDie -= ReleaseTarget;
        }

        if (newParent != null)
        {
            targetHealth = newParent.gameObject.GetComponent<IHealth>();
            targetHealth.onDie += ReleaseTarget;
        }

        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
        this.gameObject.SetActive(newParent != null);
    }

    void ReleaseTarget()
    {
        SetLockOnTarget(null);
    }
}
