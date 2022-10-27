using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemRotator : MonoBehaviour
{
    public float rotateSpeed;
    public float minHeight;
    public float maxHeight;
    public float speed;
    float height;
    Vector3 rotationY;
    // Start is called before the first frame update
    void Start()
    {
        float yRotateValue = rotateSpeed * Time.deltaTime;
        rotationY = new Vector3(0, yRotateValue, 0);
    }

    // Update is called once per frame
    void Update()
    {
        height += Time.deltaTime * speed;
        float yPos = transform.position.y + Mathf.Sin(height) * Time.deltaTime;
        transform.Rotate(rotationY);
        transform.position = new Vector3(0, yPos, 0);
    }
}
