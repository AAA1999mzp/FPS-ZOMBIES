using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ENEMY : MonoBehaviour
{
    public enum WanderType { Random, Waypoint };

    public WanderType wanderType = WanderType.Random;
    public Transform player;
    public float wanderSpeed = 4f;
    public float chaseSpeed = 7f;
    public float fov = 120f;
    public float viewDistance = 10f;
    public float wanderRadius = 7f;
    public float timeBetweenAttacks;
    public float health = 100f;
    public float Damage = 20f;
    public float loseRadius = 10f;
    public float loseTimer = 10f;
    public Transform[] waypoints;

    public bool isAware = false;
    public bool isDetecting = false;
    public bool isAttacking = false;

    private float distanceToTarget;
    private Vector3 wanderPoint;
    private NavMeshAgent agent;
    private new Renderer renderer;
    private int waypointIndex = 0;
    private Animator animator;

    public AudioSource Screaming;
    public AudioSource Searching;
    public AudioSource Attacking;

    public EnemyHealth enemyHealth;
    public void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        animator = GetComponentInChildren<Animator>();
        wanderPoint = RandomWanderPoint();
    }
    public void Update()
    {
        if (isAware && !enemyHealth.isDead)
        {
            Screaming.Play();
            agent.SetDestination(player.position);
            animator.SetBool("Aware", true);
            renderer.material.color = Color.red;
            agent.speed = chaseSpeed;
            if (!isDetecting && !enemyHealth.isDead)
            {
                loseTimer += Time.deltaTime;
                if (loseTimer >= loseRadius)
                {
                    isAware = false;
                }
            }
            if (isDetecting && !enemyHealth.isDead)
            {
                agent.SetDestination(player.transform.position);
                transform.LookAt(player);
                distanceToTarget = Vector3.Distance(transform.position, player.position);

                if (!isAttacking)
                {
                    if (distanceToTarget < 3f)
                    {
                        if (!enemyHealth.isDead)
                        {
                            Attacking.Play();
                            animator.SetBool("Attack", true);
                            player.GetComponent<PlayerHealth>().PlayerDamage(Damage);
                            isAttacking = true;
                            Invoke(nameof(ResetAttack), timeBetweenAttacks);
                        }
                        else
                        {
                            Attacking.Stop();
                            enemyHealth.isDead = true;
                            isAware = false;
                        }
                    }
                    else
                    {
                        Attacking.Stop();
                        animator.SetBool("Attack", false);
                        isAttacking = false;
                        isDetecting = false;
                    }
                }
            }
        }
        else if (!isAware && !enemyHealth.isDead)
        {
            Searching.Play();
            SearchForPlayer();
            Wander();
            animator.SetBool("Aware", false);
            renderer.material.color = Color.blue;
            agent.speed = wanderSpeed;
        }
    }

    public void SearchForPlayer()
    {
        if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.position)) < fov / 2f)
        {
            if (Vector3.Distance(player.position, transform.position) < viewDistance)
            {
                RaycastHit hit;
                if (Physics.Linecast(transform.position, player.position, out hit, -1))
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        OnAware();
                    }
                    else
                    {
                        isDetecting = false;
                    }
                }
                else
                {
                    isDetecting = false;
                }
            }
            else
            {
                isDetecting = false;
            }
        }
        else
        {
            isDetecting = false;
        }
    }

    public void OnAware()
    {
        isAware = true;
        isDetecting = true;
        loseTimer = 0;
    }

    public void Wander()
    {
        if (wanderType == WanderType.Random)
        {
            if (Vector3.Distance(transform.position, wanderPoint) < 2f)
            {
                wanderPoint = RandomWanderPoint();
            }
            else
            {
                agent.SetDestination(wanderPoint);
            }
        }
        else
        {
            //Waypoint wandering
            if (waypoints.Length >= 2)
            {
                if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2f)
                {
                    if (waypointIndex == waypoints.Length - 1)
                    {
                        waypointIndex = 0;
                    }
                    else
                    {
                        waypointIndex++;
                    }
                }
                else
                {
                    agent.SetDestination(waypoints[waypointIndex].position);
                }
            }
            else
            {
                Debug.LogWarning("Please assign more than 1 waypoint to the AI: " + gameObject.name);
            }
        }
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
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
