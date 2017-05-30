using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MapGenerator : MonoBehaviour {

    public int chunkWidth;
    public int chunkOffset;
    public GameObject chunkPrefab;
    public bool collidable;

    void LateUpdate()
    {
        float cameraMinX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - Camera.main.transform.position.z)).x;
        float cameraMaxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, transform.position.z - Camera.main.transform.position.z)).x;

        // Delete old chunk
        foreach (GameObject c in GetChunksToRemove())
        {
            Destroy(c);
        }

        // Create new chunk
        for (float x = Mathf.Floor((cameraMinX - chunkOffset) / chunkWidth) * chunkWidth; x < Mathf.Ceil((cameraMaxX + chunkOffset) / chunkWidth) * chunkWidth; x += chunkWidth)
        {
            if (!IsChunkInInterval(x, x + chunkWidth))
            {
                GameObject chunk = Instantiate(chunkPrefab) as GameObject;
                ChunkGenerator generator = chunk.AddComponent<ChunkGenerator>() as ChunkGenerator;
                chunk.transform.parent = transform;
                chunk.transform.localPosition = new Vector2(x,0);
                generator.size = new Vector2(chunkWidth, 0);
                generator.a0 = GetRandomValue(transform.position + new Vector3(x, 0f, 0f), 2);
                generator.a1 = GetRandomValue(transform.position + new Vector3(x + chunkWidth, 0f, 0f), 2);
                generator.iterations = 11;
                generator.depth = 1;
                generator.chunkPrefab = chunkPrefab;
                generator.collidable = collidable;
                generator.offset = chunkOffset;
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
            if (!IsIntervalVisible(child.gameObject.transform.position.x - chunkOffset, child.gameObject.transform.position.x + generator.size.x + chunkOffset))
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
            if (child.gameObject.transform.position.x + generator.size.x > x0 + 0.1f &&
                child.gameObject.transform.position.x < x1 - 0.1f)
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
