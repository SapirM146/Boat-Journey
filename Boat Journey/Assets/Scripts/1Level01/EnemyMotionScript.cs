using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMotionScript : MonoBehaviour
{
    public Transform[] waypoints;
    Transform player, target;
    Vector3 moveDirection;
    Rigidbody rb;
    RaycastHit rayHit;
    Ray sight;

    public float speed = 25f;
    float rotationSpeed = 5f;
    int currentWayPoint;
    public bool isPlayerDetected = false;
    float maxDistance = 150f;

    public Animator foundTextAnim;
    public Animator lostTextAnim;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = waypoints[currentWayPoint];
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        moveDirection = target.position - transform.position;

        // Rotation animation
        Quaternion rotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        sight.origin = transform.position;
        sight.direction = transform.forward;

        if (Physics.Raycast(sight, out rayHit, maxDistance))
        {
            Debug.DrawLine(sight.origin, rayHit.point, Color.red);
            if (!isPlayerDetected && rayHit.collider.CompareTag("PlayerBody"))
            {
                isPlayerDetected = true;
                target = player;
                foundTextAnim.SetTrigger("FoundPlayer");
            }
        }


        if (!isPlayerDetected)
        {
            if (moveDirection.magnitude < 1)
            {
                currentWayPoint = ++currentWayPoint % waypoints.Length;
                target = waypoints[currentWayPoint];
            }
        }

        else // Player detected
        {
            float playerDis = Vector3.Distance(player.position, transform.position);

            if (playerDis < 40f)
                speed = 0f;

            else if (playerDis < maxDistance)
                speed = 30f;

            else // playerDis > maxDistance
            {
                lostTextAnim.SetTrigger("LostPlayer");
                speed = 25f;
                target = waypoints[currentWayPoint];
                isPlayerDetected = false;
            }
        }

        rb.velocity = moveDirection.normalized * speed;
    }

}

