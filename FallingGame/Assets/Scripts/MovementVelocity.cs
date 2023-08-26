using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVelocity : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10f;
    public Vector3 dir;
    public GameObject model;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        //dir.y = Input.GetAxis("Vertical");

        // proper normalization (without converting lower magnitudes to 1)
        if (dir.magnitude > 1)
        {
            dir = Vector3.ClampMagnitude(dir, 1);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = dir * speed;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rb.velocity.x);
    }
}
