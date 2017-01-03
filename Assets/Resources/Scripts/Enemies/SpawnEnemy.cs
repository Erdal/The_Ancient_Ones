using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints; //Waypoint array
    public int timeBetweenWaves = 5; //How much time inbetween waves

    private EnemyPrefabs enemyPrefabs; //To connect to the EnemyPrefabs class that stores our enemy prefabs list
    private GamePlayController gameManager; //Store GamePlayController script in here

    private float lastSpawnTime; //When last spawned
    private int enemiesSpawned = 0; //How many enemys spawned

	private GameObject gameStatusPanel;
	private Button worldMapButton;
	private Text winLossLabel;


    List<Waves> waves = new List<Waves>();//Store all the waves we want

    // Use this for initialization
    void Start()
    {
		SetCompoinents ();
        AddWaves(5, 500); //We want 5 random waves
    }

	//Set our varable compoinents
	void SetCompoinents()
	{
		lastSpawnTime = Time.time; //Current time
		enemyPrefabs = GameObject.Find("GamePlayController").GetComponent<EnemyPrefabs>();  //Connecting the enemyPrefabs to the EnemyPrefabs component
		gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Connecting the gameManager to the GamePlayController component

		gameStatusPanel = gameManager.gameStatusPanel; //Store our GameStatusPanel here
		worldMapButton = gameManager.worldMapButton; //Store WorldMapButton in here
		winLossLabel = gameManager.winLossLabel; //Store WinLossLabel in here
	}

    int pick; //Used to store the enemy prefab of next wave
	GameObject tempEnemyPrefab; //So we dont effect the actuelly prefab we will use this to create our enemys
    void AddWaves(int numberOfWaves, int dangerRating)
    {
		gameManager.maxWaves = numberOfWaves;
        //Go through and build all the waves
        for(int i = 0; i < numberOfWaves; i++)
        {
            pick = Random.Range(0, enemyPrefabs.enemyPrefabList.Count); //Which enemy prefab is picked
			tempEnemyPrefab = enemyPrefabs.enemyPrefabList[pick]; //Make our temp a compy of our chosen prefab
			waves.Add(new Waves(tempEnemyPrefab, 2, 5, dangerRating)); //Create this wave. Enemy prefab, Spawn Interviel, max number of units, danger rating
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave; //Get the index of the current wave
        if (currentWave < waves.Count) //Check if last wave
        {
			float timeInterval = Time.time - lastSpawnTime; //calculate how much time passed since the last enemy spawn 
            float spawnInterval = waves[currentWave]._spawnInterval; //Store spawn interbal time
            //Here you consider two cases. If it’s the first enemy in the wave, you check whether timeInterval is bigger than timeBetweenWaves.
            //Otherwise, you check whether timeInterval is bigger than this wave’s spawnInterval. 
            //In either case, you make sure you haven’t spawned all the enemies for this wave.
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave]._maxEnemies)
            {
                //If necessary, spawn an enemy by instantiating a copy of enemyPrefab. You also increase the enemiesSpawned count.
                lastSpawnTime = Time.time; //Your about to spawn an emeny now so make it equell the current time
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave]._enemyPrefab); //creating new enewmy for this wave
                newEnemy.GetComponent<MoveEnemies>().waypoints = waypoints; //Sets the correct waypoint to follow for new enemy
                Animator anim = newEnemy.transform.GetChild(0).GetComponent<Animator>(); //store animator of the sprite of the new enemy
                anim.SetBool("Walk", true); //Set walk to true so enemy begins to walk
                enemiesSpawned++; //Update how many enemys have been spawned
            }
            //You check the number of enemies on screen. If there are none and it was the last enemy in the wave you spawn the next wave
            if (enemiesSpawned == waves[currentWave]._maxEnemies && GameObject.FindGameObjectWithTag("Enemies") == null)
            {
                gameManager.Wave++; //Increase wave
                enemiesSpawned = 0; //Set back to 0
                lastSpawnTime = Time.time; //Set to current time
            }
        }
        else
        {
			gameStatusPanel.SetActive (true);
            //TODO: Finish this off
        }
    }
}
