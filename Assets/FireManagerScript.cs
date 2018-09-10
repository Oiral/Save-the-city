using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class FireManagerScript : MonoBehaviour {

    public GameObject fireParticlePrefab;

    public GameObject buildingParent;
    public List<GameObject> buildings;

    private void Start()
    {
        foreach (Transform building in buildingParent.GetComponentsInChildren<Transform>())
        {
            //check if its not the parent
            if (building != buildingParent.transform)
            {
                buildings.Add(building.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartFire();
            GetName();
        }
    }

    public void StartFire()
    {
        GameObject randomBuilding = buildings[Random.Range(0, buildings.Count)];

        Instantiate(fireParticlePrefab, randomBuilding.transform.position, Quaternion.Euler(-90, 0, 0));
    }

    public void GetName()
    {
        List<string> firstNames = ReadAndSplitFile("Assets/Resources/Names.txt" , '\n');

        List<string> lastNames = ReadAndSplitFile("Assets/Resources/LastNames.txt", '\n');

        Debug.Log(firstNames[Random.Range(0, firstNames.Count)] +" "+ lastNames[Random.Range(0, lastNames.Count)]);
    }

    public List<string> ReadAndSplitFile(string path,char splitString)
    {
        List<string> items = new List<string>();
        
        StreamReader reader = new StreamReader(path);

        string line = reader.ReadToEnd();

        items = line.Split(splitString).ToList();
        reader.Close();

        return items;

        
    }
}
