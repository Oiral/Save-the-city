using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour {

    public bool onFire;

    public string nameOfPerson;
    public float timeForFire;

    public GameObject infoDisplay;
    public GameObject infoDisplayPrefab;

    public int fireEngines = 0;

    private void Update()
    {
        //DisplayCheck();
        
        if (fireEngines > 0)
        {
            timeForFire -= Time.deltaTime * fireEngines;
            Debug.Log(timeForFire);
        }

    }

    public void DisplayCheck()
    {
        //if the building is on fire and there isn't a display
        if (onFire && infoDisplay == null)
        {
            infoDisplay = Instantiate(infoDisplayPrefab, transform.position, Quaternion.identity, transform);
        }

        //if the building is not on fire and there is a display
        if (!onFire && infoDisplay != null)
        {
            Destroy(infoDisplay);
            //make sure infoDisplay is cleared
            infoDisplay = null;
        }
    }


    public void FireEngineNear()
    {
        fireEngines += 1;
    }
}
