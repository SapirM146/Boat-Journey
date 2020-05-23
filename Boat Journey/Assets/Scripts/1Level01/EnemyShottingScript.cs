using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShottingScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletStartPos;
    EnemyMotionScript motion;

    readonly float shootTimeConstant = 5f;
    float shootTimer;
    public bool shootFlag = false;


    private void Start()
    {
        motion = GetComponent<EnemyMotionScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shootFlag)
        {
            if (shootTimer > 0f)
                shootTimer -= Time.deltaTime;

            else
            {
                shootFlag = false;
            }
        }

        if (motion.isPlayerDetected && !shootFlag)
        {
            shootTimer = shootTimeConstant;
            GameObject b = Instantiate(bullet, bulletStartPos.position, Quaternion.identity);
            b.GetComponent<BulletScript>().origin = this.gameObject;
            shootFlag = true;
        }
    }

}
