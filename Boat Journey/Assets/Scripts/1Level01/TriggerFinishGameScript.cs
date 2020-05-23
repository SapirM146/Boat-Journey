using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFinishGameScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            GameManagerScript.gameWin();
        }
    }
}
