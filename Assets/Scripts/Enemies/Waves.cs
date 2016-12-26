using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
    protected GameObject enemyPrefab; //Here we store the enemy prefab for this wave
    protected float spawnInterval; //The time between enemies in the wave in seconds
    protected int maxEnemies; //quantity of enemies spawning in this wave

    public Waves(GameObject EnemyPrefab, float SpawnInterval, int MaxEnemies)
    {
        enemyPrefab = EnemyPrefab;
        spawnInterval = SpawnInterval;
        maxEnemies = MaxEnemies;
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
}
