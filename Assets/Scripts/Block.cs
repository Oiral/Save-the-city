using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public List<Corner> connectedCorners;

    public List<Block> connectedBlocks;

    public bool onFire;

    public GameObject fire;

    public Pump attachedPump;

    public Fire infernoTower;

    public bool blockerOnBlock;

    private void OnMouseDown()
    {
        GameManager.instance.CheckBlock(this);
    }

    public void SetOnFire()
    {
        onFire = true;
        //Spawn in fire
        fire = Instantiate(GameManager.instance.firePrefab,transform.position,Quaternion.Euler(-90,0,0), transform);
        //If there is an attached pump
        if (attachedPump != null)
        {
            //There is a pump attached to this block
            //Remove it from the gamemanager
            GameManager.instance.LosePump(attachedPump);
        }
        
    }
    public void RemoveFire()
    {
        onFire = false;
        Destroy(fire);
        GameManager.instance.fires[0].RemoveBlock(this);

        if (infernoTower != null)
        {
            GameManager.instance.LoseInfernoTower();
        }
    }
}
