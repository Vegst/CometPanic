using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

    private List<GameObject> chunks;

    void Start ()
    {
        chunks = new List<GameObject>();
    }
	
	void Update ()
    {
        /*
        float cameraMinX = Camera.main.gameObject.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        float cameraMaxX = Camera.main.gameObject.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float x0 = Mathf.Floor(cameraMinX / chunkSize) * chunkSize - offset;
        float x1 = Mathf.Ceil(cameraMaxX / chunkSize) * chunkSize + offset;

        // Delete old
        foreach (GameObject chunk in GetChunksToRemove())
        {
            chunks.Remove(chunk);
            Destroy(chunk);
        }


        // Generate new
        for (float x = x0; x < x1; x += chunkSize)
        {
            if (!IsChunkInInterval(x, x + chunkSize))
            {
                GameObject chunk = Instantiate(chunkPrefab) as GameObject;
                ChunkMeshGenerator generator = chunk.GetComponent<ChunkMeshGenerator>();
                chunk.transform.position = new Vector2(x, x / 2);
                generator.size.x = chunkSize;
                generator.size.y = chunkSize / 2;
                generator.a0 = generator.size.y / generator.size.x + GetRandomValue(x);
                generator.a1 = generator.size.y / generator.size.x + GetRandomValue(x + chunkSize);
                generator.iterations = 20;
                chunks.Add(chunk);
            }
        }
        */
    }
}
