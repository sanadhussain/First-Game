using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody rb;
	public GameObject restartUi;
    private Touch touch;
	private GameObject[] obstacle;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
		
    }

    // Update is called once per frame
    void Update()
    {
		
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
            Time.timeScale = 1;
		}
    }
	private void FixedUpdate()
	{
		playerMove();
	}
	private void playerMove()
	{
		if (Input.touchCount > 0)
		{
            touch = Input.GetTouch(0);
            
			if (touch.phase == TouchPhase.Moved)
			{
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * Time.deltaTime * 0.2f,
                    transform.position.y,
                    transform.position.z);
			}
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Obstacle2")
		{
			transform.GetComponent<PlayerMovement>().enabled = false;
			obstacle = GameObject.FindGameObjectsWithTag("Obstacle");
			
			for (int i = 0; i < obstacle.Length; i++)
			{
				obstacle[i].GetComponent<Obsatcle_move>().enabled = false;				
			}
			Invoke("restartLevel", 1f);
			Invoke("stopTime", 2f);


		}
		
	}
	private void restartLevel()
	{
		GameObject.FindGameObjectWithTag("Player").SetActive(false);
		restartUi.SetActive(true);

	}
	private void stopTime()
	{
		Time.timeScale = 0;
	}

}
