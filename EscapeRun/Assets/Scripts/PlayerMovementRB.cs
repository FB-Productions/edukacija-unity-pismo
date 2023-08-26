using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRB : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*Vector3 fw = transform.forward;
        Vector3 dir = (fw).normalized;
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, fw * speed);*/

        float horMove = Input.GetAxis("Horizontal");
        float verMove = Input.GetAxis("Vertical");

        Vector3 forwardMove = transform.forward * verMove;
        Vector3 sideMove = transform.right * horMove;
        Vector3 totalMove = (forwardMove + sideMove).normalized * speed;

        rb.velocity = new Vector3(totalMove.x, rb.velocity.y, totalMove.z);
    }
}
