using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class MeshGenerator : MonoBehaviour {

    
    public float thickness = 20;
    public float precision = 1f;

    void Start()
    {
        ChunkGenerator generator = GetComponent<ChunkGenerator>() as ChunkGenerator;
        if (generator.depth == 0)
        {
            Vector2 p0 = Vector2.zero;
            Vector2 p3 = generator.size;
            float dis = Vector2.Distance(p0, p3) / 2;
            Vector2 p1 = p0 + (new Vector2(1, generator.a0)).normalized * dis;
            Vector2 p2 = p3 - (new Vector2(1, generator.a1)).normalized * dis;

            List<Vector2> topline = new List<Vector2>();
            List<Vector2> bottomline = new List<Vector2>();
            foreach (Vector2 v in BezierCurver.Generate((int)(generator.size.x / precision), p0, p1, p2, p3))
            {
                topline.Add(v);
            }
            foreach (Vector2 v in BezierCurver.Generate((int)(generator.size.x / precision), p0 + Vector2.down * thickness, p1 + Vector2.down * thickness, p2 + Vector2.down * thickness, p3 + Vector2.down * thickness))
            {
                bottomline.Add(v);
            }
            
            GetComponent<MeshFilter>().mesh = createMesh(topline.ToArray(), bottomline.ToArray());

            GetComponent<EdgeCollider2D>().points = topline.ToArray();
            if (!generator.collidable) {
                GetComponent<EdgeCollider2D>().isTrigger = true;
            }
        }
    }



    Mesh createMesh(Vector2[] topline, Vector2[] bottomline)
    {
        

        //int[] triangles = new int[6] { 0, 1, 3, 3, 2, 0 };
        List<int> triangles = new List<int>();
        for (int i = 0; i < topline.Length - 1; i++)
        {
            triangles.AddRange(new int[] { i, i + 1, topline.Length + i + 1 });
            triangles.AddRange(new int[] { topline.Length + i + 1, topline.Length + i, i });
        }

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[topline.Length + bottomline.Length];
        for (int i = 0; i < topline.Length; i++)
        {
            vertices[i] = new Vector3(topline[i].x, topline[i].y, 0);
        }
        for (int i = 0; i < bottomline.Length; i++)
        {
            vertices[topline.Length + i] = new Vector3(bottomline[i].x, bottomline[i].y, 0);
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
        
        for (int i = 0; i < topline.Length; i++)
        {
            uvs.Add(new Vector2((float)i / (float)(topline.Length - 1), 1));
        }
        for (int i = 0; i < bottomline.Length; i++)
        {
            uvs.Add(new Vector2((float)i / (float)(bottomline.Length - 1), 0));
        }
        

        // Create the mesh
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();


        return mesh;
    }
}
