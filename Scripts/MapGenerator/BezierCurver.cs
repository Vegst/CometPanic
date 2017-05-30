using UnityEngine;
using System.Collections;

public class BezierCurver {

    public static Vector2[] Generate(int iterations, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        // Cubic Bezier curve:
        // y = (1-t)^3*p0 + 3*(1-t)^2*t*p1 + 3*(1-t)*t^2*p2 + t^3*p3
        Vector2[] vertices = new Vector2[iterations + 1];
        for (int i = 0; i <= iterations; i++)
        {
            float t = (float)i / iterations;
            vertices[i] = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
        }
        return vertices;
    }
}
