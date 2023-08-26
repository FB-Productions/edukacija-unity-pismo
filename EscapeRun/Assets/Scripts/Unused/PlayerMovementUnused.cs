using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovementUnused : MonoBehaviour
{
    Rigidbody rb;
    Vector3 dir;
    public float speed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dir = Vector3.zero;

        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
        {
            dir = Vector3.ClampMagnitude(dir, 1);
        }

        transform.localPosition += dir * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, dir.z * speed);
        //rb.AddForce(dir * speed);
    }
}
