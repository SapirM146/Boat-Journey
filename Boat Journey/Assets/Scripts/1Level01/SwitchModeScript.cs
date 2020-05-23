using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModeScript : MonoBehaviour
{
    public GameObject boat;
    public GameObject boatCamera;
    public GameObject fps;
    public GameObject fpsStartPosition;

    public static bool isBoatMode = true;

    // Update is called once per frame
    void Update()
    {
        // Boat Mode
        if (Input.GetKeyUp("1"))
        {
            boat.GetComponent<Rigidbody>().isKinematic = false;
            boat.GetComponent<BoatMotionScript>().enabled = true;
            boatCamera.SetActive(true);
            fps.SetActive(false);
            isBoatMode = true;
        }

        // FPS Mode
        if (Input.GetKeyUp("2"))
        {
            boat.GetComponent<Rigidbody>().isKinematic = true;
            boat.GetComponent<BoatMotionScript>().enabled = false;
            boatCamera.SetActive(false);
            fps.transform.position = fpsStartPosition.transform.position;
            fps.SetActive(true);
            isBoatMode = false;
        }
    }
}
