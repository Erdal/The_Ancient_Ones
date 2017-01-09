using UnityEngine;
using System.Collections;

public class RegionConditions : MonoBehaviour
{
	private EnemyPrefabs enemyPrefabs; //To Store the EnemyPrefabs class

	// Use this for initialization
	void Start()
	{
		SetCompoinents ();
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		enemyPrefabs = gameObject.GetComponent<EnemyPrefabs> (); //Store the EnemtPrefabs script from this object into this varable
	}

    public void RegionOne()
    {
		enemyPrefabs.ChooseEnemyList (2); //Set region one enemy list
		enemyPrefabs.ChangeAllPrefabsStats("GreenBird", 25, 3, 0);
    }
}
