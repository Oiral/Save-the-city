using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireTruckScript : MonoBehaviour {

    NavMeshAgent navAgent;
    public void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public float distanceCheckDist = 5;

    public GameObject localTarget;

    bool inRange;

    public void SetTarget(GameObject target)
    {
        localTarget = target;
        navAgent.SetDestination(target.transform.position);
        inRange = false;
    }

    private void Update()
    {
        /*
        if (navAgent.destination != target.transform.position)
        {
            //set the target
            navAgent.SetDestination(target.transform.position);
            Debug.Log("Changing destination");
        }*/
        CheckDestination();
    }

    public void CheckDestination()
    {
        //first just check if the building were going too is on fire
        if (localTarget.GetComponent<BuildingScript>() != null)
        {
            if (localTarget.GetComponent<BuildingScript>().onFire == false)
            {
                SetTarget(GameObject.FindGameObjectWithTag("Station"));
            }
        }

        float dist = Vector3.Distance(localTarget.transform.position, transform.position);

        //if we are in range of the target
        if (dist < distanceCheckDist)
        {
            if (localTarget.tag == "Station")
            {
                //we are in range of the station
                Destroy(gameObject);
                localTarget.GetComponent<FireStationScript>().UpdateTruck(1);
            }
            else if (!inRange)
            {
                inRange = true;
                // we must be at a fire
                //Debug.Log("Fire Fire Fire");
                localTarget.GetComponent<BuildingScript>().FireEngineNear();
                //check if the fire is put out

            }
            else
            {
                if (localTarget.GetComponent<BuildingScript>().onFire == false)
                {
                    //send fire engine back to base
                    SetTarget(GameObject.FindGameObjectWithTag("Station"));
                    //navAgent.SetDestination(GameObject.FindGameObjectWithTag("Station").transform.position);
                }
            }
        }

        
    }

}
