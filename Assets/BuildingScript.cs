using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour {

    public bool onFire;

    public string nameOfPerson;
    public float timeForFire;

    GameObject infoDisplay;
    public GameObject infoDisplayPrefab;

    public int fireEngines = 0;

    public GameObject fireParticlePrefab;
    GameObject fire;

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
            }
        }

    }

    public void DisplayCheck()
    {
        //if the building is on fire and there isn't a display
        if (onFire && infoDisplay == null)
        {
            infoDisplay = Instantiate(infoDisplayPrefab, transform.position, Quaternion.identity, transform);
            infoDisplay.GetComponentInChildren<Button>().onClick.AddListener(CallFireEngine);
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
}
