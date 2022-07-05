using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Text scoreUi;
	[SerializeField] private Rigidbody rb;
	[SerializeField] private GameObject restartUi;
	[SerializeField] private Text coinUi;
	[SerializeField] private Text multiplierUi;
	[SerializeField] private Text coinText;
	[SerializeField] private Text distanceText;
	[SerializeField] private Text scoreText;
	[SerializeField] private ParticleSystem particel;
	[SerializeField] private Button boostBtn;
	[SerializeField] private GameObject buttonUi;
	[SerializeField] private Slider boostSlider;
	[SerializeField] private ParticleSystem wrapEffect;
	[SerializeField] private ParticleSystem wrapEffect1;
	[SerializeField] private GameObject screenTouch;


	[Header("Forces")]
	[SerializeField] private float maxSpeed = 25f;
	public float sideForce;
	private bool collided;
	private Touch touch;
	private int coin;
	private int score;
	private int multiplier = 1;
	private int snappedBack = 0;
	
	private float tileLength = 300f;
	private float spawnZ = 450f;
	private int lastPrefabIndex = 0;
	private int lastPrefabIndex1 = 0;
	private string[] names = { "prefab1", "prefab2", "prefab3", "prefab4", "prefab5" };
	private float groundSpawn = 200f;
	private bool swipeMovement;
	private bool obtacleIsTrigger;
	private bool wait;
	private List<GameObject> obstacle;
	private List<Vector3> obstaclePostion;
	private List<GameObject> triggerObstacle;
	public static PlayerMovement instance;
	private float currentVelocity;
	
	[HideInInspector] public bool play;
	[HideInInspector] public bool isBoosted;

	#region singelton
	public void Awake()
	{
		instance = this;
	}
	#endregion
	#region Setters
	public void setScore(int score)
	{
		this.snappedBack = score;
	}
	public void setSpawn(float postion)
	{
		this.spawnZ = postion;
	}
	public void setGroundSpawn(float position)
	{
		this.groundSpawn = position;
	}
	#endregion

	void Start()
    {
		Time.timeScale = 0;
		rb.centerOfMass = new Vector3(0f, -0.3f, -0.1f );
		sideForce = UpdateUi.instance.getSensitivity();
		buttonUi.SetActive(false);
		obtacleIsTrigger = false;
		obstacle = new List<GameObject>();
		obstaclePostion = new List<Vector3>();
		triggerObstacle = new List<GameObject>();
		
		
		play = true;
		isBoosted = false;

		// Checks what mode to use
		if (UpdateUi.instance.getRadio() == "Swipe")
		{
			sideForce = sideForce / 10;
			swipeMovement = true;
		}
		else if (UpdateUi.instance.getRadio() == "Button")
		{
			swipeMovement = false;
		}
		
	}

	// Update is called once per frame
	void Update()
	{
		// Waits for user to touch the screen first time to play game
		if ((Input.touchCount > 0 || Input.GetMouseButtonDown(0))&& play)
		{
			Time.timeScale = 1;
			screenTouch.SetActive(false);
		}
		updateScoreAndSpeed();

		#region boost
		// Changes status to not boosted when user spends all boost
		if (boostSlider.value == 0)
		{
			isBoosted = false;
		}
		// changes btn to iteractable when it reaches max
		if (boostSlider.value == 20)
			boostBtn.interactable = true;
		else
			boostBtn.interactable = false;
		// Changing camera postion and field of view when boosted
		if (isBoosted)
		{
			Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView,70f,0.1f);
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Mathf.MoveTowards(Camera.main.transform.position.y,2f,0.1f), Camera.main.transform.position.z);
			float angle = Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.x, 20f, ref currentVelocity, 0.1f);
			Camera.main.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
		}
		// Changing camera postion  and field of view back when boost fineshes
		if (!isBoosted)
		{
			Camera.main.fieldOfView = Mathf.MoveTowards(Camera.main.fieldOfView, 60f, 0.1f);
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Mathf.MoveTowards(Camera.main.transform.position.y, 4f, 0.1f), Camera.main.transform.position.z);
			float angle = Mathf.SmoothDampAngle(Camera.main.transform.eulerAngles.x, 30f, ref currentVelocity, 0.1f);
			Camera.main.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
		}
		// Plays wrap effect, Changes speed and obstacle properties
		if (isBoosted && !obtacleIsTrigger)
		{
			wrapEffect.Play();
			wrapEffect1.Play();
			maxSpeed += 20;
			obstacle = TileManager.instance.getObstacle();
			
			foreach (GameObject obj in obstacle)
			{

				obj.GetComponent<BoxCollider>().isTrigger = true;
				obj.GetComponent<Rigidbody>().useGravity = false;
			}
			obtacleIsTrigger = true;
		}
		// End wrap effect, Changes speed and obstacle properties back.
		if (!isBoosted && obtacleIsTrigger)
		{
			wrapEffect.Stop();
			wrapEffect1.Stop();
			maxSpeed -= 20;
			obstacle = TileManager.instance.getObstacle();
			obstaclePostion = TileManager.instance.getObstaclePostion();
			for (int i = 0; i < triggerObstacle.Count; i++)
			{
				for (int j = 0; j < obstacle.Count; j++)
				{
					if (obstacle[j].name == triggerObstacle[i].name)
					{
						triggerObstacle[i].SetActive(true);
						if (triggerObstacle[i].tag != "Obstacle3")
						{
							triggerObstacle[i].GetComponent<Obsatcle_move>().enabled = true;
						}
						triggerObstacle[i].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
						triggerObstacle[i].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
						triggerObstacle[i].transform.localPosition = obstaclePostion[j];
						
					}
				}
			}
			foreach (GameObject obj in obstacle)
			{
				obj.GetComponent<BoxCollider>().isTrigger = false;
				obj.GetComponent<Rigidbody>().useGravity = true;
			}
			obtacleIsTrigger = false;
			triggerObstacle.Clear();
		}
		#endregion

		#region spawning endless tiles
		if (transform.position.z > groundSpawn)
		{
			int temp = randomPrefabIndex();
			groundSpawn += groundSpawn + 100f;
			switch (names[temp])
			{
				case "prefab1":
					TileManager.instance.coinCount = 26;
					break;
				case "prefab2":
					TileManager.instance.coinCount = 26;
					break;
				case "prefab3":
					TileManager.instance.coinCount = 32;
					break;
				case "prefab4":
					TileManager.instance.coinCount = 38;
					break;
				case "prefab5":
					TileManager.instance.coinCount = 30;
					break;
				default:
					Debug.LogWarning("Name dose not exists in array");
					break;
			}
			TileManager.instance.objectToSpawn(names[temp], calculateSwpanPosition());
		}
		#endregion


		if (transform.position.y < 0)
		{
			EndGame();
		}
	}
	private void FixedUpdate()
	{
		playerMove();	
	}
	private void OnCollisionEnter(Collision collision)
	{
		
		if ((collision.transform.tag == "Obstacle" || collision.transform.tag == "Obstacle2" || collision.transform.tag == "Obstacle3") && !isBoosted)
		{
			collided = true;
			EndGame();
		}
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Coin" && !collided)
		{
			if (boostSlider.value < 20 && !isBoosted)
			{
				boostSlider.value += 1;
			}
			FindObjectOfType<AudioManager>().Play("CoinCollect");
			particel.Play();
			other.gameObject.SetActive(false);
			coin++;
			coinUi.text = coin.ToString();
			
		}
		if ((other.transform.tag == "Obstacle" ||other.transform.tag == "Obstacle2" || other.transform.tag == "Obstacle3") && isBoosted)
		{
			triggerObstacle.Add(other.gameObject);
			if (other.tag != "Obstacle3")
			{
				other.GetComponent<Obsatcle_move>().enabled = false;
			}
			other.GetComponent<Rigidbody>().velocity = new Vector3(0f, 100f, rb.velocity.z*2);
			StartCoroutine(disableTriggeredObstacle(other.gameObject));
		}
	}
	private void playerMove()
	{
		rb.velocity = new Vector3(rb.velocity.x,
			rb.velocity.y,
			rb.velocity.z + maxSpeed * Time.deltaTime);
		if (rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
		}
		
		swipeMove(swipeMovement);
		
	}
	private void swipeMove(bool swipe)
	{
		if (swipe)
		{
			if (Input.touchCount > 0)
			{
				touch = Input.GetTouch(0);

				if (touch.phase == TouchPhase.Moved)
				{
					rb.AddForce(touch.deltaPosition.x * Time.deltaTime * sideForce, 0f, 0f, ForceMode.VelocityChange);
				}
			}
		}
		else if (!swipe)
		{
			buttonUi.SetActive(true);
		}
		
	}

	// Setting values or Result screen
	private void resultUi()
	{
		coinText.text = coin.ToString();
		distanceText.text = (score * 3).ToString();
		scoreText.text = score.ToString();

	}
	// Saving progress of user
	private void loadPlayer()
	{
		PlayerData data = SaveSystem.loadData();

		if (score > data.highScore)
		{
			data.highScore = score;
		}
		if (coin > data.mostCoins)
		{
			data.mostCoins = coin;
		}
		int temp = data.coin;
		data.coin = temp += coin;
		int distanceTemp = data.distance;
		data.distance = distanceTemp += score * 3;

		SaveSystem.savePlayer(data.highScore, data.coin, data.distance, data.mostCoins, UpdateUi.instance.getMusic(), UpdateUi.instance.getVfx(), UpdateUi.instance.getSensitivity(), UpdateUi.instance.getRadio());
	}
	// Updating score on screen and speed
	private void updateScoreAndSpeed()
	{
		
		score = (int)transform.position.z - 2;
		score += snappedBack;
		score = score / 3 + multiplier;
		if (score < 0)
			scoreUi.text = "0";
		else
			scoreUi.text = score.ToString("0");
		if (score > 100 * multiplier && multiplier < 5)
		{
			maxSpeed += 2;
			multiplier++;
			multiplierUi.text = "x" + multiplier.ToString();
		}
		
	}
	// Implemnting end game
	public void EndGame()
	{
		transform.GetComponent<PlayerMovement>().enabled = false;
		loadPlayer();
		resultUi();
		Invoke("restartLevel", 1f);
		Invoke("stopTime", 2f);
		Invoke("playerInactive", 2f);
		Invoke("deactivateObstacle", 2f);



	}
	// Method invoked in EndGame 
	// Handels decativation of obstacles
	private void deactivateObstacle()
	{
		obstacle = TileManager.instance.getObstacle();
		foreach (GameObject obj in obstacle)
		{
			if (obj.tag == "obstacle" || obj.tag == "obstacle2")
			{
				obj.GetComponent<Obsatcle_move>().enabled = false;
			}
		}
	}
	// Method invoked in EndGame
	// Activates ui if user collided
	private void restartLevel()
	{
		restartUi.SetActive(true);
	}
	// Method invoked in EndGame
	// Sets player inactive
	private void playerInactive()
	{	
		GameObject.FindGameObjectWithTag("Player").SetActive(false);
	}
	// Method invoked in EndGame
	// Stops time
	private void stopTime()
	{
		Time.timeScale = 0;
	}
	// Method calculates spawn postion of next prefab
	private Vector3 calculateSwpanPosition()
	{
		Vector3 pos = Vector3.forward * spawnZ;
		spawnZ += tileLength;
		return pos;
	}
	// Used to get random index in specific range
	private int randomPrefabIndex()
	{
		if (names.Length < 1)
			return 0;
		int randomIndex = lastPrefabIndex;
		while (randomIndex == lastPrefabIndex || randomIndex == lastPrefabIndex1)
			randomIndex = Random.Range(0, names.Length);
		lastPrefabIndex1 = lastPrefabIndex;
		lastPrefabIndex = randomIndex;
		return randomIndex;
	}
	// Enum to spend boost when user intiates it
	public IEnumerator spendBoost()
	{
		while (boostSlider.value != 0 && !wait)
		{
			boostSlider.value -= 0.1f;
			yield return new WaitForSeconds(0.05f);	
		}
	}
	// Enum to disable obstacel when user colides with them while boosted
	IEnumerator disableTriggeredObstacle(GameObject trigger)
	{
		yield return new WaitForSeconds(1f);
		trigger.SetActive(false);
	}

}
