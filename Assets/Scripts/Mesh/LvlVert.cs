using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LvlVert : MonoBehaviour {

	private Vector3 vertex;
    private Guid id;
    private GameObject vertNode;

    public Vector3 Vertex {
        get {return this.vertex;}
    }

    public Guid Id {
        get {return this.id;}
    }

    public GameObject VertNode {
        get {return this.vertNode;}
        set {this.vertNode = value;}
    }

    public LvlVert(Vector3 vert) {
        id = Guid.NewGuid();
        vertex = vert;
    }
}
