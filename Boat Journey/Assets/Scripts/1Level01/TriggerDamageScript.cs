using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamageScript : MonoBehaviour
{
    private BoatHPScript playerBoat;

    private void Start()
    {
        playerBoat = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatHPScript>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
            playerBoat.damageBoat();
    }
}
