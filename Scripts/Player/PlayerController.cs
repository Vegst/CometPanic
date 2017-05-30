using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        rb.AddForce(Vector2.right * Input.GetAxis("Horizontal")*50);
        for (int i = 0; i < Input.touchCount; i++)
            if (Input.GetTouch(i).phase.Equals(TouchPhase.Began))
            {
                rb.AddForce(Vector2.right * 500);
            }
        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector2.right * 500);
        }
    }
}
