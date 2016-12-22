using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints; //Waypoint array
    public GameObject enemyPrefab; //reference to the Enemy prefab

    public Wave[] waves; //All the waves we want
    public int timeBetweenWaves = 5; //How much time inbetween waves

    private GamePlayController gameManager;

    private float lastSpawnTime; //When last spawned
    private int enemiesSpawned = 0; //How many enemys spawned

    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnInterval = 2; //The time between enemies in the wave in seconds
        public int maxEnemies = 20; //quantity of enemies spawning in this wave
    }

    // Use this for initialization
    void Start()
    {
        lastSpawnTime = Time.time; //Current time
        gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave; //Get the index of the current wave
        if (currentWave < waves.Length) //Check if last wave
        {
            //calculate how much time passed since the last enemy spawn and whether it’s time to spawn an enemy. 
            float timeInterval = Time.time - lastSpawnTime;
            float spawnInterval = waves[currentWave].spawnInterval;
            //Here you consider two cases. If it’s the first enemy in the wave, you check whether timeInterval is bigger than timeBetweenWaves.
            //Otherwise, you check whether timeInterval is bigger than this wave’s spawnInterval. 
            //In either case, you make sure you haven’t spawned all the enemies for this wave.
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies)
            {
                //If necessary, spawn an enemy by instantiating a copy of enemyPrefab. You also increase the enemiesSpawned count.
                lastSpawnTime = Time.time; //Your about to spawn an emeny now so make it equell the current time
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab); //creating new enewmy for this wave
                newEnemy.GetComponent<MoveEnemies>().waypoints = waypoints;
                Animator anim = newEnemy.transform.GetChild(0).GetComponent<Animator>();
                anim.SetBool("Walk", true);
                enemiesSpawned++;
            }
            //You check the number of enemies on screen. If there are none and it was the last enemy in the wave you spawn the next wave
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemies") == null)
            {
                gameManager.Wave++; //Increase wave
                //gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f); //Give 10 of gold left as reward
                enemiesSpawned = 0; //Set back to 0
                lastSpawnTime = Time.time; //Set to current time
            }
        }
        else //Upon beating the last wave this runs the game won animation.
        {
            //gameManager.gameOver = true;
            //GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            //gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}
