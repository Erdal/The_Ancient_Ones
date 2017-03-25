using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
	public static Waves instance;
	
	protected GameObject enemyPrefab; //Here we store the enemy prefab for this wave
	BasicStatsEnemies tempEnemyBasicStatsEnemies;
    protected float spawnInterval; //The time between enemies in the wave in seconds
    protected int maxEnemies; //quantity of enemies spawning in this wave
	public static float dangerPoints; //Danger points of units in wave

	//Here we will store the stats of the unit for each wave to efffect the prefab when we spawn for that wave
	private float unitHealth;
	private float unitSpeed;
	private float unitArmour;
	private float unitBloodValue;
	private float unitXpBloodValue;

    private int healthCost = 1; //Cost of health
    private int speedCost = 10; //Cost of speed
    private int armourCost = 5; //Cost of armour

	private EnemyPrefabs enemyPrefabs; //To Store the EnemyPrefabs class

	public Waves(GameObject EnemyPrefab, float SpawnInterval, int MaxEnemies)
    {
		MakeInstance ();

		enemyPrefab = EnemyPrefab;
        spawnInterval = SpawnInterval;
        maxEnemies = MaxEnemies;

		tempEnemyBasicStatsEnemies = enemyPrefab.GetComponent<BasicStatsEnemies> ();

		ChangingNewEnemyStats ();
		//SetWaveStats ();
    }

	void Awake()
	{
		MakeInstance ();
	}

	void MakeInstance()
	{
		if (instance == null) 
		{
			instance = this;
		}
	}

	//Set this waves stats for the unit for this wave
	void SetWaveStats()
	{
		//Set this wave postitions stats
		unitHealth = tempEnemyBasicStatsEnemies.health;
		unitSpeed = tempEnemyBasicStatsEnemies.speed;
		unitArmour = tempEnemyBasicStatsEnemies.armour;
		unitBloodValue = (tempEnemyBasicStatsEnemies.health + (tempEnemyBasicStatsEnemies.speed * 10) + (tempEnemyBasicStatsEnemies.armour * 5)) * ((GamePreferences.GetBloodIncrease() * 0.05f) + 1);
		unitXpBloodValue = ((tempEnemyBasicStatsEnemies.health / 10) + (tempEnemyBasicStatsEnemies.speed) + (tempEnemyBasicStatsEnemies.armour / 2)) * ((GamePreferences.GetBloodXpIncrease() * 0.05f) + 1);
	}

	//Spent danger points to change unit stats
	void ChangingNewEnemyStats()
    {
		Debug.Log (dangerPoints.ToString ());
		ChangingStats(true);
		dangerPoints = dangerPoints * 1.20f; //Increase the danger rate by 20% for the next match
		SetWaveStats ();
    }

	public void ChangingOldEnemyStats(int WaveNumber, float DangerRating)
	{
		enemyPrefab = SpawnEnemy.waves[WaveNumber]._enemyPrefab; //Make this the new enemy prefab
		dangerPoints = DangerRating; //Make this the new dangerPoints to spend
		tempEnemyBasicStatsEnemies = enemyPrefab.GetComponent<BasicStatsEnemies> (); //Grab the correct script to change
		tempEnemyBasicStatsEnemies.health = SpawnEnemy.waves[WaveNumber]._unitHealth; //Set the current health of this unit
		tempEnemyBasicStatsEnemies.speed = SpawnEnemy.waves[WaveNumber]._unitSpeed; //Set the current speed of this unit
		tempEnemyBasicStatsEnemies.armour = SpawnEnemy.waves[WaveNumber]._unitArmour;  //Set the current armour of this unit
		tempEnemyBasicStatsEnemies.bloodValue = SpawnEnemy.waves[WaveNumber]._unitBloodValue;  //Set the current bloodValue of this unit
		tempEnemyBasicStatsEnemies.xpBloodValue = SpawnEnemy.waves[WaveNumber]._unitXpBloodValue;  //Set the current xpBloodValue of this unit
		ChangingStats (false);

		//Set this wave new stats
		SpawnEnemy.waves[WaveNumber]._unitHealth = tempEnemyBasicStatsEnemies.health;
		SpawnEnemy.waves[WaveNumber]._unitSpeed = tempEnemyBasicStatsEnemies.speed;
		SpawnEnemy.waves[WaveNumber]._unitArmour = tempEnemyBasicStatsEnemies.armour;
		SpawnEnemy.waves[WaveNumber]._unitBloodValue = (tempEnemyBasicStatsEnemies.health + (tempEnemyBasicStatsEnemies.speed * 10) + (tempEnemyBasicStatsEnemies.armour * 5)) * ((GamePreferences.GetBloodIncrease() * 0.05f) + 1);
		SpawnEnemy.waves[WaveNumber]._unitXpBloodValue = ((tempEnemyBasicStatsEnemies.health / 10) + (tempEnemyBasicStatsEnemies.speed) + (tempEnemyBasicStatsEnemies.armour / 2)) * ((GamePreferences.GetBloodXpIncrease() * 0.05f) + 1);
	}

	void ChangingStats(bool NewWave)
	{
		float tempDangerPoints = dangerPoints;
		Debug.Log (tempDangerPoints.ToString () + " Nice");
		if (NewWave == true) 
		{
			enemyPrefabs = GameObject.Find("GameManagerController").GetComponent<EnemyPrefabs> (); //Store the EnemtPrefabs script from this object into this varable
			enemyPrefabs.ChangeAllPrefabsStats(5, 1, 0); //Changing all prefabs stats
			tempDangerPoints -= 15; //Take away points
		}

		while (tempDangerPoints > 0) 
		{
			int tempPick = Random.Range(0, 4); //Choose a stat to change
			//Change Health
			if (tempPick == 1 && tempDangerPoints > 0)
			{
				//Debug.Log ("Health1 " + tempEnemyBasicStatsEnemies.health);
				tempEnemyBasicStatsEnemies.health += 1;
				tempDangerPoints -= 1;
				//Debug.Log ("Health2 " + tempEnemyBasicStatsEnemies.health);
				//Debug.Log ("Health " + tempDangerPoints);
			}
			//Change Armour
			else if (tempPick == 2 && tempDangerPoints >= 5) 
			{
				//Debug.Log ("Armour1 " + tempEnemyBasicStatsEnemies.armour);
				tempEnemyBasicStatsEnemies.armour += 1;
				tempDangerPoints -= 5;
				//Debug.Log ("Armour2 " + tempEnemyBasicStatsEnemies.armour);
				//Debug.Log ("Armour " + tempDangerPoints);
			}
			//Change Speed
			else if (tempPick == 3 && tempDangerPoints >= 10) 
			{
				//Debug.Log ("Speed1 " + tempEnemyBasicStatsEnemies.speed);
				tempEnemyBasicStatsEnemies.speed += 1;
				tempDangerPoints -= 10;
				//Debug.Log ("Speed2 " + tempEnemyBasicStatsEnemies.speed);
				//Debug.Log ("Speed " + tempDangerPoints);
			}
		}
	}

    //Getter and Setter
    public GameObject _enemyPrefab
    {
        get { return enemyPrefab; }
        set { enemyPrefab = value; }
    }

    //Getter and Setter
    public float _spawnInterval
    {
        get { return spawnInterval; }
        set { spawnInterval = value; }
    }

    //Getter and Setter
    public int _maxEnemies
    {
        get { return maxEnemies; }
        set { maxEnemies = value; }
    }

	//Getter and Setter
	public float _unitHealth
	{
		get { return unitHealth; }
		set { unitHealth = value; }
	}

	//Getter and Setter
	public float _unitSpeed
	{
		get { return unitSpeed; }
		set { unitSpeed = value; }
	}

	//Getter and Setter
	public float _unitArmour
	{
		get { return unitArmour; }
		set { unitArmour = value; }
	}

	//Getter and Setter
	public float _unitBloodValue
	{
		get { return unitBloodValue; }
		set { unitBloodValue = value; }
	}

	//Getter and Setter
	public float _unitXpBloodValue
	{
		get { return unitXpBloodValue; }
		set { unitXpBloodValue = value; }
	}
}
