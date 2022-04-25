using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsatcle_move : MonoBehaviour
{
    private Vector3 postion;
    private float maxSpeed = 25f;
    private int target_postion;
    private bool move;
    public Rigidbody rb;
    private float temp;
    private float laneSize = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        postion = transform.position;
        temp = transform.position.z;
        target_postion = 5;
        move= true;
      
    }
	private void FixedUpdate()
	{
       rb.velocity = new Vector3(rb.velocity.x,
            rb.velocity.y,
            rb.velocity.z - maxSpeed * Time.deltaTime);
		if (rb.velocity.magnitude > maxSpeed)
		{
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}
	}
	// Update is called once per frame
	void Update()
    {
        if(transform.tag == "Obstacle2")
		{

		}
		else if(transform.tag == "Obstacle")   
		{
            obstacleMoveX();
        }
             
    }
    public Vector3 getObstaclePostion()
	{
        return postion;
	}
    public void setObstaclePostion(Vector3 newPostion)
	{
        transform.position = newPostion;
	}
    private void obstacleMoveZ()
	{
        
        if ((int)transform.position.z == transform.position.z + 5 && move)
		{
            
            target_postion *= -1;
            move = false;
		}
        else if((int)transform.position.z == transform.position.z - 5 && !move)
		{
            target_postion *= -1;
            move = true;
        }
        if ((int) transform.position.z <= temp + laneSize && (int)transform.position.z >= temp - laneSize)
		{
           
            transform.position = new Vector3(transform.position.x,
                    transform.position.y,
                    Mathf.MoveTowards(transform.position.z, transform.position.z + target_postion, 0.05f)
                    );
        }

	}

    private void obstacleMoveX()
	{
        
        if((int)transform.position.x == 5 && move)
		{
            
            target_postion *= -1;
            move = false;
		}
        else if ((int)transform.position.x == -5 && !move)
        {
           
            target_postion *= -1;
            move = true;
        }

        if ((int)transform.position.x <= 5 && (int)transform.position.x >= -5)
        {
            transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, transform.position.x + target_postion,Time.deltaTime * 8f),
                    transform.position.y,
                    transform.position.z
                    );
        }
       
    }
}
