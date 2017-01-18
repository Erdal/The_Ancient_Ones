using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints; //Waypoint array
    public int timeBetweenWaves = 5; //How much time inbetween waves

    private EnemyPrefabs enemyPrefabs; //To connect to the EnemyPrefabs class that stores our enemy prefabs list
	private GamePlayController gamePlayManager; //Store GamePlayController script in here

    private float lastSpawnTime; //When last spawned
    private int enemiesSpawned = 0; //How many enemys spawned

	//GameStatusPanel panel stuff
	private GameObject gameStatusPanel; //store the GameStatusPanel
	private Button worldMapButton; //Store its button
	private Text winLossLabel; //Store its label


    List<Waves> waves = new List<Waves>();//Store all the waves we want

    // Use this for initialization
    void Start()
    {
		SetCompoinents ();
        AddWaves(5, 500); //We want 5 random waves
		CommitWaves();
    }

	//Set our varable compoinents
	void SetCompoinents()
	{
		lastSpawnTime = Time.time; //Current time
		enemyPrefabs = GameObject.Find("GameManagerController").GetComponent<EnemyPrefabs>();  //Connecting the enemyPrefabs to the EnemyPrefabs component
		gamePlayManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Connecting the gameManager to the GamePlayController component

		gameStatusPanel = gamePlayManager.gameStatusPanel; //Store our GameStatusPanel here
		worldMapButton = gamePlayManager.worldMapButton; //Store WorldMapButton in here
		winLossLabel = gamePlayManager.winLossLabel; //Store WinLossLabel in here
	}

    int pick; //Used to store the enemy prefab of next wave
	GameObject tempEnemyPrefab; //So we dont effect the actuelly prefab we will use this to create our enemys
    void AddWaves(int numberOfWaves, int dangerRating)
    {
		gamePlayManager.maxWaves = numberOfWaves;
        //Go through and build all the waves
        for(int i = 0; i < numberOfWaves; i++)
        {
            pick = Random.Range(0, enemyPrefabs.enemyPrefabList.Count); //Which enemy prefab is picked
			tempEnemyPrefab = enemyPrefabs.enemyPrefabList[pick]; //Make our temp a compy of our chosen prefab
			waves.Add(new Waves(tempEnemyPrefab, 2, 5, dangerRating)); //Create this wave. Enemy prefab, Spawn Interviel, max number of units, danger rating
        }
    }

	void CommitWaves()
	{
		int currentWave = gamePlayManager.Wave; //Get the index of the current wave
		if (currentWave < waves.Count) //Check if last wave
		{
			float timeInterval = Time.time - lastSpawnTime; //calculate how much time passed since the last enemy spawn 
			float spawnInterval = waves[currentWave]._spawnInterval; //Store spawn interbal time
			//Here you consider two cases. If it’s the first enemy in the wave, you check whether timeInterval is bigger than timeBetweenWaves.
			//Otherwise, you check whether timeInterval is bigger than this wave’s spawnInterval.
			if (enemiesSpawned == 0 && timeInterval > timeBetweenWaves || timeInterval > spawnInterval && enemiesSpawned < waves [currentWave]._maxEnemies) 
			{
				lastSpawnTime = Time.time; //Your about to spawn an emeny now so make it equell the current time
				GameObject newEnemy = (GameObject)Instantiate (waves [currentWave]._enemyPrefab); //creating new enewmy for this wave
				newEnemy.GetComponent<MoveEnemies> ().waypoints = waypoints; //Sets the correct waypoint to follow for new enemy
				Animator anim = newEnemy.transform.GetChild (0).GetComponent<Animator> (); //store animator of the sprite of the new enemy
				anim.SetBool ("Walk", true); //Set walk to true so enemy begins to walk
				enemiesSpawned++; //Update how many enemys have been spawned
				StartCoroutine (WaitCommitWaves ());
			}
			//enemiesSpawned == 0 && timeInterval > timeBetweenWaves || 
			//You check the number of enemies on screen. If there are none and it was the last enemy in the wave you spawn the next wave
			else if (enemiesSpawned == waves [currentWave]._maxEnemies && GameObject.FindGameObjectWithTag ("Enemies") == null) 
			{
				gamePlayManager.Wave++; //Increase wave
				enemiesSpawned = 0; //Set back to 0
				lastSpawnTime = Time.time; //Set to current time
				StartCoroutine (WaitCommitWaves ());
			} 
			else 
			{
				StartCoroutine (WaitCommitWaves());
			}
		}
		else
		{
			gameStatusPanel.SetActive (true);
			GameObject.Find("GameManagerController").GetComponent<GameManagerController>().CheckIfCanLevel(GameObject.Find ("GamePlayController").GetComponent<GamePlayController> ().XpBlood);
			Time.timeScale = 0;
			//TODO: Finish this off
		}
		Debug.Log ("6");
	}

	IEnumerator WaitCommitWaves()
	{
		yield return new WaitForSeconds(1);
		CommitWaves ();
	}

    // Update is called once per frame
    void Update()
    {

    }
}
