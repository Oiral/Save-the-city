using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public List<Corner> connectedCorners;

    public bool onFire;

    public GameObject fire;

    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().CheckBlock(this);
    }

    public void SetOnFire()
    {
        onFire = true;
        //Spawn in fire
        fire = Instantiate(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().firePrefab,transform.position,Quaternion.identity, transform);
    }
    public void RemoveFire()
    {
        onFire = false;
        Destroy(fire);
    }
}
