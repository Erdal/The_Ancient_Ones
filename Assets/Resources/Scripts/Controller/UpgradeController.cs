﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour 
{
	public GameObject onHoverPanel; //Store our OnHoverPanel here
	public Text onHoverText; //Store our OnHoverText from our OnHoverPanel here
	
	Type prefTypeGamePreferences; //Used to store the GamePreferences class we wish to connect to using MethodInfo class
	Type prefTypeHoverDescription; //Used to store the HoverDescriptions class we wish to connect to using MethodInfo class
	string[] NamesOfUpgrades = {"BasicDamageIncrease", "BasicAttackSpeedIncrease", "BasicRangeIncrease", "BloodIncrease", "BloodXpIncrease"}; //Used to store the names of our upgrades
	
	// Use this for initialization
	void Start () 
	{
		SetCompoinents ();
	}

	void SetCompoinents()
	{
		prefTypeGamePreferences = typeof(GamePreferences); //Get type of class GamePreferences
		prefTypeHoverDescription = typeof(HoverDescriptions); //Get type of class HoverDescriptions
		UpgradeLevelLables(); //Set all our level tags so player knows what level each upgrade is at
	}

	//Set upgrade level tags
	void UpgradeLevelLables()
	{
		foreach (string name in NamesOfUpgrades) 
		{
			MethodInfo methodInfoGet = prefTypeGamePreferences.GetMethod ("Get" + name); //Get this method by name
			GameObject.Find(name).transform.FindChild("LevelLabel").GetComponent<Text>().text = methodInfoGet.Invoke (null, null).ToString();
		}
	}

	//Go to the World_Map scene
	public void WorldMap()
	{
		SceneManager.LoadScene("World_Map"); //Go to scene
	}

	public void OnHoverObjectDescription(string objectName)
	{
		MethodInfo methodInfoGet = prefTypeHoverDescription.GetMethod ("Get" + objectName + "Description"); //Get this method by name

		onHoverText.text = methodInfoGet.Invoke (null, null).ToString();
		onHoverPanel.gameObject.SetActive (true);
	}

	public void OffHoverObjectDescription()
	{
		onHoverPanel.gameObject.SetActive (false);
	}

	//Increase level of selected upgrade
	public void IncreaseLevel(string objectName)
	{
		IncreaseDecreaseLevel (objectName, true); //Increase level
	}

	//Decrease level of selected upgrade
	public void DecreaseLevel(string objectName)
	{
		IncreaseDecreaseLevel (objectName, false); //Decrease level
	}

	void IncreaseDecreaseLevel(string objectName, bool isPositive)
	{
		MethodInfo methodInfoGet = prefTypeGamePreferences.GetMethod ("Get" + objectName); //Get this method by name

		var tempValueHolder = methodInfoGet.Invoke (null, null); //Call method and chuck the return value into tempValueHolder
		int tempValue = Convert.ToInt32(tempValueHolder); //Convert tempValueHolder into a int and place in tempValue
		object[] parameters;

		if (isPositive == true) 
		{
			parameters = new object[] { tempValue + 1 }; //Our parameters that we plan to send which in this case we plan to increase by 1
		} 
		else if (isPositive == false) 
		{
			parameters = new object[] { tempValue - 1 }; //Our parameters that we plan to send which in this case we plan to decrease by 1
		} 
		else 
		{
			parameters = new object[]{ };
		}

		MethodInfo methodInfoSet = prefTypeGamePreferences.GetMethod ("Set" + objectName); //Get this method by name
		methodInfoSet.Invoke (null, parameters); //Call method and send parameter with it
		GameObject.Find(objectName).transform.FindChild("LevelLabel").GetComponent<Text>().text = methodInfoGet.Invoke (null, null).ToString();
	}
}