using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 dir = Vector3.right;
    public float speed = 5f;
    public int distance = 5;
    Vector3 startPos;
    public bool flip;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if (!flip)
        {
            transform.localPosition += dir * speed * Time.deltaTime;
        }
        else
        {
            transform.localPosition -= dir * speed * Time.deltaTime;
        }

        if (transform.position.x > startPos.x + distance || transform.position.y > startPos.y + distance || transform.position.z > startPos.z + distance)
        {
            flip = true;
            transform.position = new Vector3(startPos.x + (distance * dir.x), startPos.y + (distance * dir.y), startPos.z + (distance * dir.z));
        }
        else if (transform.position.x < startPos.x || transform.position.y < startPos.y || transform.position.z < startPos.z)
        {
            flip = false;
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            other.transform.parent = null;
        }
    }
}
