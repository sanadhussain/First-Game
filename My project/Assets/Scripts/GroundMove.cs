using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z - 25.0f * Time.deltaTime);
    }
    public Vector3 getTransform()
	{
        return transform.position;
	}

    public void setTransform(Vector3 postion)
	{
        transform.position = postion;
	}
}
