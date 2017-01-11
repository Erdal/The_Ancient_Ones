using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;

public class UpgradeController : MonoBehaviour 
{
	Type prefType;
	
	// Use this for initialization
	void Start () 
	{
		SetCompoinents ();
	}

	void SetCompoinents()
	{
		prefType = typeof(GamePreferences); //Get type of class
	}

	//Go to the World_Map scene
	public void WorldMap()
	{
		SceneManager.LoadScene("World_Map"); //Go to scene
	}

	//Increase level of selected upgrade
	public void IncreaseLevel(string objectName)
	{
		MethodInfo methodInfoGet = prefType.GetMethod ("Get" + objectName); //Get this method by name

		var tempValueHolder = methodInfoGet.Invoke (null, null); //Call method and chuck the return value into tempValueHolder
		int tempValue = Convert.ToInt32(tempValueHolder); //Convert tempValueHolder into a int and place in tempValue

		object[] parameters = new object[] { tempValue + 1 }; //Our parameters that we plan to send

		MethodInfo methodInfoSet = prefType.GetMethod ("Set" + objectName); //Get this method by name
		methodInfoSet.Invoke (null, parameters); //Call method and send parameter with it
		//Debug.Log(methodInfoGet.Invoke (null, null));
	}

	//Decrease level of selected upgrade
	public void DecreaseLevel()
	{
		
	}
}