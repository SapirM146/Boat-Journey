using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMotionScript : MonoBehaviour
{
    public Transform[] waypoints;
    Transform player, target;
    //Rigidbody rb;
    RaycastHit rayHit;
    Ray sight;
    NavMeshAgent agent;

    //public float speed = 25f;
    //float rotationSpeed = 5f;
    int currentWayPoint;
    public bool isPlayerDetected = false;
    float maxDistance = 150f;

    public Animator foundTextAnim;
    public Animator lostTextAnim;


    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
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
        //if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            GotoNextPoint();
        

        else if(isPlayerDetected) // Player detected
        {
            float playerDis = Vector3.Distance(player.position, transform.position);
            //float playerDis = agent.remainingDistance;
            agent.SetDestination(player.position);

            Debug.Log("To player: " + agent.remainingDistance);

            //if (playerDis < 40f)
            //    speed = 0f;

            if (playerDis < maxDistance)
                agent.speed = 30f;

            if (playerDis > maxDistance) // playerDis > maxDistance
            {
                lostTextAnim.SetTrigger("LostPlayer");
                agent.stoppingDistance = 5f;
                agent.speed = 25f;
                //target = waypoints[currentWayPoint];
                isPlayerDetected = false;
                agent.autoBraking = false;
                GotoNextPoint();
            }
        }
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
                agent.stoppingDistance = 40f;
                //agent.SetDestination(player.position);
                foundTextAnim.SetTrigger("FoundPlayer");
            }
        }   
    }

}

