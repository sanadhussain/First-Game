using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButton : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private float sideSpeed;
	private bool moveRightOrLeft;
	private static bool dontMove;

	private void Start()
	{
		sideSpeed = PlayerMovement.instance.sideForce;
	}
	public void moveLeft()
	{
		moveRightOrLeft = false;
		dontMove = false;
	}
	public void moveRight()
	{	
		moveRightOrLeft = true;
		dontMove = false;
	}
	public void zeroVelocity()
	{
	
		dontMove = true;
	}
	public void FixedUpdate()
	{
		
		if (moveRightOrLeft && !dontMove)
		{
			
			rb.AddForce(sideSpeed * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
		}
		else if (!moveRightOrLeft && !dontMove)
		{
			
			rb.AddForce(-sideSpeed * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
		}
		else
		{
			
		}
		
	}
}
