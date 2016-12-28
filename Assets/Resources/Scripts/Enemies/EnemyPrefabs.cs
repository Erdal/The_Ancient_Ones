using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPrefabs : MonoBehaviour
{
    public List<GameObject> enemyPrefabList = new List<GameObject>(); //List of references to the Enemy prefabs

    void Start()
    {
		AllEnemies ();
    }

    public void AllEnemies()
    {
        //enemyPrefabList.Clear();
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/Cave_Man") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugOne") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugTwo") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BlueBird") as GameObject));
		enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/GreenBird") as GameObject));
    }

	//TODO: Create reginons and set the list accordingly to what each region can actuelly use and to what each prefab starting stats should be.
}
