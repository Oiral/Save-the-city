using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MovementType { Corner,Block};

public class PlayerMovementScript : MonoBehaviour {

    public Corner currentCorner;
    public Block currentBlock;
    public float moveSpeed = 10;

    public int actionPoints = 3;

    public int maxActionPoints = 3;

    public Text actionCounter;
    public Text health;

    public MovementType movementType = MovementType.Corner;

    public int maxHealth = 8;

    public int currentHealth = 8;

    private void Start()
    {
        if (currentCorner == null)
        {
            //Find a new corner and set it to the first one
            currentCorner = GameObject.FindGameObjectWithTag("Corner").GetComponent<Corner>();
        }
        if (currentBlock == null)
        {
            //Find a new corner and set it to the first one
            currentBlock = GameObject.FindGameObjectWithTag("Blocks").GetComponent<Block>();
        }
    }

    public void MovePlayer(Corner cornerToMove)
    {
        //check if we can move
        if (cornerToMove.movable == true)
        {
            if (cornerToMove.blocked == true && actionPoints >= 2)
            {
                cornerToMove.UnBlockCorner();
                actionPoints -= 2;
            }
            else if (currentCorner.connectedCorners.Contains(cornerToMove) && actionPoints > 0 && cornerToMove.blocked == false)
            {
                currentCorner = cornerToMove;
                actionPoints -= 1;
            }
        }
    }
    public void MovePlayer(Block blockToMove)
    {
        //check if we can move
        if (currentBlock.connectedBlocks.Contains(blockToMove) && actionPoints > 0 && blockToMove.onFire == false)
        {
            currentBlock.blockerOnBlock = false;
            currentBlock.attachedPlayer = null;

            blockToMove.blockerOnBlock = true;
            blockToMove.attachedPlayer = this;

            currentBlock = blockToMove;
            actionPoints -= 1;
        }
    }

    private void OnMouseDown()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.SelectPlayer(this);
    }

    private void Update()
    {
        switch (movementType)
        {
            case MovementType.Corner:
                if (currentCorner != null)
                {
                    transform.position = Vector3.Lerp(transform.position, currentCorner.gameObject.transform.position, moveSpeed * Time.deltaTime);
                }
                break;
            case MovementType.Block:
                if (currentBlock != null)
                {
                    transform.position = Vector3.Lerp(transform.position, currentBlock.gameObject.transform.position, moveSpeed * Time.deltaTime);
                }
                break;
            default:
                break;
        }

        UpdateUI();
    }

    public void ResetActionPoints()
    {
        actionPoints = maxActionPoints;
    }

    public bool CanRemoveFire(Block blockClicked)
    {
        //Check if we are in range of the fire to be put out
        bool inRange = false;
        if (movementType == MovementType.Corner)
        {
            if (currentCorner.connectedBlocks.Contains(blockClicked))
            {
                inRange = true;
            }
        }
        else if (movementType == MovementType.Block)
        {
            return false;
            /*
            if(currentBlock.connectedBlocks.Contains(blockClicked))
            {
                inRange = true;
            }*/
        }

        //if the block is actually on fire and we can actually put out a fire and we are in range
        if (inRange && actionPoints >= 2 && blockClicked.onFire)
        {
            //Now actually do something with this information
            
                actionPoints -= 2;
                return true;
        }
        else
        {
            return false;
        }
    }

    public void NextTurnCheck()
    {
        //Check the current health
        //check with all the block around to see if we need to lose health
        if (movementType == MovementType.Corner)
        {
            for (int i = 0; i < currentCorner.connectedBlocks.Count; i++)
            {
                if (currentCorner.connectedBlocks[i].onFire)
                {
                    LoseHealth(1);
                }
            }
        }

        if (currentHealth <= 0)
        {
            Debug.Log("I am dead!", gameObject);
            //Check if there are no more players left alive
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 1)
            {
                GameManager.instance.LoseGame();
            }

            Destroy(gameObject);
        }
    }

    public void LoseHealth(int amount)
    {
        currentHealth -= amount;
    }

    public void UpdateUI()
    {
        actionCounter.text = actionPoints.ToString();
        health.text = currentHealth.ToString();
    }

}
