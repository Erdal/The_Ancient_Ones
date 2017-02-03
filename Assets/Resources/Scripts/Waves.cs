using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
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

	public Waves(GameObject EnemyPrefab, float SpawnInterval, int MaxEnemies)
    {
		enemyPrefab = EnemyPrefab;
        spawnInterval = SpawnInterval;
        maxEnemies = MaxEnemies;

		tempEnemyBasicStatsEnemies = enemyPrefab.GetComponent<BasicStatsEnemies> ();

		ChangingEnemyStats ();
		SetWaveStats ();

		//Reset to basic stats for next generated wave
		tempEnemyBasicStatsEnemies.health = 5;
		tempEnemyBasicStatsEnemies.speed = 1;
		tempEnemyBasicStatsEnemies.armour = 0;
		tempEnemyBasicStatsEnemies.bloodValue = 0;
		tempEnemyBasicStatsEnemies.xpBloodValue = 0;
    }

	void SetWaveStats()
	{
		//Set this wave postitions stats
		unitHealth = tempEnemyBasicStatsEnemies.health;
		unitSpeed = tempEnemyBasicStatsEnemies.speed;
		unitArmour = tempEnemyBasicStatsEnemies.armour;
		unitBloodValue = (tempEnemyBasicStatsEnemies.health + (tempEnemyBasicStatsEnemies.speed * 10) + (tempEnemyBasicStatsEnemies.armour * 5)) * ((GamePreferences.GetBloodIncrease() * 0.05f) + 1);
		unitXpBloodValue = ((tempEnemyBasicStatsEnemies.health / 10) + (tempEnemyBasicStatsEnemies.speed) + (tempEnemyBasicStatsEnemies.armour / 2)) * ((GamePreferences.GetBloodXpIncrease() * 0.05f) + 1);
	}
		
	void ChangingEnemyStats()
    {
		float tempDangerPoints = dangerPoints;
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
