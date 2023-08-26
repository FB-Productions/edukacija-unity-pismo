using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    [Header("True = Good prefab | False = Bad prefab")]
    public bool isPositive = true;
    GameManager gm; // povezivanje s bilokojom skriptom (ne mora biti GameManager, samo je to naziv trenutne skripte)

    private void Start()
    {
        gm = FindObjectOfType<GameManager>(); // isto kao i gore
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isPositive)
            {
                gm.score++;
                Destroy(gameObject);
                other.GetComponent<PlayerStretchyAnimation>().sequence = 0; // Panda plays its stretchy animation
                MakeChain();
            }
            else
            {
                gm.life--;
                Destroy(gameObject);
                other.GetComponent<PlayerHurtAnimation>().sequence = 0; // Panda plays its hurt animation
            }
        }
        
        if (other.gameObject.tag == "Floor")
        {
            if (isPositive && gm.score > 0)
            {
                gm.score--;
            }

            Destroy(gameObject);
        }
    }

    void MakeChain()
    {
        // Keeps a history of up to 3 recently collected positive items.
        // The goal is to collect 3 of the same item in a row, i.e. chain them together, to get a bonus.
        // If, at any point of forming the 3 item chain, a different item is collected, the history (chain) resets itself.

        switch(gm.chainHistory.Length)
        {
            case 0:
                {
                    gm.chainHistory[0] = gameObject.name;
                }
                break;
            case 1:
                {
                    if (gm.chainHistory[0] == gameObject.name)
                    {
                        gm.chainHistory[1] = gameObject.name;
                    }
                    else
                    {
                        Array.Clear(gm.chainHistory, 0, gm.chainHistory.Length);
                        gm.chainHistory[0] = gameObject.name;
                    }
                }
                break;
            case 2:
                {
                    if (gm.chainHistory[1] == gameObject.name)
                    {
                        gm.chainHistory[2] = gameObject.name;
                    }
                    else
                    {
                        Array.Clear(gm.chainHistory, 0, gm.chainHistory.Length);
                        gm.chainHistory[0] = gameObject.name;
                    }
                }
                break;
            case 3:
                {
                    if (gm.chainHistory[2] == gameObject.name)
                    {
                        // Chain completed!
                        Debug.Log("Chain!");
                    }
                    else
                    {
                        Array.Clear(gm.chainHistory, 0, gm.chainHistory.Length);
                        gm.chainHistory[0] = gameObject.name;
                    }
                }
                break;
            default:
                {
                    Array.Clear(gm.chainHistory, 0, gm.chainHistory.Length);
                    gm.chainHistory[0] = gameObject.name;
                }
                break;
        }
    }
}
