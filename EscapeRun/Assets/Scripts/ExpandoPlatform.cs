using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExpandoPlatform : MonoBehaviour
{
    public float speed = 5f;
    public int sizeChange = 4;
    Vector3 initSize;
    public bool isExpanding = true;

    private void Start()
    {
        initSize = transform.localScale;
    }

    private void Update()
    {
        if (isExpanding)
        {
            transform.localScale += Vector3.right * speed * Time.deltaTime;
            transform.localScale += Vector3.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.right * speed * Time.deltaTime;
            transform.localScale -= Vector3.forward * speed * Time.deltaTime;
        }

        if (transform.localScale.x > initSize.x + sizeChange || transform.localScale.z > initSize.z + sizeChange)
        {
            transform.localScale = new Vector3(initSize.x + sizeChange, transform.localScale.y, initSize.z + sizeChange);
            isExpanding = false;
        }
        else if (transform.localScale.x < initSize.x || transform.localScale.z < initSize.z)
        {
            transform.localScale = initSize;
            isExpanding = true;
        }
    }
}
