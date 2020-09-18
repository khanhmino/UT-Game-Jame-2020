using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    [Tooltip("How much the camera lags behind the object its following")]
    public float lagBehind;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 noZcamPos = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 newPos = -(noZcamPos - followObject.transform.position) / lagBehind;
        transform.position += new Vector3(newPos.x, newPos.y, 0);
    }
}
