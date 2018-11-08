using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawnObject : MonoBehaviour {

    public PlayerDetails details;

    public Corner attachedCorner;
    public Block attachedBlock;

    public GameObject cornerPlayerPrefab;
    public GameObject blockPlayerPrefab;
    public GameObject stationPlayerPrefab;

    private void OnEnable()
    {
        LevelManager.instance.startGame += SpawnRelated;
    }

    public void SpawnRelated()
    {
        if (details.playerMovementType == MovementType.Corner)
        {
            GameObject player;
            if (details.station)
            {
                player = Instantiate(stationPlayerPrefab);
            }
            else
            {
                player = Instantiate(cornerPlayerPrefab);
            }

            
            player.transform.position = transform.position;
            PlayerMovementScript movementScript = player.GetComponent<PlayerMovementScript>();

            movementScript.currentCorner = attachedCorner;
            details.SetPlayer(movementScript);
            Destroy(gameObject);

        }
        else if (details.playerMovementType == MovementType.Block)
        {
            GameObject player = Instantiate(blockPlayerPrefab);
            player.transform.position = transform.position;
            PlayerMovementScript movementScript = player.GetComponent<PlayerMovementScript>();

            movementScript.currentBlock = attachedBlock;
            details.SetPlayer(movementScript);
            Destroy(gameObject);
        }
    }
    
}
