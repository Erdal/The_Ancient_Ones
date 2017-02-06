using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints; //Waypoint array
    public int timeBetweenWaves = 5; //How much time inbetween waves
	public float dangerRating; //Store the danger rating of the units for the first wave of this map
	public int firstWaveUnits; //Stores how large the first wave on this map should be

    private EnemyPrefabs enemyPrefabs; //To connect to the EnemyPrefabs class that stores our enemy prefabs list
	private GamePlayController gamePlayController; //Store GamePlayController script in here

    private float lastSpawnTime; //When last spawned
    private int enemiesSpawned = 0; //How many enemys spawned

	//GameStatusPanel panel stuff
	private GameObject gameStatusPanel; //store the GameStatusPanel
	private Button worldMapButton; //Store its button
	private Text winLossLabel; //Store its label


    public static List<Waves> waves = new List<Waves>();//Store all the waves we want

    // Use this for initialization
    void Start()
    {
		SetCompoinents ();
		waves.Clear (); //Destroy any waves left from any other map or attempt
        AddWaves(5); //We want 5 random waves
		CommitWaves();
    }

	//Set our varable compoinents
	void SetCompoinents()
	{
		lastSpawnTime = Time.time; //Current time
		enemyPrefabs = GameObject.Find("GameManagerController").GetComponent<EnemyPrefabs>();  //Connecting the enemyPrefabs to the EnemyPrefabs component
		gamePlayController = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Connecting the gameManager to the GamePlayController component

		gameStatusPanel = gamePlayController.gameStatusPanel; //Store our GameStatusPanel here
		worldMapButton = gamePlayController.worldMapButton; //Store WorldMapButton in here
		winLossLabel = gamePlayController.winLossLabel; //Store WinLossLabel in here
	}

    int pick; //Used to store the enemy prefab of next wave
	GameObject tempEnemyPrefab; //So we dont effect the actuelly prefab we will use this to create our enemys
    void AddWaves(int numberOfWaves)
    {
		gamePlayController.maxWaves = numberOfWaves; //Set the new max number of waves
		Waves.dangerPoints = dangerRating; //Set the new danger rating in the wave class
        //Go through and build all the waves
        for(int i = 0; i < numberOfWaves; i++)
        {
            pick = Random.Range(0, enemyPrefabs.enemyPrefabList.Count); //Which enemy prefab is picked
			tempEnemyPrefab = (enemyPrefabs.enemyPrefabList[pick]); //Make our temp a compy of our chosen prefab
			waves.Add(new Waves(tempEnemyPrefab, 2, firstWaveUnits)); //Create this wave. Enemy prefab, Spawn Interviel, max number of units, danger rating
			firstWaveUnits += 1; //Increase the amont of units for the next wave
        }
		dangerRating = Waves.dangerPoints; //set the new danger rating in this class from the wave class
    }

	void CommitWaves()
	{
		int currentWave = gamePlayController.Wave; //Get the index of the current wave
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
				newEnemy.GetComponent<BasicStatsEnemies>().health = waves[currentWave]._unitHealth; //Set health of unit
				newEnemy.GetComponent<BasicStatsEnemies>().speed = waves[currentWave]._unitSpeed; //Set speed of unit
				newEnemy.GetComponent<BasicStatsEnemies>().armour = waves [currentWave]._unitArmour; //Set armour of unit
				newEnemy.GetComponent<BasicStatsEnemies>().bloodValue = waves [currentWave]._unitBloodValue; //Set blood value of unit
				newEnemy.GetComponent<BasicStatsEnemies>().xpBloodValue = waves [currentWave]._unitXpBloodValue; //Set xp blod value of unit
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
				gamePlayController.Wave++; //Increase wave
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
			GameObject.Find ("GameManagerController").GetComponent<GameManagerController> ().CheckNewHighScore(GameObject.Find ("GamePlayController").GetComponent<GamePlayController> ().XpBlood);
			Time.timeScale = 0;
			//TODO: Finish this off
		}
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
