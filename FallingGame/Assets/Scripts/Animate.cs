using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public Vector3 angle;
    /*public Vector3 angleLimit;
    public Vector3 scale;
    public Vector3 scaleLimit;*/
    public float speed;
    /*public bool play = true;
    public bool loop = true;*/

    private void Update()
    {
        /*if (play)
        {*/
            transform.eulerAngles += angle * speed * Time.deltaTime;
            /*transform.localScale += scale * speed * Time.deltaTime;

            if ()
        }*/
    }
}
