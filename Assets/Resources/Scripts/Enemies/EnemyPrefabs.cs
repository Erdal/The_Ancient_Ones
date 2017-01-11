using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPrefabs : MonoBehaviour
{
    public List<GameObject> enemyPrefabList = new List<GameObject>(); //List of references to the Enemy prefabs


    void Start()
    {
		
    }

	//Change a specific prefab
	public void ChangePrefabStats(string nameOfPrefab, float health, float speed, float armour)
	{
		int i = 0; //Counter
		//Check if correct prefab has been found, if not increase i and contiue to check
		while (enemyPrefabList [i].name != nameOfPrefab) 
		{
			i++; //Increase by 1
		}

		SetStatsPrefab (i, health, speed, armour);
	}

	//Change all prefabs
	public void ChangeAllPrefabsStats(float health, float speed, float armour)
	{
		for (int i = 0; i < enemyPrefabList.Count; i++) 
		{
			SetStatsPrefab (i, health, speed, armour);
		}
	}

	//Sets stats for prefab
	private void SetStatsPrefab(int i, float health, float speed, float armour)
	{
		enemyPrefabList [i].GetComponent<BasicStatsEnemies> ().health = health;
		enemyPrefabList [i].GetComponent<BasicStatsEnemies> ().speed = speed;
		enemyPrefabList [i].GetComponent<BasicStatsEnemies> ().armour = armour;
		enemyPrefabList [i].GetComponent<BasicStatsEnemies> ().bloodValue = health + speed + armour;
		enemyPrefabList [i].GetComponent<BasicStatsEnemies> ().xpBloodValue = (health / 10) + speed + (armour / 5);
	}

	//This method allows us to change our enemyPrefabList into what ever we need
	public void ChooseEnemyList(int choice)
	{
		//This switchstament is used to decide what enemy list we need
		switch(choice)
		{
			case 1:
				AllEnemies ();
				break;

			case 2:
				RegionOneList ();
				break;
		}
	}

	//Fill the enemyPrefabList with all units
    public void AllEnemies()
    {
        enemyPrefabList.Clear();
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/CaveMan") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugOne") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugTwo") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BlueBird") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/GreenBird") as GameObject));
    }

	//Fill the enemyPrefabList with all units avalable in region 1
	public void RegionOneList()
	{
		enemyPrefabList.Clear();
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/CaveMan") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugOne") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugTwo") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BlueBird") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/GreenBird") as GameObject));
	}

	//TODO: Create reginons and set the list accordingly to what each region can actuelly use and to what each prefab starting stats should be.
}
