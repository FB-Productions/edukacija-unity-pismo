using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    /*public*/ GameObject target; // To je player, odnosno mi
    public float speed; // brzina kretanja neprijatelja
    public int damage;
    /*public*/ GameManager gm;
    public GameObject debris;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player"); // najjednostavniji nacin za ovo napraviti, manje intenzivno od Find ali jos uvijek koristi samo ako moras
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.LookAt(target.transform); /* * Time.deltaTime*/ // gledaj prema playeru
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime); // idi prema playeru
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //other.CompareTag("Player")
        {
            gm.LoseLife(damage, 0);
            gm.UpdateLifeText();

            Instantiate(debris, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
