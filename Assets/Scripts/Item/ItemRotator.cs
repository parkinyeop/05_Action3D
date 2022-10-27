using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public float rotateSpeed;
    public float minHeight;
    public float maxHeight;

    float timeElapsed;
    float halfDiff;
    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        newPosition.y = minHeight;
        transform.position = newPosition;

        timeElapsed = 0;
        halfDiff = 0.5f * (maxHeight - minHeight);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        newPosition.y = minHeight + (1 - Mathf.Cos(timeElapsed))*halfDiff;
        transform.position = newPosition;

        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }
}
