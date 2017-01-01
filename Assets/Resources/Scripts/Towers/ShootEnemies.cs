﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour 
{
	public List<GameObject> enemiesInRange; //Store all enemies that are in range
	private float lastShotTime; //Here we store the time in whihc the last shot was fired
	private float fireRate; //Here we will store how long it take to fire the next bullet

	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		enemiesInRange = new List<GameObject>(); //Setting up the list
		lastShotTime = Time.time; //Set to the current time
		fireRate = 60/this.gameObject.GetComponent<BasicStatsTowers>().attackSpeed; //Calculate fire rate by divinding 60 by the attack speed of the tower to determin how many bullets this tower can fire per minute
	}
	
	// Update is called once per frame
	void Update() 
	{
		GameObject target = null; //Store the game object we wish to target in here, make it start as null

		float minimalEnemyDistance = float.MaxValue; //Start with maxium distance in the minimalEnemyDistance
		//Iterate over all enemies in range
		foreach (GameObject enemies in enemiesInRange) 
		{
			float distanceToGoal = enemies.GetComponent<MoveEnemies> ().DistanceToGoal (); //Get distance to goal
			//Make an enemy new target if its distance to the last way point is smaller then the current minium
			if (distanceToGoal < minimalEnemyDistance) 
			{
				target = enemies; //New target set
				minimalEnemyDistance = distanceToGoal; //New minimalEnemyDistance set
			}
		}

		if (target != null) 
		{
			if (Time.time - lastShotTime >= fireRate) 
			{
				
				lastShotTime = Time.time;
			}
		}
	}

	void TowerAttack(Collider2D target)
	{
		GameObject bulletPrefab = (Resources.Load("Prefabs/Towers/Attachments/Bullet1") as GameObject)
	}

	//Remove the nemy from enemiesInRange
	void OnEnemyDestroy(GameObject enemy)
	{
		enemiesInRange.Remove (enemy); //Remove targeted enemy from list
	}

	//When GameObject enters triggered
	void OnTriggerEnter2D(Collider2D other)
	{
		//If GameObject is one of the enemies
		if (other.gameObject.tag == "Enemies")
		{
			enemiesInRange.Add (other.gameObject); //Add to list
			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate> (); //Store the EnemyDestructionDelegate in del
			del.enemyDelegate += OnEnemyDestroy; //Add OnEnemyDestroy so that when enemy is killed out method is called, insuring that we are not targeting a destroyied object
		}
	}

	//When GameObject exit our trigger range
	void OnTriggerExit2D(Collider2D other)
	{
		//If GameObject is one of the enemies
		if (other.gameObject.tag.Equals ("Enemies")) 
		{
			enemiesInRange.Remove (other.gameObject); //Remove from list
			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate> (); //Store the EnemyDestructionDelegate in del
			del.enemyDelegate -= OnEnemyDestroy; //Remove OnEnemyDestroy, it is no longer needed
		}
	}
}
