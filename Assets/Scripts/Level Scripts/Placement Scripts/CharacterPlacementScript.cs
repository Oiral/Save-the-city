using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPlacementScript : MonoBehaviour {

    public int currentSelectedPlayer;

    GameManager gm;

    public GameObject SpawnPlayerPrefab;

    public GameObject SpawnStationPrefab;

    public List<PlayerDetails> spawned = new List<PlayerDetails>();

    public Button startLevelButton;

    private void Awake()
    {
        gm = GameManager.instance;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            bool cornerClicked = false;
            bool blockClicked = false;
            bool startingPanelClicked = false;

            GameObject cornerObject = null;
            GameObject blockObject = null;

            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            hits = Physics.RaycastAll(ray);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                switch (hit.transform.tag)
                {
                    case "Corner":
                        cornerClicked = true;
                        cornerObject = hit.transform.gameObject;
                        break;
                    case "Blocks":
                        blockClicked = true;
                        blockObject = hit.transform.gameObject;
                        break;

                    case "Start":
                        startingPanelClicked = true;
                        break;
                }
            }

            if (spawned.Contains(gm.playerSquads[currentSelectedPlayer]) == false)
            {

                if (startingPanelClicked && cornerClicked)
                {
                    if (gm.playerSquads[currentSelectedPlayer].playerMovementType == MovementType.Corner)
                    {
                        //Spawn in the corner squad member
                        Debug.Log("Attempt To Spawn");
                        SpawnPlayer(cornerObject.GetComponent<Corner>());
                    }
                }
                else if (startingPanelClicked && blockClicked)
                {
                    if (gm.playerSquads[currentSelectedPlayer].playerMovementType == MovementType.Block)
                    {
                        //Spawn in the block squad member
                        SpawnPlayer(blockObject.GetComponent<Block>());
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setSelectedPlayer(0);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setSelectedPlayer(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setSelectedPlayer(2);
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            setSelectedPlayer(3);
        }
    }

    public void setSelectedPlayer(int number)
    {
        if (number < gm.playerSquads.Count)
        {
            Debug.Log("Set to player" + number);
            currentSelectedPlayer = number;
        }
        else
        {
            Debug.Log("Player not active yet");
        }
    }

    public void SpawnPlayer(Corner corner)
    {
        startLevelButton.interactable = true;
        GameObject player;
        if (gm.playerSquads[currentSelectedPlayer].station == true)
        {
            player = Instantiate(SpawnStationPrefab);
        }
        else
        {
            player = Instantiate(SpawnPlayerPrefab);
        }
        PlayerSpawnObject spawnScript = player.GetComponent<PlayerSpawnObject>();

        spawned.Add(gm.playerSquads[currentSelectedPlayer]);

        spawnScript.details = gm.playerSquads[currentSelectedPlayer];
        spawnScript.attachedCorner = corner;
        player.transform.position = corner.gameObject.transform.position;
    }

    public void SpawnPlayer(Block block)
    {
        startLevelButton.interactable = true;
        GameObject player = Instantiate(SpawnPlayerPrefab);
        PlayerSpawnObject spawnScript = player.GetComponent<PlayerSpawnObject>();

        spawned.Add(gm.playerSquads[currentSelectedPlayer]);

        spawnScript.details = gm.playerSquads[currentSelectedPlayer];
        spawnScript.attachedBlock = block;
        player.transform.position = block.gameObject.transform.position;
    }
}
