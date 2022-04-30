using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
	{
        public string tag;
        public GameObject[] prefab;
        public int size;
       
	}

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;
    private int lastPrefabIndex = 1;
    private float spawnz =140.0f;
    private float tileLength = 300f;
    private Vector3 prefabPostion;
    private int count;
    private Vector3 obstaclePostion;
    private Obsatcle_move [] obstacle;
    





    private void Start()
	{
        
      poolDictionary = new Dictionary<string, List<GameObject>>();

		
		foreach (Pool pool in pools)
		{
<<<<<<< Updated upstream
            List<GameObject> objectPool = new List<GameObject>();
			if (pool.tag == "ground")
			{
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject go = Instantiate(pool.prefab[i]);
                    go.SetActive(false);
                    go.transform.SetParent(transform);
                    objectPool.Add(go);
                }
=======
            Queue<GameObject> objectPool = new Queue<GameObject>();
			
            for (int i = 0; i < pool.size; i++)
            {
                GameObject go = Instantiate(pool.prefab);
                go.SetActive(false);
                objectPool.Enqueue(go);
>>>>>>> Stashed changes
            }
			
            poolDictionary.Add(pool.tag, objectPool);
		}
<<<<<<< Updated upstream
		for (int i = 0; i < pools[0].size; i++)
		{
            objectToSpawn("ground", true, i);
        }
       
            
            
=======
        pos = Vector3.forward * 150f;
        objectToSpawn("prefab1", pos);
		
    }
   
	public void objectToSpawn(string tag, Vector3 postion)
    {
>>>>>>> Stashed changes
       
        
        

    }
    private void Update()
    {
        
        prefabPostion = poolDictionary["ground"][lastPrefabIndex].GetComponentInChildren<GroundMove>().getTransform();
        if (prefabPostion.z < 0.1f)
		{
            
            objectToSpawn("ground");
            poolDictionary["ground"][lastPrefabIndex].GetComponentInChildren<GroundMove>().setTransform(new Vector3(prefabPostion.x, prefabPostion.y, 300f));
            

        }
	
    }
   
	public void objectToSpawn(string tag,bool first = false, int index = 0)
    {
		if (first == true)
		{
            GameObject spawn = poolDictionary[tag][index];
            spawn.transform.position = Vector3.forward * spawnz;
            spawn.SetActive(true);
            spawnz += tileLength;
		}
		else
		{
            GameObject spawn = poolDictionary[tag][randomGroundPrefabIndex()];
            spawn.transform.position = Vector3.forward * spawnz;
            spawn.SetActive(true);
            
        }
        
        

    }
    private int randomGroundPrefabIndex()
    {
        count = pools[0].prefab.Length;
        
        if ( count <= 1)
            return 0;
        int randomIdex = lastPrefabIndex;
        while (randomIdex == lastPrefabIndex)
            randomIdex = Random.Range(0, count);
        lastPrefabIndex = randomIdex;
        return randomIdex;
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
