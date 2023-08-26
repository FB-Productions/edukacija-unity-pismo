using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disco : MonoBehaviour
{
    Light lit;
    float lessgo = 0f;
    float speed = 0.2f;
    int count = 0;

    float whabam = 0.3f;
    bool whabamUp = true;

    private void Start()
    {
        lit = GetComponent<Light>();
    }

    private void Update()
    {
        lessgo += speed * Time.deltaTime;
        if (whabamUp)
        {
            whabam += speed * Time.deltaTime;
        }
        else
        {
            whabam += speed * 4 * Time.deltaTime;
        }

        if (lessgo >= 1)
        {
            lessgo = 0;
            count++;
            if (count > 3)
            {
                count = 0;
                // Gotta go faster!
                if (speed < 0.5)
                {
                    speed *= 1.5f;
                    FindObjectOfType<GameManager>().GetComponent<AudioSource>().pitch *= 1.025f;
                }
            }
        }

        if (whabam >= 0.8)
        {
            whabamUp = false;
            whabam = 1;
        }
        else if (whabam <= 0.3f)
        {
            whabamUp = true;
            whabam = 0.3f;
        }

        lit.color = Color.HSVToRGB(lessgo, whabam, 1f);
    }
}
