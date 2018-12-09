using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapMarker : MonoBehaviour {

    public UnityEvent markerEvent;

    public int markerNumber;

    private void Start()
    {
        MapManager.instance.markers.Add(this);
        markerNumber = MapManager.instance.markers.IndexOf(this);
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
