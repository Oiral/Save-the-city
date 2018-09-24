using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LvlEdge : MonoBehaviour {

	private Guid id;
    private LvlVert vert1;
    private LvlVert vert2;

    private GameObject edgeNode;

    public Guid Id {
        get {return this.id;}
    }

    public LvlVert Vert1 {
        get {return this.vert1;}
        set {this.vert1 = value;}
    }

    public LvlVert Vert2 {
        get {return this.vert2;}
        set {this.vert2 = value;}
    }
    
    public GameObject EdgeNode {
        get {return this.edgeNode;}
        set {this.edgeNode = value;}
    }

    public LvlEdge(LvlVert vert1, LvlVert vert2) {
        id = Guid.NewGuid();
        this.vert1 = vert1;
        this.vert2 = vert2; 
    }
}
