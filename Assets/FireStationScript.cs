using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireStationScript : MonoBehaviour {

    public GameObject fireTruckPrefab;

    public Transform spawnPoint;

    public int spareTrucks = 3;

    public void CallFireTruck(GameObject buildingOnFire)
    {
        if (spareTrucks > 0)
        {
            spareTrucks -= 1;
            GameObject truck = Instantiate(fireTruckPrefab, spawnPoint.position, Quaternion.identity);
            truck.GetComponent<FireTruckScript>().SetTarget(buildingOnFire);
        }
    }
}
