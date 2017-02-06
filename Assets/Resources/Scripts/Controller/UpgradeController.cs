using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour 
{
	//OnHoverPanel
	public GameObject onHoverPanel; //Store our OnHoverPanel here
	public Text onHoverText; //Store our OnHoverText from our OnHoverPanel here

	//Stats
	public GameObject statsTable; //Store our stats table in scene here
	public Text unspentTonsLabel; //Store out UnspentTonsLabel label from our stats table
	public Text bloodGainedLabel; //Store our BloodGainedLabel label from our stats table
	public Text currentLevelLabel; //Store our CurrentLevelLabel leabel from our stats table

	public Text currentStatusLabel; //Store the CurrentStatusLabel here
	
	Type prefTypeGamePreferences; //Used to store the GamePreferences class we wish to connect to using MethodInfo class
	Type prefTypeHoverDescription; //Used to store the HoverDescriptions class we wish to connect to using MethodInfo class
	string[] namesOfUpgrades = {"BasicDamageIncrease", "BasicAttackSpeedIncrease", "BasicRangeIncrease", "BloodIncrease", "BloodXpIncrease", "FuseBloodCostDecrease", "BloodGainedValueIncrease"}; //Used to store the names of our upgrades
	
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

		SetStatsLabels ();
	}

	void SetStatsLabels()
	{
		unspentTonsLabel.text = GamePreferences.GetUnspentTons ().ToString (); //Set our tag to show how many upgrade points are left
		bloodGainedLabel.text = ((GamePreferences.GetBloodGainedValueIncrease() + 5) * GamePreferences.GetUnspentTons()).ToString();
		currentLevelLabel.text = "Level: " + GamePreferences.GetPlayerLevel ().ToString ();
	}

	//Set upgrade level tags
	void UpgradeLevelLables()
	{
		foreach (string name in namesOfUpgrades) 
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
		onHoverText.text = methodInfoGet.Invoke (null, null).ToString(); //Invoke method and use its return as the text for onHoverText
		onHoverPanel.gameObject.SetActive (true); //Turn on our panel
	}

	public void OffHoverObjectDescription()
	{
		onHoverPanel.gameObject.SetActive (false);
	}

	//Increase level of selected upgrade
	public void IncreaseLevel(string objectName)
	{
		MethodInfo methodInfoGetGamePreferences = prefTypeGamePreferences.GetMethod ("Get" + objectName); //Get this method by name
		var tempValueHolder = methodInfoGetGamePreferences.Invoke (null, null); //Call method and chuck the return value into tempValueHolder

		//If the user wishes to upgrade a level 0 upgrade and has more then 0 tons of blood
		if ((int)tempValueHolder == 0 && GamePreferences.GetUnspentTons () > 0) 
		{
			IncreaseDecreaseLevel (objectName, true); //Increase level
			GamePreferences.SetSpentTons (GamePreferences.GetSpentTons () + 1);
		} 
		else if ((int)tempValueHolder > 0 && GamePreferences.GetUnspentTons () >= (int)tempValueHolder) 
		{
			IncreaseDecreaseLevel (objectName, true); //Increase level
			GamePreferences.SetSpentTons (GamePreferences.GetSpentTons () + (int)tempValueHolder);
		} 
		else 
		{
			StartCoroutine (StatusCoroutine ("You do not have the blood to upgrade, gather more from the killing fields"));
		}
		GamePreferences.SetUnspentTons ();
		SetStatsLabels ();
		OnHoverObjectDescription(objectName); //Show updated hover panel
	}

	//Decrease level of selected upgrade
	public void DecreaseLevel(string objectName)
	{
		MethodInfo methodInfoGetGamePreferences = prefTypeGamePreferences.GetMethod ("Get" + objectName); //Get this method by name
		var tempValueHolder = methodInfoGetGamePreferences.Invoke (null, null); //Call method and chuck the return value into tempValueHolder
		if ((int)tempValueHolder > 0) 
		{
			if ((int)tempValueHolder == 1) 
			{
				IncreaseDecreaseLevel (objectName, false); //Decrease level
				GamePreferences.SetSpentTons (GamePreferences.GetSpentTons () - 1);
			} 
			else if ((int)tempValueHolder > 1) 
			{
				IncreaseDecreaseLevel (objectName, false); //Decrease level
				GamePreferences.SetSpentTons (GamePreferences.GetSpentTons () - ((int)tempValueHolder - 1));
			}
			GamePreferences.SetUnspentTons ();
			SetStatsLabels ();
			OnHoverObjectDescription(objectName); //Show updated hover panel
		} 
		else 
		{
			StartCoroutine (StatusCoroutine ("Lowest level this upgrade can go, you cant downgrade anymore"));

		}
	}

	public IEnumerator StatusCoroutine(string message)
	{
		currentStatusLabel.text = message; //Change label text
		currentStatusLabel.gameObject.SetActive(true); //Activate label
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(2f)); //wait
		currentStatusLabel.gameObject.SetActive(false); //Deactivate label
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