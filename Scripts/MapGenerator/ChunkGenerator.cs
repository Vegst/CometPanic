using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkGenerator : MonoBehaviour {

    public Vector2 size;
    public int offset;
    public float a0;
    public float a1;
    public int iterations;

    public int depth;
    public GameObject chunkPrefab;

    public bool collidable;

	void LateUpdate ()
    {
        if (depth > 0)
        {
            // Delete old
            foreach (GameObject chunk in GetChunksToRemove())
            {
                Destroy(chunk);
            }

            // Create new
            Vector2 p0 = Vector2.zero;
            Vector2 p3 = size;
            float dis = Vector2.Distance(p0, p3) / 2;
            Vector2 p1 = (new Vector2(1, a0)).normalized * dis;
            Vector2 p2 = (new Vector2(1, a1)).normalized * dis;

            Vector2[] points = BezierCurver.Generate(iterations, p0, p1, p2, p3);

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector2 pointSize = points[i + 1] - points[i];
                if (IsIntervalVisible(points[i].x - offset, points[i].x + pointSize.x + offset) &&
                   !IsChunkInInterval(points[i].x, points[i].x + pointSize.x))
                {
                    GameObject chunk = Instantiate(chunkPrefab) as GameObject;
                    ChunkGenerator generator = chunk.AddComponent<ChunkGenerator>() as ChunkGenerator;
                    chunk.transform.parent = transform;
                    chunk.transform.localPosition = points[i];
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
                    generator.a0 += GetRandomValue(transform.position + new Vector3(points[i].x, points[i].y, 0f), generator.depth);
                    generator.a1 += GetRandomValue(transform.position + new Vector3(points[i + 1].x, points[i + 1].y, 0f), generator.depth);
                    generator.iterations = 5;
                    generator.depth = depth - 1;
                    generator.chunkPrefab = chunkPrefab;
                    generator.collidable = collidable;
                    generator.offset = offset;
                }
            }
        }
	}

    static float GetRandomValue(Vector3 p, int seed)
    {
        Random.InitState((int)(p.x + p.y + p.z + seed));
        return Random.Range(-1.0f, 1.0f);
    }

    List<GameObject> GetChunksToRemove()
    {
        List<GameObject> toRemove = new List<GameObject>();
        foreach (Transform child in transform)
        {
            ChunkGenerator generator = child.gameObject.GetComponent<ChunkGenerator>();
            if (!IsIntervalVisible(child.gameObject.transform.localPosition.x - offset, child.gameObject.transform.localPosition.x + generator.size.x + offset))
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
            if (child.gameObject.transform.localPosition.x + generator.size.x > x0 + 0.1f &&
                child.gameObject.transform.localPosition.x < x1 - 0.1f)
            {
                return true;
            }
        }
        return false;
    }

    bool IsIntervalVisible(float x0, float x1)
    {
        Vector3 screenPoint0 = Camera.main.WorldToViewportPoint(transform.position + new Vector3(x0, 0, 0));
        Vector3 screenPoint1 = Camera.main.WorldToViewportPoint(transform.position + new Vector3(x1, 0, 0));

        return screenPoint0.x < 1 && screenPoint1.x > 0;
    }
}
