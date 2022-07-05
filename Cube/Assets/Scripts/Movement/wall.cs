using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Class for side Walls
public class wall : MonoBehaviour
{
	private Rigidbody rb;
	float resettime;
	bool endGame = true;
	int count;
	private void Start()
	{
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}
	private void Update()
	{
		if (Time.time - resettime > 5f)
		{
			count = 0;
		}
		if (count == 2 && endGame)
		{
			endGame = false;
			PlayerMovement.instance.EndGame();
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.tag == "Player")
		{
			count++;
			resettime = Time.time;
			
			rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, rb.velocity.z);
		}
	
	}
}
