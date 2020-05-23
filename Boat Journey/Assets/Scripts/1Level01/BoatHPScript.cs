using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHPScript : MonoBehaviour
{
    float damagedFromLandtimer = 5f;
    float boatHP = 1f;
    bool damagedFromLandFlag = false;

    AudioSource[] audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (damagedFromLandFlag)
        {
            if (damagedFromLandtimer > 0f)
                damagedFromLandtimer -= Time.deltaTime;

            else
            {
                damagedFromLandtimer = 5f;
                damagedFromLandFlag = false;
            }   
        }
    }

    public void damageBoat()
    {
        if (!damagedFromLandFlag && boatHP > 0)
        {
            damagedFromLandFlag = true;
            audioSource[1].Play();
            boatHP -= 0.10f;
        }
    }

    public void damageBoatByEnemy()
    {
        if (boatHP > 0)
        {
            audioSource[2].Play();
            boatHP -= 0.15f;
        }
    }

    public float getBoatHP()
    {
        return boatHP;
    }

}
