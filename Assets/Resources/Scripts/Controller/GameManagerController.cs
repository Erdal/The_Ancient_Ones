using UnityEngine;
using System.Collections;

public class GameManagerController : MonoBehaviour 
{
	public static GameManagerController instance;

	// Use this for initialization
	void Start() 
	{
		SetCompoinents ();
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		HoverDescriptions.SetAllDescription ();
	}

	//Update Tower prefab
	public void UpdateTowerPrefabs(GameObject currentTower)
	{
		float tempMethodHolder = GamePreferences.GetBasicDamageIncrease ();
		currentTower.GetComponent<BasicStatsTowers> ().damage = (((tempMethodHolder * 5) / 100) * 10) + 10;  //Set damage
		tempMethodHolder = GamePreferences.GetBasicAttackSpeedIncrease();
		currentTower.GetComponent<BasicStatsTowers> ().attackSpeed = (((tempMethodHolder * 5) / 100) * 10) + 10;
		tempMethodHolder = GamePreferences.GetBasicRangeIncrease ();
		currentTower.GetComponent<BasicStatsTowers>().range = (((tempMethodHolder * 5) / 100) * 2) + 2;
		currentTower.GetComponent<CircleCollider2D> ().radius = currentTower.GetComponent<BasicStatsTowers> ().range;
	}
	                                                                                                                                                                                                                                                                                       
	void Awake()
	{
		MakeSingleton ();
	}

	void MakeSingleton()
	{
		if (instance != null) 
		{
			Destroy (gameObject); //Destroy the new object, we do not need it
		} 
		else 
		{
			instance = this; //Make the instnce of this class equel this current class
			DontDestroyOnLoad (gameObject); //Don't destroy object
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
