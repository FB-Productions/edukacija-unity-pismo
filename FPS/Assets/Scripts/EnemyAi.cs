using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy target & navmesh agent")]
    Transform player;
    NavMeshAgent agent;
    GameManager gm;
    public int scoreValue = 10;
    [Header("Enemy stats")]
    public float closeCombatDistance = 2f;
    public float damageAmount = 10f;
    public float health = 100f;
    public float attackRate = 2f;
    public float attackRateMax = 2f;
    [Header("Attack checks")]
    bool isAttacking = false;
    bool canAttack;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        isAttacking = false;
        if (!isAttacking)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if(distanceToPlayer > closeCombatDistance)
            {
                agent.SetDestination(player.position);
                //canAttack = false;
            }
            else
            {
                agent.ResetPath();
                isAttacking = true;
                canAttack = true;
            }
        }
        //else
        //{
            attackRate -= Time.deltaTime;
        //}

        if (canAttack && attackRate <= 0f)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        attackRate = attackRateMax;
        //canAttack = false;
    }

    public void ReceiveDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gm.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
