using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class of movement button for movemnet of player to left or right 

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
	// Listener for left movemnet button
	public void moveLeft()
	{
		moveRightOrLeft = false;
		dontMove = false;
	}
	// Listener for right movemnet button
	public void moveRight()
	{	
		moveRightOrLeft = true;
		dontMove = false;
	}
	// Listener for when either button are reaslesd
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
	}
}
