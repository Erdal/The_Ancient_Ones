using UnityEngine;
using System.Collections;

public class GameManagerController : MonoBehaviour 
{
	public static GameManagerController instance;

	float experenceNeededToLevel = 0; //Store experences needed to level up

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
		currentTower.GetComponent<BasicStatsTowers> ().damage = (((tempMethodHolder * 0.05f) + 1) * 10);  //Set damage
		tempMethodHolder = GamePreferences.GetBasicAttackSpeedIncrease();
		currentTower.GetComponent<BasicStatsTowers> ().attackSpeed = (((tempMethodHolder * 0.05f) + 1) * 10);
		tempMethodHolder = GamePreferences.GetBasicRangeIncrease ();
		currentTower.GetComponent<BasicStatsTowers>().range = (((tempMethodHolder * 0.05f) + 1) * 2);
		currentTower.GetComponent<CircleCollider2D> ().radius = currentTower.GetComponent<BasicStatsTowers> ().range;
	}

	//Here we check if the user can level and level them if we can
	public void CheckIfCanLevel(float mapXP)
	{
		float tempCurrentXP = GamePreferences.GetLeftOverExperence (); //Current left over experence stored here
		float tempNewCurrentXP = tempCurrentXP + mapXP; //This is now how much xp the user has in total, mapXP is the xp they just gained from there last match
		NeededXPToLevel(); //Set experence needed for next level

		if (tempNewCurrentXP >= experenceNeededToLevel) 
		{
			tempNewCurrentXP = tempNewCurrentXP - experenceNeededToLevel; //Take away the experence needed to level
			GamePreferences.SetPlayerLevel (GamePreferences.GetPlayerLevel () + 1); //Increase game level by 1
			GamePreferences.SetLeftOverExperence (tempNewCurrentXP); //Set left over XP to the LeftOverExperence in GamePreferences
			NeededXPToLevel(); //Set how much XP is now needed to level
			CheckIfCanLevel (0); //Call this method again to see if we are done leveling
		} 
		else 
		{
			GamePreferences.SetLeftOverExperence (tempNewCurrentXP); //tempNewCurrentXP is now how much XP the user has left over, which we store back in the LeftOVerExperence in GamePreferences
		}
	}

	//How much experences is needed for next level
	public void NeededXPToLevel()
	{
		experenceNeededToLevel = 0; //Reset to 0
		int tempPlayerLevel = GamePreferences.GetPlayerLevel (); //Store player current level here
		int i = 0; //Counter
		while (i <= tempPlayerLevel) //While i is less then or equal too the level of the player
		{
			//If the player is only level 0 or 1
			if (i == 0 || i == 1) 
			{
				experenceNeededToLevel += 500; //Added 500 points to the needed experence
			} 
			else 
			{
				experenceNeededToLevel = ((experenceNeededToLevel + 1000) * 1.1f); //Add 1000 points to the current needed experence and then add an extra 10%
			}
			i++;
		}
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
