using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class WolfNewMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform playert;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float attackSpeed;

    Animator m_Animator; 

    float timer = 0;

    float timerattack = 0;

    Transform player;

    bool run = true;


    //Patroling
    Vector3 walkPoint;
    bool walkPointSet=false;
    public float walkPointRange ;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject target;
    

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange=false, playerInAttackRange=false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Friendly")
        {
            player = other.gameObject.GetComponent<Transform>();
        }
    }

    //private void OnTriggerEnter();

    private void Awake()
    {
       m_Animator = GetComponent<Animator>();
       agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        if (player != null)
        {
            playerInSightRange = true;
            playerInAttackRange = false;
            if (System.Math.Abs(2) > System.Math.Abs(transform.position.x - player.transform.position.x) && 2 > System.Math.Abs(transform.position.z - player.transform.position.z)) playerInAttackRange = true;
            else playerInAttackRange = false;
        }
        //Debug.Log(run);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        //Debug.Log("Patroling");
        if (!walkPointSet)
        {
            timer += Time.deltaTime;
           
            if (timer >= 2)
            {
                timer = 0;
                SearchWalkPoint();
            }
        }

        if (walkPointSet)
        {
            //Debug.Log("Should Run");
            run = true;
            m_Animator.SetBool("IsRunning", run);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 2f && walkPointSet)
        {
            //Debug.Log("Reached Point");
            walkPointSet = false;
            run = false;
            m_Animator.SetBool("IsRunning", run);
        }
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            //Debug.Log("WalkPointIsTrue");
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (System.Math.Abs(player.position.x - transform.position.x) > 1.5f && System.Math.Abs(player.position.z - transform.position.z) > 1.5f)
        {
            agent.SetDestination(player.position);
            m_Animator.SetBool("IsRunning", true);
        }
        else
        {
            m_Animator.SetBool("IsRunning", false);
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        //agent.SetDestination(transform.position);

        transform.LookAt(player);
        timerattack += Time.deltaTime;
        if (timerattack>=attackSpeed)
        {
            //Debug.Log("Attack");
            m_Animator.SetTrigger("IsAttacking");
            timerattack = 0f;
            //player.PlayerTakeDamage(100);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }*/
}
