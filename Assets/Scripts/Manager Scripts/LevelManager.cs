using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Phase { Placement, PlayerTurn, FireSpread };

public class LevelManager : MonoBehaviour {

    #region Singleton

    public static LevelManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(this);
        }
    }

    #endregion

    public int turnCount = 0;

    public Phase phase = Phase.Placement;

    public PlayerMovementScript selectedPlayer;

    public Material selectedMat;
    public Material unSelectedMat;

    public GameObject firePrefab;

    public List<Pump> pumps;
    public Fire fire;

    public List<InfernoTower> infernoTowers;

    public GameObject loseCanvas;
    public GameObject winCanvas;

    public int globalSeed = 15663;

    [HideInInspector]
    public List<Block> blocksOnFire = new List<Block>();

    public delegate void StartAction();
    public event StartAction startGame;

    public PlayerDetails reward;

    public void StartGame()
    {
        if (startGame != null)
        {
            startGame.Invoke();
            phase = Phase.PlayerTurn;
        }
    }

    public void CheckCorner(Corner cornerToCheck)
    {
        if (selectedPlayer != null)
        {
            if (selectedPlayer.movementType == MovementType.Corner)
            {
                selectedPlayer.MovePlayer(cornerToCheck);
            }
        }
    }

    public void CheckBlock(Block blockToCheck)
    {
        if (selectedPlayer != null)
        {
            if (selectedPlayer.CanRemoveFire(blockToCheck))
            {
                blockToCheck.RemoveFire();

                //set a corner to be disabled
                //Pick a random corner

                List<Corner> corners = blockToCheck.connectedCorners;

                bool lookingForPlace = true;
                while (lookingForPlace)
                {
                    int randnum = Random.Range(0, corners.Count);
                    Corner corner = corners[randnum].GetComponent<Corner>();

                    if (corner.blocked == false)
                    {
                        corner.BlockCorner();
                        corner.GetComponent<MeshRenderer>().material = unSelectedMat;
                        lookingForPlace = false;
                    }
                    corners.Remove(corner);

                    //check if all are full
                    if (corners.Count == 0)
                    {
                        lookingForPlace = false;
                    }
                }
            }else if (selectedPlayer.movementType == MovementType.Block)
            {
                selectedPlayer.MovePlayer(blockToCheck);
            }
        }
    }

	public void NextTurn()
    {
        //Debug.Log("Next Turn");

        turnCount += 1;

        //Reset the action points
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerMovementScript>().ResetActionPoints();
            players[i].GetComponent<PlayerMovementScript>().NextTurnCheck();
        }

        //Create a new fire

        /*
        if (blocksOnFire.Count > 2)
        {
            SpreadRandomFire();
        }
        else
        {
            MakeNewFire();
        }*/

        //Spread the fire
        fire.ExpandFire();
        //Debug.Log(infernoTowers.Count);
    
    }

    public void MakeNewFire()
    {
        //find a empty screen
        List<GameObject> blocks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blocks"));

        bool lookingForPlace = true;
        while (lookingForPlace)
        {
            int randnum = Random.Range(0, blocks.Count);
            Block block = blocks[randnum].GetComponent<Block>();

            if (block.onFire == false)
            {
                
                lookingForPlace = false;
                SetBlockOnFire(block);
            }
            blocks.Remove(block.gameObject);

            //check if all are full
            if (blocks.Count == 0)
            {
                lookingForPlace = false;
            }
        }
    }

    public void SpreadRandomFire()
    {
        //Pick a random block that is already on fire
        Block randomBlock = blocksOnFire[Random.Range(0, blocksOnFire.Count)];

        //Pick a random corner from the block
        List<Corner> eligableCorners = randomBlock.connectedCorners;
        bool lookingForSpread = true;

        while (lookingForSpread)
        {
            Corner randomCorner = eligableCorners[Random.Range(0, randomBlock.connectedCorners.Count)];
            List<Block> cornerBlocks = randomCorner.connectedBlocks;
            //remove the random block because we dont want to try spread to the block were spreading from
            cornerBlocks.Remove(randomBlock);
            if (cornerBlocks.Count > 0)
            {
                //We still have another block to spread too
                //Pick a random block to spread too now
                
                int RandNum = Random.Range(0, cornerBlocks.Count);
                if (cornerBlocks[RandNum].onFire == false)
                {
                    SetBlockOnFire(cornerBlocks[RandNum]);
                    lookingForSpread = false;
                }
                
            }
        }
        

        //Get the connected blocks


    }

    public void SetBlockOnFire(Block blockToIgnite)
    {
        Debug.Log("Setting Block on fire");
        blockToIgnite.SetOnFire();
        blocksOnFire.Add(blockToIgnite);
    }

    public void SelectPlayer(PlayerMovementScript player)
    {
        if (selectedPlayer != null)
        {
            //Reset the material on the select player
            ChangeMat(unSelectedMat);
            selectedPlayer.PlayerDeselected();
        }

        //select the player
        selectedPlayer = player;

        ChangeMat(selectedMat);
    }

    public void ChangeMat(Material matChange)
    {
        if (selectedPlayer.gameObject.GetComponent<MeshRenderer>() == null)
        {

            selectedPlayer.gameObject.GetComponentInChildren<MeshRenderer>().material = matChange;
        }
        else
        {
            selectedPlayer.gameObject.GetComponent<MeshRenderer>().material = matChange;
        }
    }

    public void LosePump(Pump pumpToLose)
    {
        pumps.Remove(pumpToLose);
        if (pumps.Count <= 0)
        {
            LoseGame();
        }
    }

    public void LoseGame()
    {
        loseCanvas.SetActive(true);
    }

    public void WinLevel()
    {
        if (reward != null)
        {
            GameManager.instance.playerSquads.Add(reward);
        }
        Debug.Log("Win");
        winCanvas.SetActive(true);
    }
    
}
