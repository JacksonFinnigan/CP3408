using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{



    public float distanceToMove = 10f;

    Vector3 startPoint;
    Vector3 endPoint;

    void Awake()
    {

        startPoint = transform.position;
        endPoint = startPoint;
        endPoint.y = startPoint.y - distanceToMove;

    }

    public float speed = 5f;

    void Update()
    {
        Vector3 currrentPoint = transform.position;
        transform.position = Vector3.MoveTowards(currrentPoint, endPoint, speed * Time.deltaTime);


        if (Vector3.Distance(currrentPoint, endPoint) < 0.001f)
        {
            endPoint.y = startPoint.y;
        }
        if (Vector3.Distance(currrentPoint, startPoint) < 0.001f)
        {
            endPoint.y = startPoint.y - distanceToMove;
        }

    }


}
