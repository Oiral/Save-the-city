using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FireStationScript : MonoBehaviour {

    public GameObject fireTruckPrefab;

    public Transform spawnPoint;

    public Text fireTruckCounter;

    public int spareTrucks = 3;

    public int maxTrucks = 3;

    public void CallFireTruck(GameObject buildingOnFire)
    {
        if (spareTrucks > 0)
        {
            UpdateTruck(-1);
            GameObject truck = Instantiate(fireTruckPrefab, spawnPoint.position, Quaternion.identity);
            truck.GetComponent<FireTruckScript>().SetTarget(buildingOnFire);
        }
    }

    public void UpdateTruck(int amount)
    {
        spareTrucks += amount;
        fireTruckCounter.text = spareTrucks + "/" + maxTrucks;
    }
}
