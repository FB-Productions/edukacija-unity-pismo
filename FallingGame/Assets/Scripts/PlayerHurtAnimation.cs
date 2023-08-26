using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtAnimation : MonoBehaviour
{
    public int sequence = -1; // -1 = deactivated; 0 = activated
    public GameObject obj;
    Vector3 startRot;

    // Start is called before the first frame update
    void Start()
    {
        startRot = obj.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (sequence == 0)
        {
            obj.transform.localEulerAngles += Vector3.up * 90 * Time.deltaTime;
            obj.transform.localEulerAngles += Vector3.right * 90 * Time.deltaTime;
            if (obj.transform.localEulerAngles.y >= startRot.y + 15)
            {
                sequence = 1;
            }
        }
        else if (sequence == 1)
        {
            obj.transform.localEulerAngles -= Vector3.up * 15 * Time.deltaTime;
            obj.transform.localEulerAngles -= Vector3.right * 15 * Time.deltaTime;
            if (obj.transform.localEulerAngles.y <= startRot.y + 1)
            {
                obj.transform.localEulerAngles = startRot;
                sequence = -1;
            }
        }
    }
}
