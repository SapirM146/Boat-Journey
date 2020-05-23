using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMotionScript : MonoBehaviour
{
    public float turnSpeed = 1500f;
    public float accelerateSpeed = 2000f;
    bool motorSoundPlaying = false;

    AudioSource[] audioSource;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponents<AudioSource>();
    }

    void FixedUpdate()
    {
        float turnDirection = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.AddTorque(0, turnDirection * turnSpeed * Time.deltaTime, 0);
        rb.AddForce(transform.forward * v * accelerateSpeed* Time.deltaTime);

        if (!motorSoundPlaying && v > 0)
        {
            audioSource[0].Play();
            motorSoundPlaying = true;
        }

        else if(v <= 0)
        {
            audioSource[0].Stop();
            motorSoundPlaying = false;
        }
    }
}
