using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMotionScript : MonoBehaviour
{
    public Transform[] waypoints;
    Transform player;
    RaycastHit rayHit;
    Ray sight;
    NavMeshAgent agent;

    int currentWayPoint;
    public bool isPlayerDetected = false;
    float maxDistance = 150f;
    float lostDistance;

    public Animator foundTextAnim;
    public Animator lostTextAnim;


    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        lostDistance = maxDistance + 20f;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GotoNextPoint();
    }

    private void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!isPlayerDetected && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if (waypoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.SetDestination(waypoints[currentWayPoint].position);

        currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
    }

    private void FixedUpdate()
    {
        sight.origin = transform.position;
        sight.direction = transform.forward;

        if (Physics.Raycast(sight, out rayHit, maxDistance))
        {
            Debug.DrawLine(sight.origin, rayHit.point, Color.red);
            if (!isPlayerDetected && rayHit.collider.CompareTag("PlayerBody"))
            {
                isPlayerDetected = true;
                agent.autoBraking = true;
                agent.stoppingDistance = 70f;
                foundTextAnim.SetTrigger("FoundPlayer");
                agent.SetDestination(player.position);
            }
        }


        if (isPlayerDetected && !agent.pathPending)
        {
            //float playerDis = Vector3.Distance(player.position, transform.position);
            float playerDis = agent.remainingDistance;
            
            //Debug.Log("remainingDistance To player: " + agent.remainingDistance);
            //Debug.Log("playerDis To player: " + playerDis);

            if (playerDis <= agent.stoppingDistance)
                transform.LookAt(player);

            else if (playerDis < lostDistance)
                agent.speed = 30f;

            else // playerDis > lostDistance
            {
                lostTextAnim.SetTrigger("LostPlayer");
                agent.stoppingDistance = 5f;
                agent.speed = 25f;
                isPlayerDetected = false;
                agent.autoBraking = false;
                GotoNextPoint();
            }

            agent.SetDestination(player.position);
        }
    }

}

