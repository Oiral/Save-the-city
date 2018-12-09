using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public List<Corner> connectedCorners;

    public List<Block> connectedBlocks;

    public bool onFire;

    public GameObject fire;

    public Pump attachedPump;

    public InfernoTower infernoTower;

    public bool blockerOnBlock;

    public PlayerMovementScript attachedPlayer;

    //Set the costume
    private void Start()
    {
        if (LevelManager.instance.costumeSet != null)
        {
            Instantiate(LevelManager.instance.costumeSet.blockCostumes[Random.Range(0, LevelManager.instance.costumeSet.blockCostumes.Count)],transform);
        }
    }

    private void OnMouseDown()
    {
        LevelManager.instance.CheckBlock(this);
    }

    public void SetOnFire()
    {
        onFire = true;
        //Spawn in fire
        fire = Instantiate(LevelManager.instance.firePrefab,transform.position,Quaternion.Euler(-90,0,0), transform);
        //If there is an attached pump
        if (attachedPump != null)
        {
            //There is a pump attached to this block
            //Remove it from the gamemanager
            LevelManager.instance.LosePump(attachedPump);
        }
        
    }
    public void RemoveFire()
    {
        onFire = false;
        Destroy(fire);
        LevelManager.instance.fire.RemoveBlock(this);
        
        if (infernoTower != null)
        {
            LevelManager.instance.infernoTowers.Remove(infernoTower);
            //If there is no more inferno towers
            if (LevelManager.instance.infernoTowers.Count <= 0)
            {
                LevelManager.instance.WinLevel();
            }
        }
    }

    [ContextMenu("Remove Block")]
    public void RemoveBlock()
    {
        foreach (Block block in connectedBlocks)
        {
            block.connectedBlocks.Remove(this);
            if (block.connectedCorners.Count == 0)
            {
                RemoveBlock();
            }
        }
        foreach (Corner corner in connectedCorners)
        {
            corner.connectedBlocks.Remove(this);
            if (corner.connectedCorners.Count == 0)
            {
                corner.RemoveCorner();
            }
        }
        DestroyImmediate(gameObject);
    }
}
