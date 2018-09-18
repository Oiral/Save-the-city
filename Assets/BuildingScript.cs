using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BuildingScript : MonoBehaviour {

    public bool onFire;
    public bool demolished;

    public string nameOfPerson;
    public float timeForFire;

    GameObject infoDisplay;
    public GameObject infoDisplayPrefab;

    public int fireEngines = 0;

    public GameObject fireParticlePrefab;
    GameObject fire;

    public float fireTimer = 0;

    public Material demolishedMat;

    private void Update()
    {
        DisplayCheck();
        
        if (fireEngines > 0)
        {
            timeForFire -= Time.deltaTime * fireEngines;
            //Debug.Log(timeForFire);
            if (timeForFire < 0)
            {
                onFire = false;
                fireTimer = 0;
            }
        }
        if (onFire && fireEngines == 0)
        {
            fireTimer += Time.deltaTime;

            if (fireTimer > 10)
            {
                //DemolishBuilding();
            }
        }
    }

    public void DisplayCheck()
    {
        //if the building is on fire and there isn't a display
        if (onFire && infoDisplay == null)
        {
            infoDisplay = Instantiate(infoDisplayPrefab, transform.position, Quaternion.identity, transform);
            //infoDisplay.GetComponentInChildren<Button>().onClick.AddListener(CallFireEngine);
            infoDisplay.GetComponentInChildren<Button>().onClick.AddListener(GameObject.FindGameObjectWithTag("InteractionEvent").GetComponent<EventManagerScript>().NextEvent);
            fire = Instantiate(fireParticlePrefab, gameObject.transform.position, Quaternion.Euler(-90, 0, 0));
        }

        //if the building is not on fire and there is a display
        if (!onFire && infoDisplay != null)
        {
            Destroy(infoDisplay);
            Destroy(fire);
            //make sure infoDisplay is cleared
            infoDisplay = null;
            
        }
    }

    public void CallFireEngine()
    {
        //Debug.Log("I need help, pls", gameObject);
        GameObject.FindGameObjectWithTag("Station").GetComponent<FireStationScript>().CallFireTruck(gameObject);
    }

    public void FireEngineNear()
    {
        fireEngines += 1;
    }

    void DemolishBuilding()
    {
        tag = "Untagged";
        Destroy(fire);
        onFire = false;
        demolished = true;
        NavMeshObstacle obstacle = gameObject.AddComponent<NavMeshObstacle>();
        obstacle.size = new Vector3(6, 5, 6);
        obstacle.carving = true;
        GetComponentInChildren<MeshRenderer>().material = demolishedMat;
    }
}
