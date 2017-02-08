using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Reflection;
using System;

public class WorldMapController : MonoBehaviour 
{
	[HideInInspector]
	public GameObject gameManagerController; //Store our gameManagerController here

	//TopPanel elements
	public Text upgradeLabel; //Store UpgradeLabel label from the TopPanel

	string[] namesOfMaps = { "Map1_1" };
	Type prefTypeGamePreferences; //Used to store the GamePreferences class we wish to connect to using MethodInfo class
	
	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gameManagerController = GameObject.Find ("GameManagerController"); //Store the GameManagerController in here
		prefTypeGamePreferences = typeof(GamePreferences); //Get type of class GamePreferences
		upgradeLabel.text = "Level: " + GamePreferences.GetPlayerLevel().ToString(); //Set the text of this label to be the value of the players current level
		UpdateMapScores(); //Update all the map scores for the user to see
	}

	//Update all the map scores for the user to see
	void UpdateMapScores()
	{
		foreach (string map in namesOfMaps) 
		{
			MethodInfo methodInfoGet = prefTypeGamePreferences.GetMethod ("Get" + map + "Score"); //Get this method by name
			GameObject.Find(map).transform.FindChild("HighScore").GetComponent<Text>().text = methodInfoGet.Invoke (null, null).ToString();
		}
	}

	//Go to upgrade scene
	public void UpgradeScene()
	{
		SceneManager.LoadScene ("Upgrade_Scene"); //Go to scene
	}

	//Go to the correct scene for the correct map button being clicked
	public void RegionMapChoice(string RegionMapName)
	{
		gameManagerController.GetComponent<RegionConditions> ().RegionOne (); //Set the region one conditions
		gameManagerController.GetComponent<GameManagerController>().nameOfCurrentMap = RegionMapName;
		SceneManager.LoadScene (RegionMapName + "Scene"); //Go to scene
	}
}