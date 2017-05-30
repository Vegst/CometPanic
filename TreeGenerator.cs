using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour {

    public GameObject Tree;

	void Start () {
        Vector2[] vertices = GetComponent<EdgeCollider2D>().points;
        for (int i = 0; i < vertices.Length; i += 5)
        {
            float a;
            if (i == 0)
            {
                a = Mathf.Atan2(vertices[i + 1].y - vertices[i].y, vertices[i + 1].x - vertices[i].x) * Mathf.Rad2Deg;
            }
            else
            {
                a = Mathf.Atan2(vertices[i].y - vertices[i - 1].y, vertices[i].x - vertices[i - 1].x) * Mathf.Rad2Deg;
            }
            GameObject tree = Instantiate(Tree) as GameObject;
            tree.transform.parent = transform;
            tree.transform.localPosition = vertices[i];
            tree.transform.Rotate(new Vector3(
                0, //GetRandomValue(tree.transform.position, 0) * 10,
                0, //GetRandomValue(tree.transform.position, 1) * 20,
                a));
        }
    }

    static float GetRandomValue(Vector3 p, int seed)
    {
        Random.InitState((int)(p.x + p.y + p.z + seed));
        return Random.Range(-1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
