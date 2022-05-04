using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;

    public float health;
    public float Damage = 20f;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float fov = 120f;
    public float viewRange = 20f;
    public float walkSpeed = 4f;
    public float chaseSpeed = 7f;
    public bool isAware = false;

    public float distanceToTarget;
    public float timeBetweenAttacks;
    public bool isAttacking;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public EnemyHealth enemyHealth;

    public void Start()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    public void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Chasing();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Attacking();
        }
    }
    public void Patrolling()
    {
        if (!walkPointSet)
        {
            animator.SetBool("Standing", true);
            SearchWalkPoint();
        }
        else if (walkPointSet)
        {
            animator.SetBool("Walking", true);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            animator.SetBool("Walking", false);
        }
    }
    public void SearchWalkPoint()
    {
        float RandomZ = Random.Range(-walkPointRange, walkPointRange);
        float RandomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + RandomX, transform.position.y, transform.position.z + RandomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }
    public void Chasing()
    {
        if (isAware && !enemyHealth.isDead)
        {
            agent.SetDestination(player.position);
            animator.SetBool("Running", true);
            agent.speed = chaseSpeed;
        }
        else if (!isAware && !enemyHealth.isDead)
        {
            SearchPlayer();
            Patrolling();
            animator.SetBool("Running", false);
            agent.speed = walkSpeed;
        }
    }
    public void SearchPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.position)) < fov / 2f)
        {
            if (Vector3.Distance(transform.position, player.position) < viewRange)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                }
            }
        }
    }
    public void OnAware()
    {
        isAware = true;
    }
    public void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        distanceToTarget = Vector3.Distance(transform.position, player.position);

        if (!isAttacking)
        {
            if (distanceToTarget < 2f)
            {
                if (!enemyHealth.isDead)
                {
                    animator.SetBool("Attacking", true);
                    player.GetComponent<PlayerHealth>().PlayerDamage(Damage);
                    isAttacking = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }
                else
                {
                    enemyHealth.isDead = true;
                }
            }
            else
            {
                animator.SetBool("Attacking", false);
                isAttacking = false;
            }
        }
    }
    public void ResetAttack()
    {
        isAttacking = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
