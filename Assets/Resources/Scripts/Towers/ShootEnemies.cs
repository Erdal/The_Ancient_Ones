using UnityEngine;
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
		fireRate = 60/this.gameObject.GetComponent<BasicStatsTowers>().attackSpeed; //Calculate fire rate by divinding 60 by the attack speed of the tower to determin how many bullets this tower can fire per minute
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

		//If target is not null
		if (target != null) 
		{
			//If we are ready to attack
			if (Time.time - lastShotTime >= fireRate) 
			{
				TowerAttack (target.GetComponent<Collider2D>()); //Call attack method and send through target collider2d
				lastShotTime = Time.time; //Reset lastShotTime to now since we have attacked again
			}

			Vector3 direction = gameObject.transform.position - target.transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI, new Vector3 (0, 0, 1));
		}
	}

	//Our tower attack
	void TowerAttack(Collider2D target)
	{
		GameObject bulletPrefab = (Resources.Load ("Prefabs/Towers/Attachments/Bullet3") as GameObject); //Prefab of our bullet
		//TODO: for testing purposes change the bullet prefab to the larger bullets on larger levels of tower
		Vector3 startPostition = this.gameObject.transform.position; //Set the position of this object, since the attack will start from here
		Vector3 targetPostition = target.transform.position; //Set the position of our target object, this is where the attack needs to go
		startPostition.z = bulletPrefab.transform.position.z; //Set z position to bulletPrefab, which we set inside of inspector
		targetPostition.z = bulletPrefab.transform.position.z; //Set z position to bulletPrefab, which we set inside of inspector

		GameObject newBullet = (GameObject)Instantiate (bulletPrefab); //Create a copy of our prefab
		newBullet.transform.position = startPostition; //Set starting position
		BulletActions bulletActions = newBullet.GetComponent<BulletActions>(); //Store BullActions component inside of bulletActions
		bulletActions.target = target.gameObject; //Set target for BulletActions Script
		bulletActions.startPosition = startPostition; //Set startPostition for BulletActions Script
		bulletActions.targetPosition = targetPostition; //Set targetPosition for BulletActions Script
		bulletActions.basicStatsTowers = this.gameObject.GetComponent<BasicStatsTowers>();
		AudioSource audioSource = this.gameObject.GetComponent<AudioSource> (); //Store the AudioSource component of this object
		audioSource.PlayOneShot (audioSource.clip); //Play the sound of our object
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
