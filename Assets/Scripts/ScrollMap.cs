using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMap : MonoBehaviour
{
    public Vector2 startPos;
    public float endX;
    public float spd;
    public float mapSpd;

    void Update()
    {
        transform.position += Vector3.left * mapSpd * spd * Time.deltaTime;

        if (transform.position.x < endX)
        {
            transform.position = startPos + Vector2.left * mapSpd * spd * Time.deltaTime;
        }
    }
}

