using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnObject : MonoBehaviour {

    public PlayerDetails details;

    public Corner attachedCorner;
    public Block attachedBlock;

    public GameObject cornerPlayerPrefab;
    public GameObject blockPlayerPrefab;

    private void OnEnable()
    {
        LevelManager.startGame += SpawnRelated;
    }

    public void SpawnRelated()
    {
        if (details.playerMovementType == MovementType.Corner)
        {
            GameObject player = Instantiate(cornerPlayerPrefab);
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
