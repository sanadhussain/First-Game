using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
	private Transform player;
	private Rigidbody rb;
	float targetPostion;
	float position;
	float resettime;
	bool endGame = true;
	int count;
	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
	}
	private void Update()
	{
		if (count == 2 && endGame)
		{
			endGame = false;
			PlayerMovement.instance.EndGame();
		}
		if (Time.time - resettime > 5f)
		{
			count = 0;
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
