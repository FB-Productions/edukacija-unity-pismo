using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs = new GameObject[3];
    public Transform[] transforms = new Transform[3];
    public int[] nums = new int[2];

    float timer;

    private void Start()
    {
        //Instantiate(prefabs[nums[0]], transforms[nums[1]]);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject id = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transforms[Random.Range(0, transforms.Length)].position, Quaternion.Euler(0,0,0));
            id.transform.position += Vector3.up * 2;
            timer = 2f;
        }
    }
}
