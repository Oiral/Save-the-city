using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquadType {General,Fire,Rescue,Demolishion };

public class Squad {

    public SquadType typeOfSquad = SquadType.General;

    public bool onMission = false;

    public int exp;

    public int level = 0;
}
