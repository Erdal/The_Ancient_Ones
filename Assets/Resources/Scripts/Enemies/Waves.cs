﻿using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
    protected GameObject enemyPrefab; //Here we store the enemy prefab for this wave
    protected float spawnInterval; //The time between enemies in the wave in seconds
    protected int maxEnemies; //quantity of enemies spawning in this wave
    protected int dangerPoints;

    private int healthCost = 1; //Cost of health
    private int speedCost = 10; //Cost of speed
    private int armourCost = 5; //Cost of armour

	public Waves(GameObject EnemyPrefab, float SpawnInterval, int MaxEnemies, int DangerPoints)
    {
        enemyPrefab = EnemyPrefab;
        spawnInterval = SpawnInterval;
        maxEnemies = MaxEnemies;
        dangerPoints = DangerPoints;
        //ChangingStats();
    }

	void ChangingStats(int MaxEnemies, int DangerPoints)
    {
        //enemyPrefab.GetComponent<BasicStats>().health = 195;
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

    public int _dangerPoints
    {
        get { return dangerPoints; }
        set { dangerPoints = value; }
    }
}
