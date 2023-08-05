using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding : MonoBehaviour
{
    PlayerMovement pm;
    GameManager gm;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        gm.canHide = pm.isGrounded;
    }
}
