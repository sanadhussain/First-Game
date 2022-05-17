using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
	[Header("References")]
		public Text scoreUi;
		public Rigidbody rb;
		public GameObject restartUi;
		public Text coinUi;
		public Text multiplierUi;
		public Text coinText;
		public Text distanceText;
		public Text scoreText;
	[SerializeField] private GameObject buttonUi;
	
	[Header("Forces")]
		public float sideForce;
	private bool collided;
    private Touch touch;
	private int coin;
	private GameObject[] obstacle;
	private GameObject[] obstacle2;
	private int score;
	private int multiplier = 1;
	private int snappedBack = 0;
	private float maxSpeed = 25f;
	private float tileLength = 300f;
	private float spawnZ = 450f;
	private int lastPrefabIndex = 0;
	private string [] names = {"prefab1", "prefab2", "prefab3", "prefab4", "prefab5" };
	private float groundSpawn = 200f;
	private bool swipeMovement;

	public static PlayerMovement instance;

	#region singelton
	public void Awake()
	{
		instance = this;
	}
	#endregion
	#region  Getters Setters
	public int highScore()
	{
		return score;
	}
	public int getCoin()
	{
		return coin;
	}
	public int getDistance()
	{
		return (int)transform.position.z;
	}
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
		sideForce = UpdateUi.instance.getSensitivity();
		buttonUi.SetActive(false);
		if (UpdateUi.instance.getRadio() == "Swipe")
		{
			sideForce = sideForce / 10;
			swipeMovement = true;
		}
		else if (UpdateUi.instance.getRadio() == "Button")
		{
			swipeMovement = false;
		}
		Time.timeScale = 0;
	}

	// Update is called once per frame
	void Update()
	{

		updateScoreAndSpeed();
		
		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
		{
			Time.timeScale = 1;
		}

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
		if (transform.position.y < 0)
		{
			EndGame();
		}
	}

	private void FixedUpdate()
	{
		playerMove();
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
	private void OnCollisionEnter(Collision collision)
	{
		
		if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Obstacle2" || collision.transform.tag == "Obstacle3")
		{
			collided = true;
			EndGame();
		}
		
	}
	private void EndGame()
	{
		transform.GetComponent<PlayerMovement>().enabled = false;
		loadPlayer();
		resultUi();
		Invoke("restartLevel", 1f);
		Invoke("stopTime", 2f);
		Invoke("playerInactive", 2f);
		Invoke("deactivateObstacle", 2f);

		
		
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Coin" && !collided)
		{
			FindObjectOfType<AudioManager>().Play("CoinCollect");
			other.gameObject.SetActive(false);
			coin++;
			coinUi.text = coin.ToString();
		}
	}

	private void resultUi()
	{
		coinText.text = coin.ToString();
		distanceText.text = (score * 3).ToString();
		scoreText.text = score.ToString();

	}

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
	private void updateScoreAndSpeed()
	{
		
		score = (int)transform.position.z - 2;
		score += snappedBack;
		score = score / 3 + multiplier;
		if (score < 0)
			scoreUi.text = "0";
		else
			scoreUi.text = score.ToString("0");
		if (score > 100 * multiplier && multiplier <= 5)
		{
			maxSpeed += 2;
			multiplier++;
			multiplierUi.text = "x" + multiplier.ToString();
		}
		
	}
	private void deactivateObstacle()
	{
		obstacle = GameObject.FindGameObjectsWithTag("Obstacle");
		obstacle2 = GameObject.FindGameObjectsWithTag("Obstacle2");
		for (int i = 0; i < obstacle.Length; i++)
		{
			obstacle[i].GetComponent<Obsatcle_move>().enabled = false;
		}
		for (int i = 0; i < obstacle2.Length; i++)
		{
			obstacle2[i].GetComponent<Obsatcle_move>().enabled = false;
		}
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
