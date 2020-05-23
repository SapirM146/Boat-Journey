using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatShottingScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletStartPos;

    // Update is called once per frame
    void Update()
    {
        if(SwitchModeScript.isBoatMode && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Instantiate(bullet, bulletStartPos.position, Quaternion.identity);
            b.GetComponent<BulletScript>().origin = this.gameObject;
        }
    }
}
