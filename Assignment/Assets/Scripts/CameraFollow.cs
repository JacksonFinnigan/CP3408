using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    public Transform target; //use transform when getting into on position, transform or scale
   
    public float smoothSpeed = 10f; //higher value, faster camera will lock on

    public Vector3 offset;

    //late update runs after update so that char movement doesn't affect camera position before char moves
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
    }

    

}
