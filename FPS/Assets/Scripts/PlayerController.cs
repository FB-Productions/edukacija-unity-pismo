using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] Camera aimCam;
    [SerializeField] Transform aimCamPos;
    [SerializeField] Transform gunPos;
    [SerializeField] float aimZoom;
    //float fovInit;
    //float fovADS;
    
    private void Start()
    {
        //fovInit = aimCam.fieldOfView;
        //fovADS = aimCam.fieldOfView - 45;
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Shoot");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //aimCam.fieldOfView = fovADS;
            aimCamPos.position += Vector3.forward * aimZoom;
            gunPos.position -= Vector3.forward * aimZoom;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            //aimCam.fieldOfView = fovInit;
            aimCamPos.position -= Vector3.forward * aimZoom;
            gunPos.position += Vector3.forward * aimZoom;
        }
    }

    void Shoot()
    {

    }
}
