using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPrefabs : MonoBehaviour
{
    public List<GameObject> enemyPrefabList = new List<GameObject>(); //List of references to the Enemy prefabs

    void Start()
    {
        enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/Cave_Man") as GameObject));
        enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugOne") as GameObject));
        enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BugTwo") as GameObject));
        enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/BlueBird") as GameObject));
        enemyPrefabList.Add((Resources.Load("Prefabs/Enemies/GreenBird") as GameObject));
    }
}
