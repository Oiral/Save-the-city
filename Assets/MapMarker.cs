using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapMarker : MonoBehaviour {

    public UnityEvent markerEvent;

    public int markerNumber;

    private void Start()
    {
        MapManager map = MapManager.instance;
        map.markers.Add(this);
        markerNumber = map.markers.IndexOf(this);

        SceneFunctions sceneFunctions = map.GetComponent<SceneFunctions>();

        markerEvent.AddListener(delegate { sceneFunctions.LoadLevel(markerNumber); });
    }

    private void OnMouseDown()
    {
        SelectMarker();
    }

    private void SelectMarker()
    {
        MapManager.instance.SetMarker(markerNumber);
    }
}
