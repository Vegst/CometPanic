using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSight : MonoBehaviour
{

    public GameObject from;

    void Start()
    {
        if (transform.position.z < from.transform.position.z)
        {
            Renderer r = GetComponent<SpriteRenderer>();
            r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0.0f);
        }
    }

    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(from.transform.position);
        Vector3 herePoint = Camera.main.ViewportToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, transform.position.z));

        if (transform.position.z < from.transform.position.z)//if (GetComponent<CapsuleCollider2D>().bounds.Contains(new Vector2(herePoint.x, herePoint.y)))
        {
            Renderer r = GetComponent<SpriteRenderer>();
            if (r != null)
            {
                r.material.color = Color.Lerp(r.material.color, new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0.0f), Time.deltaTime);
            }
        }
    }
}
