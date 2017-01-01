using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints; //Waypoint array
    public int timeBetweenWaves = 5; //How much time inbetween waves

    private EnemyPrefabs enemyPrefabs; //To connect to the EnemyPrefabs class that stores our enemy prefabs list
    private GamePlayController gameManager;

    private float lastSpawnTime; //When last spawned
    private int enemiesSpawned = 0; //How many enemys spawned

    List<Waves> waves = new List<Waves>();//Store all the waves we want

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time; //Current time
		enemyPrefabs = GameObject.Find("GamePlayController").GetComponent<EnemyPrefabs>();
        gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Connecting the gameManager to the GamePlayController component
        AddWaves(5, 500); //We want 5 random waves
    }

    int pick; //Used to store the enemy prefab of next wave
    void AddWaves(int numberOfWaves, int dangerRating)
    {
        //Go through and build all the waves
        for(int i = 0; i < numberOfWaves; i++)
        {
            pick = Random.Range(0, enemyPrefabs.enemyPrefabList.Count); //Which enemy prefab is picked
			waves.Add(new Waves(enemyPrefabs.enemyPrefabList[pick], 2, 5, dangerRating)); //Create this wave. Enemy prefab, Spawn Interviel, max number of units, danger rating
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("5");
        int currentWave = gameManager.Wave; //Get the index of the current wave
        if (currentWave < waves.Count) //Check if last wave
        {
            //calculate how much time passed since the last enemy spawn and whether it’s time to spawn an enemy. 
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave]._spawnInterval;
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
                //gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f); //Give 10 of gold left as reward
                enemiesSpawned = 0; //Set back to 0
                lastSpawnTime = Time.time; //Set to current time
            }
        }
        else
        {
            //TODO: Do something here for when the user wins!
        }
    }
}
