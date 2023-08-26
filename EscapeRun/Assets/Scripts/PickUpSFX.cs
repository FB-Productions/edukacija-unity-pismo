using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PickUpSFX : MonoBehaviour
{
    AudioSource aS;
    bool ready;

    private void Start()
    {
        aS = GetComponent<AudioSource>();
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
