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
    

	private float touchBegan;
	private float touchEnded;
	private float maxSpeed = 25f;
	private float tileLength = 300f;
	private float spawnZ = 450f;
	private int lastPrefabIndex = 0;
	private string [] names = {"prefab1", "prefab2"};
	private float groundSpawn = 200f;
	public static PlayerMovement instance;
	// Start is called before the first frame update

	public void Awake()
	{
		instance = this;
	}


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

		if (transform.position.z > groundSpawn)
		{
			groundSpawn += groundSpawn + 100f;
			TileManager.instance.objectToSpawn(names[randomPrefabIndex()], calculateSwpanPosition());
		}
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector3(rb.velocity.x,
			rb.velocity.y,
			rb.velocity.z + maxSpeed * Time.deltaTime);
		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}
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
			//Invoke("stopTime", 2f);
			Invoke("playerInacative", 2.1f);


		}
		
	}
	public void setSpawn(float postion)
	{
		this.spawnZ = postion;
	}
	public void setGroundSpawn(float position)
	{
		this.groundSpawn = position;
	}
	private void restartLevel()
	{
		
		restartUi.SetActive(true);
		

	}
	private void playerInactive()
	{
		
		GameObject.FindGameObjectWithTag("Player").SetActive(false);
	}
	private void stopTime()
	{
		Time.timeScale = 0;
	}
	private Vector3 calculateSwpanPosition()
	{
		Vector3 pos = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		return pos;
	}
	private int randomPrefabIndex()
	{
		if (names.Length < 1)
			return 0;
		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex)
			randomIndex = Random.Range(0, names.Length);
		lastPrefabIndex = randomIndex;
		return randomIndex;
	}

}
