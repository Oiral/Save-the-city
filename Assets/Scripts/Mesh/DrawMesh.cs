using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMesh : MonoBehaviour {

	public static Mesh AddNewQuad(Mesh mesh,Vector3 vertex1,Vector3 vertex2,Vector3 vertex3,Vector3 vertex4)
    {
        //Debug.Log("Adding new quad");
        Vector3[] verts = new Vector3[mesh.vertexCount + 4];
        int[] tris = new int[mesh.triangles.Length + 6];

        verts[verts.Length - 4] = vertex1;
        verts[verts.Length - 3] = vertex2;
        verts[verts.Length - 2] = vertex3;
        verts[verts.Length - 1] = vertex4;

        tris[tris.Length - 6] = verts.Length - 4;
        tris[tris.Length - 5] = tris[tris.Length - 2] = verts.Length - 3;
        tris[tris.Length - 4] = tris[tris.Length - 3] = verts.Length - 2;
        tris[tris.Length - 1] = verts.Length - 1;

        mesh.Clear();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        return mesh;
    }
}
