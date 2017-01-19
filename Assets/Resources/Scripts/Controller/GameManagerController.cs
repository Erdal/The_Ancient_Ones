using UnityEngine;
using System.Collections;
using System;
using System.Reflection;
using UnityEngine.UI;

public class GameManagerController : MonoBehaviour 
{
	public static GameManagerController instance;
	Type prefTypeGamePreferences; //Used to store the GamePreferences class we wish to connect to using MethodInfo class

	public string nameOfCurrentMap; //Here we streo teh name of the current map being playied by the user
	float experenceNeededToLevel = 0; //Store experences needed to level up

	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
		//GamePreferences.ResetGame();
		//GamePreferences.SetPlayerLevel (10);
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		HoverDescriptions.SetAllDescription (); //Set all hover descriptions for our objects 
		prefTypeGamePreferences = typeof(GamePreferences); //Get type of class GamePreferences
	}

	//Update Tower prefab
	public void UpdateTowerPrefabs(GameObject currentTower)
	{
		currentTower.GetComponent<BasicStatsTowers> ().damage = (((GamePreferences.GetBasicDamageIncrease () * 0.05f) + 1) * 10);  //Set damage
		currentTower.GetComponent<BasicStatsTowers> ().attackSpeed = (((GamePreferences.GetBasicAttackSpeedIncrease() * 0.05f) + 1) * 10); //Set attack Speed
		currentTower.GetComponent<BasicStatsTowers>().range = (((GamePreferences.GetBasicRangeIncrease () * 0.05f) + 1) * 2); //Set range
		currentTower.GetComponent<CircleCollider2D> ().radius = currentTower.GetComponent<BasicStatsTowers> ().range; //Make CircleColliders2D radius that of our towers range
	}

	//Check to see if the user has a new highscore
	public void CheckNewHighScore(float currentScore)
	{
		MethodInfo methodInfoGetGamePreferences = prefTypeGamePreferences.GetMethod ("Get" + nameOfCurrentMap + "Score"); //Get this method by name
		float tempHighScore = (float)methodInfoGetGamePreferences.Invoke (null, null);
		//Only if current score on map is greater then the last high score on the map
		if (currentScore > tempHighScore) 
		{
			CheckIfCanLevel (currentScore - tempHighScore); //Take the current score - the old score and what ever is left give to the user
			
			MethodInfo methodInfoSet = prefTypeGamePreferences.GetMethod ("Set" + nameOfCurrentMap + "Score"); //Get this method by name
			object[] parameters = new object[] {currentScore}; //Our parameters that we plan to send
			methodInfoSet.Invoke (null, parameters); //Call method and send parameter with it
		}
	}

	//Here we check if the user can level and level them if we can
	public void CheckIfCanLevel(float mapXP)
	{
		float tempCurrentXP = GamePreferences.GetLeftOverExperence (); //Current left over experence stored here
		float tempNewCurrentXP = tempCurrentXP + mapXP; //This is now how much xp the user has in total, mapXP is the xp they just gained from there last match
		NeededXPToLevel(); //Set experence needed for next level

		//Only if the current xp is greater or equal to the experence needed to level up
		if (tempNewCurrentXP >= experenceNeededToLevel) 
		{
			tempNewCurrentXP = tempNewCurrentXP - experenceNeededToLevel; //Take away the experence needed to level
			GamePreferences.SetPlayerLevel (GamePreferences.GetPlayerLevel () + 1); //Increase game level by 1
			GamePreferences.SetLeftOverExperence (tempNewCurrentXP); //Set left over XP to the LeftOverExperence in GamePreferences
			GamePreferences.SetUnspentTons(); //Set unspent points now that we have leveled again
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
