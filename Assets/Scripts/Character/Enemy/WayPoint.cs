using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    Transform[] children;

    int index = 0;
    public Transform Current => children[index];
    private void Awake()
    {
        children = GetComponentsInChildren<Transform>();
        children = new Transform[transform.childCount];
        for(int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i);
        }
    }

    public Transform GetCurrentWaypoint()
    {
        return children[index];
    }

    public Transform MoveNext()
    {
        index++;
        index %= children.Length;   //index 반복을 위해 %연산 사용

        return children[index];
    }
}
