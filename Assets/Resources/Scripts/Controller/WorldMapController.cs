﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMapController : MonoBehaviour 
{
	[HideInInspector]
	public GameObject gameManagerController; //Store our gameManagerController here

	//TopPanel elements
	public Text upgradeLabel; //Store UpgradeLabel label from the TopPanel

	string[] NamesOfMaps = { "Map1.1Button" };
	
	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gameManagerController = GameObject.Find ("GameManagerController"); //Store the GameManagerController in here
		upgradeLabel.text = GamePreferences.GetPlayerLevel().ToString(); //Set the text of this label to be the value of the players current level
	}

	void UpdateMapScores()
	{
		
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
		SceneManager.LoadScene (RegionMapName + "Scene"); //Go to scene
	}
}