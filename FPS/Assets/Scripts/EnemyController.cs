using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyController : MonoBehaviour
{
    Transform player;
    
    private void Start()
    {
        player = FindObjectOfType<FirstPersonController>().transform;
    }

    private void Update()
    {
        if (/*Vector3.Distance(transform.position, player.transform.position) < 5f && */Physics.Raycast(transform.position, player.transform.position, 5f, 3))
        {
            Debug.Log("hit");
        }
    }
}
