using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour {

    public Vector2 size;
    public float a0;
    public float a1;
    public int iterations;

    public int depth;
    public GameObject chunkPrefab;

    void Start ()
    {
    }
	
	void LateUpdate ()
    {
        if (GetComponent<ChunkMeshGenerator>() == null)
        {
            // Delete old
            foreach (GameObject chunk in GetChunksToRemove())
            {
                Destroy(chunk);
            }

            // Create new
            Vector2 p0 = new Vector2(transform.position.x, transform.position.y);
            Vector2 p3 = p0 + size;
            float dis = Vector2.Distance(p0, p3) / 2;
            Vector2 p1 = p0 + (new Vector2(1, a0)).normalized * dis;
            Vector2 p2 = p3 - (new Vector2(1, a1)).normalized * dis;

            Vector2[] points = BezierCurver.Generate(iterations, p0, p1, p2, p3);
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector2 pointSize = points[i + 1] - points[i];
                if (IsIntervalVisible(points[i].x, points[i].x + pointSize.x) &&
                   !IsChunkInInterval(points[i].x, points[i].x + pointSize.x))
                {
                    GameObject chunk;
                    if (depth == 0)
                    {
                        chunk = Instantiate(chunkPrefab) as GameObject;
                    }
                    else
                    {
                        chunk = new GameObject();
                    }
                    ChunkGenerator generator = chunk.AddComponent<ChunkGenerator>() as ChunkGenerator;
                    chunk.transform.position = points[i];
                    chunk.transform.parent = transform;
                    generator.size = pointSize;
                    if (i == 0)
                    {
                        generator.a0 = a0;
                    }
                    else
                    {
                        generator.a0 = (points[i + 1].y - points[i - 1].y) / (points[i + 1].x - points[i - 1].x);
                    }

                    if (i == points.Length - 2)
                    {
                        generator.a1 = a1;
                    }
                    else
                    {
                        generator.a1 = (points[i + 2].y - points[i].y) / (points[i + 2].x - points[i].x);
                    }
                    generator.a0 += GetRandomValue(points[i].x);
                    generator.a1 += GetRandomValue(points[i + 1].x);
                    generator.iterations = 5;
                    generator.depth = depth - 1;
                    generator.chunkPrefab = chunkPrefab;
                }
            }
        }
	}

    static float GetRandomValue(float x)
    {
        Random.InitState((int)x);
        return Random.Range(-1.0f, 1.0f);
    }

    List<GameObject> GetChunksToRemove()
    {
        List<GameObject> toRemove = new List<GameObject>();
        foreach (Transform child in transform)
        {
            ChunkGenerator generator = child.gameObject.GetComponent<ChunkGenerator>();
            if (!IsIntervalVisible(child.gameObject.transform.position.x, child.gameObject.transform.position.x + generator.size.x))
            {
                toRemove.Add(child.gameObject);
            }
        }
        return toRemove;
    }

    bool IsChunkInInterval(float x0, float x1)
    {
        foreach (Transform child in transform)
        {
            ChunkGenerator generator = child.gameObject.GetComponent<ChunkGenerator>();
            if (child.gameObject.transform.position.x + generator.size.x <= x1 &&
                child.gameObject.transform.position.x >= x0)
            {
                return true;
            }
        }
        return false;
    }

    bool IsIntervalVisible(float x0, float x1)
    {
        float cameraMinX = Camera.main.gameObject.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        float cameraMaxX = Camera.main.gameObject.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

        return (x0 < cameraMaxX && x1 > cameraMinX);
    }
}
