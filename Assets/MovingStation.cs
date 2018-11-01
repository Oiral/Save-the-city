﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moving Station", menuName = "Player/Moving Station", order = 1)]
public class MovingStation : ScriptableObject {

    public string playerName = "Truck 1";
    public int maxMoves = 3;
    public int maxHealth = 4;
    public MovementType playerMovementType = MovementType.Corner;
    public Color playerColor = Color.red;

    public void SetPlayer(PlayerMovementScript movementScript)
    {
        movementScript.maxActionPoints = maxMoves;
        movementScript.actionPoints = maxMoves;

        movementScript.maxHealth = maxHealth;
        movementScript.currentHealth = maxHealth;

        movementScript.movementType = playerMovementType;
    }
}
