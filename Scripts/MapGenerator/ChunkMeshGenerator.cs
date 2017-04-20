using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkMeshGenerator : MonoBehaviour {

    
    public float thickness = 10;

    void Start()
    {
        ChunkGenerator generator = GetComponent<ChunkGenerator>() as ChunkGenerator;
        Vector2 p0 = Vector2.zero;
        Vector2 p3 = generator.size;
        float dis = Vector2.Distance(p0, p3) / 2;
        Vector2 p1 = p0 + (new Vector2(1, generator.a0)).normalized * dis;
        Vector2 p2 = p3 - (new Vector2(1, generator.a1)).normalized * dis;

        List<Vector2> vertices = new List<Vector2>();
        foreach (Vector2 v in BezierCurver.Generate(generator.iterations, p0, p1, p2, p3))
        {
            vertices.Add(v);
        }
        vertices.Add(p3 + Vector2.down * thickness);
        vertices.Add(p0 + Vector2.down * thickness);

        createMesh(vertices.ToArray());

    }

    

    void createMesh(Vector2[] vertices2D)
    {
        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i=0; i<vertices.Length; i++) {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        // Create the Vector2 uvs
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }

        // Create the mesh
        Mesh mesh = new Mesh();
        // Set up game object with mesh;
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();


        // Create collider
        GetComponent<PolygonCollider2D>().points = vertices2D;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
