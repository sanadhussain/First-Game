using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    // Class of user defined data type to implemt pooling
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;

    }
    public int coinCount = 26;
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    private Vector3 pos;
    private List<GameObject> obstacle;
    private List<Vector3> obstaclePostion;
    private List<string> coin;
    

    public static TileManager instance;
    #region singleton
    private void Awake()
    {
        instance = this;
    }
    #endregion


    public List<GameObject> getObstacle()
	{
        return obstacle;
	}
    public List<Vector3> getObstaclePostion()
    {
        return obstaclePostion;
    }

    private void Start()
	{
        obstacle = new List<GameObject>();
        obstaclePostion = new List<Vector3>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

		
		foreach (Pool pool in pools)
		{

            Queue<GameObject> objectPool = new Queue<GameObject>();
            GameObject[] tempObstacle = null;
            GameObject[] tempObstacle2 = null;
            GameObject[] tempObstacle3 = null;

            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                tempObstacle = GameObject.FindGameObjectsWithTag("Obstacle");
                tempObstacle2 = GameObject.FindGameObjectsWithTag("Obstacle2");
                tempObstacle3 = GameObject.FindGameObjectsWithTag("Obstacle3");
                if (tempObstacle.Length != 0)
                {
					foreach (GameObject obj in tempObstacle)
					{
                        obstacle.Add(obj);
					}
                }
                if (tempObstacle2.Length != 0)
                {
                    foreach (GameObject obj in tempObstacle2)
                    {
                        obstacle.Add(obj);
                    }
                }
                if (tempObstacle3.Length != 0)
                {
                    foreach (GameObject obj in tempObstacle3)
                    {
                        obstacle.Add(obj);
                    }
                }
				
                
                go.SetActive(false);
                objectPool.Enqueue(go);

            }
			
			
            poolDictionary.Add(pool.tag, objectPool);
		}

        
		
        pos = Vector3.forward * 150f;
        objectToSpawn("prefab1", pos);
		foreach (GameObject obj in obstacle)
		{
            obstaclePostion.Add(obj.transform.localPosition);
		}

    }
   
	public void objectToSpawn(string tag, Vector3 position)
    {
        coin = new List<string>();
		for (int i = 0; i <= coinCount; i++)
		{
            coin.Add("(" + i + ")");
            
		}
        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
         for (int i = 0; i < coin.Count; i++)
         {
	        try
	        {
                 obj.transform.Find("GoldCoin " + coin[i]).gameObject.SetActive(true);
            }
	        catch (System.Exception)
			{
                Debug.Log("GoldCoin Not Found");
            }
   
         }
        
		
        obj.transform.position = position;

        poolDictionary[tag].Enqueue(obj);

    }
    

    /*public GameObject[] tiles;
    

    private int lastPrefabIndex = 0;
    private List<GameObject> tilesList;
    private float spawnz = 150.0f;
    private float tileLength = 300.0f;
    private Transform player;
    private float safezone = 315.0f;

    private void Start()
	{
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tilesList = new List<GameObject>();

		for (int i = 0; i < tiles.Length; i++)
		{
            GameObject go = Instantiate(tiles[i]) as GameObject;
            go.transform.SetParent(transform);
            go.SetActive(false);
            tilesList.Add(go);
		}

       
        
	}
	
	private void Update()
	{
        if (player.position.z - safezone > (spawnz - 2 * tileLength))
        {
            spawnTile();
           
        }
    }
	private void spawnTile()
	{
        GameObject go = tilesList[randomPrefabIndex()];
        go.transform.position = Vector3.forward * spawnz;
        go.SetActive(true);
        spawnz += tileLength;
	}
    private int randomPrefabIndex()
    {
        if (tilesList.Count <= 1)
            return 0;
        int randomIdex = lastPrefabIndex;
        while (randomIdex == lastPrefabIndex)
            randomIdex = Random.Range(0,tilesList.Count);
        lastPrefabIndex = randomIdex;
        return randomIdex;
    }*/




    /*public GameObject[] tilePrefabs;

    private List<GameObject> activeTiles;
    
   
    
    private int amountOfTileOnScreen = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
       
        for(int i=0; i < amountOfTileOnScreen; i++)
		{
            spawnTile();
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (player.position.z - safezone > (spawnz - amountOfTileOnScreen * tileLength))
		{
            spawnTile();
            deleteTiles();
		}
    }

    private void spawnTile()
	{
        GameObject go;
        go = Instantiate(tilePrefabs[0]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnz;
        spawnz += tileLength;
        activeTiles.Add(go);

	}

    private void deleteTiles()
	{
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
	}*/
}
