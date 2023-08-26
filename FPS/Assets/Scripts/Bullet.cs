using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public float speed = 20f;
    public float lifeTime = 2f; // Time before the bullet disappears
    public float damage = 10f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Pogoðen");
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            
            GunController gunController = FindObjectOfType<GunController>(); // nije preporuceno jer pretrazuje cijelu hijerarhiju (grozno za velike projekte), ali nema puno veze sada jer je igra mala i nebitna - bolje s public ili [SerializeField]
            //GunController gunController = GetComponentInParent<GunController>(); // ovo je bilo krivo jer bulleti nisu childovi guna

            if (gunController != null)
            {
                gunController.BulletHitEnemy(enemyAI);
            }
            else
            {
                Debug.Log("Gun controller");
            }

            Destroy(gameObject);
        }

        else
        {
            Debug.Log("Nije");
        }
    }
}
