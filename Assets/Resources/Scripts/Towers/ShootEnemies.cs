using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour 
{
	public List<GameObject> enemiesInRange; //Store all enemies that are in range

	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	void SetCompoinents()
	{
		enemiesInRange = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		GameObject target = null; //Store the game object we wish to target in here

		float minimalEnemyDistance = float.MaxValue;
		foreach (GameObject enemies in enemiesInRange) 
		{ Debug.Log ("2");
			float distanceToGoal = enemies.GetComponent<MoveEnemies> ().DistanceToGoal ();
			if (distanceToGoal < minimalEnemyDistance) 
			{ Debug.Log ("3");
				target = enemies;
				minimalEnemyDistance = distanceToGoal;
			}
		}
	}

	void OnEnemyDestroy(GameObject enemy)
	{
		enemiesInRange.Remove (enemy);
		Debug.Log ("4");
	}

	void OnTriggerEnter2D(Collider2D other)
	{Debug.Log ("5");
		if (other.gameObject.tag == "Enemies") 
		{Debug.Log ("6");
			enemiesInRange.Add (other.gameObject);
			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate> ();
			del.enemyDelegate += OnEnemyDestroy;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{Debug.Log ("7");
		if (other.gameObject.tag.Equals ("Enemies")) 
		{Debug.Log ("8");
			enemiesInRange.Remove (other.gameObject);
			EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate> ();
			del.enemyDelegate -= OnEnemyDestroy;
		}
	}
}
