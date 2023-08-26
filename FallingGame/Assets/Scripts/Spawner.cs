using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //public GameObject prefabGood;
    //public GameObject prefabBad;
    public float timer = 1f;
    public float timerReset;
    public int spawnCount;
    public GameObject[] prefab;

    private void Start()
    {
        timerReset = timer;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Spawn();

            timer = timerReset;

            spawnCount++;

            if (spawnCount == 13 && timerReset > 0.7f)
            {
                timerReset *= 0.9f;
                spawnCount = 0;
            }
        }
    }

    void Spawn()
    {
        float randomPosX = Random.Range(-7.5f,7.5f);

        /*int prefabNum = Random.Range(0, 2);
        GameObject prefab = null;

        switch(prefabNum)
        {
            case 0:
            {
                prefab = prefabGood;
            }
            break;

            case 1:
            {
                prefab = prefabBad;
            }
            break;
        }*/

        Instantiate(prefab[Random.Range(0, prefab.Length)], new Vector3(randomPosX, 20, 0), transform.rotation/*Quaternion.identity*/);
    }
}
