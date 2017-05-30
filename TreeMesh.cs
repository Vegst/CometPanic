using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[ExecuteInEditMode]
public class TreeMesh : MonoBehaviour {

    public Vector2 size;
    public Vector2 center;

    private MeshFilter meshFilter;

	void Start ()
    {
        meshFilter = GetComponent<MeshFilter>();

        meshFilter.mesh = GenerateMesh(2, 2);
	}

    Mesh GenerateMesh(int hVertices, int vVertices)
    { 
        List<int> triangles = new List<int>();
        for (int y = 0; y < vVertices - 1; y++)
        {
            for (int x = 0; x < hVertices - 1; x++)
            {
                triangles.AddRange(new int[] { x, x + 1, hVertices * (y + 1) + x + 1 });
                triangles.AddRange(new int[] { hVertices * (y + 1) + x + 1, hVertices * (y + 1) + x, x });
            }
        }


        // Create the Vector3 vertices
        List<Vector3> vertices = new List<Vector3>();
        //Vector3[] vertices = new Vector3[topline.Length + bottomline.Length];
        for (int y = 0; y < vVertices; y++)
        {
            for (int x = 0; x < hVertices; x++)
            {
                vertices.Add(new Vector3((x / hVertices - center.x) * size.x, (y / vVertices - center.y), 0));
            }
        }

        // Create uvs
        List<Vector2> uvs = new List<Vector2>();
        /*
        for (float y = topline[0].y - bottomline[0].y; y >= 0; y -= topline[topline.Length-1].x - topline[0].x)
        {
            for (int i = 0; i < topline.Length; i++)
            {
                uvs.Add(new Vector2((float)i / (float)(topline.Length - 1), y / (topline[0].y - bottomline[0].y)));
            }
        }
        */
        for (int y = 0; y < vVertices; y++)
        {
            for (int x = 0; x < hVertices; x++)
            {
                uvs.Add(new Vector2((float)x / (hVertices - 1), 1 - (float)y / (vVertices - 1)));
            }
        }


        // Create the mesh
        Mesh mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();


        return mesh;
    }
	
	void Update ()
    {
	}
}
