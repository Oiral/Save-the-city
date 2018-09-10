using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStationScript : MonoBehaviour {

    public GameObject fireTruckPrefab;

    public Transform spawnPoint;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(fireTruckPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
