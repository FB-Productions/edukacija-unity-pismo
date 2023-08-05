using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeking : MonoBehaviour
{
    public CharacterController hider;
    public GameManager gm;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider == hider)
        {
            gm.Winner("Seeker"); // nasli smo ga
        }
    }
}
