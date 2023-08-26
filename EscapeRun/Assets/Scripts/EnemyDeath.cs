using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    AudioSource aS;
    public AudioClip hurtSnd1; // ImpactMeat01
    public AudioClip hurtSnd2; // ImpactMeat02
    bool ready;

    private void Start()
    {
        aS = GetComponent<AudioSource>();

        if (Random.Range(0, 2) == 0)
        {
            aS.clip = hurtSnd1;
        }
        else
        {
            aS.clip = hurtSnd2;
        }

        aS.pitch = Random.Range(0.8f, 1.2f);

        aS.Play();
        
        ready = true;
    }

    private void Update()
    {
        if (!aS.isPlaying && ready)
        {
            Destroy(gameObject);
        }
    }
}
