using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsatcle_move : MonoBehaviour
{

    private Rigidbody rb;
    public bool move;
    private float temp;
    public float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        temp = transform.position.z;
        rb = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update()
    {
        if(transform.tag == "Obstacle2")
		{
            //obstacleMoveZ();
		}
		else if(transform.tag == "Obstacle")   
		{
            obstacleMoveX();
        }
             
    }

    private void obstacleMoveZ()
	{
        
        if (transform.position.z >= temp + 5 && move)
		{         
            MoveSpeed *= -1;
            move = false;
            if (rb.velocity.magnitude > 0)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 3f);
            }
        }
        else if(transform.position.z <= temp - 5 && !move)
		{
            MoveSpeed *= -1;
            move = true;
            if (rb.velocity.magnitude > 0)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 3f);
            }
        }
        if (rb.velocity.magnitude > 6)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 6);
        }
        else if (rb.velocity.magnitude < 6)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 6);
        }

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z + MoveSpeed * Time.deltaTime);

    }

    private void obstacleMoveX()
	{
        
        if((int)transform.position.x >= 5 && move)
		{
            
            MoveSpeed *= -1;
            move = false;
            if (rb.velocity.magnitude > 0)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 3f);
            }
        }
        else if ((int)transform.position.x <= -5 && !move)
        {
            
            MoveSpeed *= -1;
            move = true;
            if (rb.velocity.magnitude > 0)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 3f);
            }
        }

        
            
			if (rb.velocity.magnitude > 6)
			{
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 6);
            }
			else if(rb.velocity.magnitude < 6)
			{
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, 6);
            }
           
            rb.velocity = new Vector3 (rb.velocity.x + MoveSpeed *Time.deltaTime , rb.velocity.y, rb.velocity.z);
        
       
    }
    
}
