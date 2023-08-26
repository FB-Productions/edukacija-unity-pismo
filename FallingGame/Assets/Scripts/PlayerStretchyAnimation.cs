using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStretchyAnimation : MonoBehaviour
{
    public int sequence = -1; // -1 = deactivated; 0 = activated
    public GameObject obj;
    Vector3 startScale;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startScale = obj.transform.localScale;
        startPos = obj.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (sequence == 0)
        {
            obj.transform.localScale += Vector3.up * 2 * Time.deltaTime;
            obj.transform.localPosition += Vector3.down * 0.5f * Time.deltaTime;
            if (obj.transform.localScale.y >= startScale.y+0.25f)
            {
                sequence = 1;
            }
        }
        else if (sequence == 1)
        {
            obj.transform.localScale -= Vector3.up * 0.5f * Time.deltaTime;
            obj.transform.localPosition -= Vector3.down * 0.1f * Time.deltaTime;
            if (obj.transform.localScale.y <= startScale.y)
            {
                obj.transform.localScale = startScale;
                obj.transform.localPosition = startPos;
                sequence = -1;
            }
        }
    }
}
