using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    public bool isPowerup;

    [Header("Normal")]
    public float scoreValue; // koliko vrijedi objekt kad ga se skupi

    [Header("Powerup")]
    public int powerupType = 0; // 0 = 8 seconds of Invincibility
                                // ...
    [Header("")]
    public GameObject debris;
    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();

        if (CompareTag("PickUp"))
        {
            gm.scoreMax++; // Count the maximum possible number of points
                           // We do this in awake so the UI gets the correct value in GM's Start()
        }
    }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isPowerup)
            {
                gm.score += scoreValue;
                gm.UpdateScoreText();
            }
            else
            {
                switch(powerupType)
                {
                    case 0:
                        {
                            gm.invincibility += 8f;
                        }
                        break;
                }
            }

            Instantiate(debris, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
