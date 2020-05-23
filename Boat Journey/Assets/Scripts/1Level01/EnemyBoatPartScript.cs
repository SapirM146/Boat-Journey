using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatPartScript : MonoBehaviour
{
    public GameObject MainPart;

    private void OnDestroy()
    {
        Destroy(MainPart);
    }

}
