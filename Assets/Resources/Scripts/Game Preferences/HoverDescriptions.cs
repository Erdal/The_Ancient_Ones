using UnityEngine;
using System.Collections;

public static class HoverDescriptions
{
	//All Purpose Descriptions
	//
	public static string WorldMapButtonDescription = "WorldMapButtonDescription";

	//Upgrade_Scene Descriptions
	//
	//Basics
	public static string UnspentTonsLabelDescription = "UnspentPointsLabelDescription";
	public static string BloodGainedLabelDescription = "BloodGainedLabelDescription";
	//Upgrades
	public static string BasicDamageIncreaseDescription = "BasicDamageIncreaseDescription";
	public static string BasicAttackSpeedIncreaseDescription = "BasicAttackSpeedIncreaseDescription";
	public static string BasicRangeIncreaseDescription = "BasicRangeIncreaseDescription";
	public static string BloodIncreaseDescription = "BloodIncreaseDescription";
	public static string BloodXpIncreaseDescription = "BloodXpIncreaseDescription";
	public static string FuseBloodCostDecreaseDescription = "FuseBloodCostDecreaseDescription";

	//All Purpose Descriptions Getters and Setters
	//
	public static void SetWorldMapButtonDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.WorldMapButtonDescription, "The World Map button takes you back to the world map as you could have guessed");
	}

	public static string GetWorldMapButtonDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.WorldMapButtonDescription);
	}

	//Upgrade_Scene Descriptions Getters and Setters
	//
	//Basics
	public static void SetUnspentTonsLabelDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.UnspentTonsLabelDescription, "You current have " + GamePreferences.GetUnspentTons () + " tons of blood to spend on upgrades, choose carfully");
		SetBloodGainedLabelDescription ();
	}

	public static string GetUnspentTonsLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.UnspentTonsLabelDescription);
	}

	public static void SetBloodGainedLabelDescription()
	{
		int tempCalculater = (GamePreferences.GetUnspentTons() * 10);
		PlayerPrefs.SetString (HoverDescriptions.BloodGainedLabelDescription, "You gain 10 points of blood for every unspent ton of blood you have at the begining of any map you play, with your current unspent points you will start with " + tempCalculater + " at the beggining of your next game!");
	}

	public static string GetBloodGainedLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BloodGainedLabelDescription);
	}

	//Upgrades
	public static void SetBasicDamageIncreaseDescription()
	{
		int effect = GamePreferences.GetBasicDamageIncrease () * 5;
		PlayerPrefs.SetString (HoverDescriptions.BasicDamageIncreaseDescription, "Here we increase the basic damage of all towers by 5% each upgrade. Your current damage increase is " + effect + "%");
	}

	public static string GetBasicDamageIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BasicDamageIncreaseDescription);
	}

	public static void SetBasicAttackSpeedIncreaseDescription()
	{
		int effect = GamePreferences.GetBasicAttackSpeedIncrease () * 5;
		PlayerPrefs.SetString (HoverDescriptions.BasicAttackSpeedIncreaseDescription, "Here we increase the basic attack speed of all towers by 5% each upgrade. Your current attack speed increase is " + effect + "%");
	}

	public static string GetBasicAttackSpeedIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BasicAttackSpeedIncreaseDescription);
	}

	public static void SetBasicRangeIncreaseDescription()
	{
		int effect = GamePreferences.GetBasicRangeIncrease () * 5;
		PlayerPrefs.SetString (HoverDescriptions.BasicRangeIncreaseDescription, "Here we increase the basic range of all towers by 5% each upgrade. Your current range increase is " + effect + "%");
	}

	public static string GetBasicRangeIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BasicRangeIncreaseDescription);
	}

	public static void SetBloodIncreaseDescription()
	{
		int effect = GamePreferences.GetBloodIncrease () * 5;
		PlayerPrefs.SetString (HoverDescriptions.BloodIncreaseDescription, "Here we increase the blood gained from killing enemys by 5% each upgrade. Your current blood increase is " + effect + "%");
	}

	public static string GetBloodIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BloodIncreaseDescription);
	}

	public static void SetBloodXpIncreaseDescription()
	{
		int effect = GamePreferences.GetBloodXpIncrease () * 5;
		PlayerPrefs.SetString (HoverDescriptions.BloodXpIncreaseDescription, "Here we increase the blood gathered from the killing fields for the ancient one by 5% each upgrade. Your current blood xp increase is " + effect + "%");
	}

	public static string GetBloodXpIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BloodXpIncreaseDescription);
	}

	public static void SetFuseBloodCostDecreaseDescription()
	{
		int tempCost = 150 - (GamePreferences.GetFuseBloodCostDecrease() * 5);
		PlayerPrefs.SetString(HoverDescriptions.FuseBloodCostDecreaseDescription, "Everytime your tower is upgraded or fused it comes with a cost of 150 points of blood. This upgrade takes that cost down by 5 points each level up to a total of 20 levels. You currently pay " + tempCost);
	}

	public static string GetFuseBloodCostDecreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.FuseBloodCostDecreaseDescription);
	}

	//Set all descriptions
	public static void SetAllDescription()
	{
		//All Purpose Descriptions
		//
		SetWorldMapButtonDescription ();

		//Upgrade_Scene Descriptions
		//
		//Basics
		SetUnspentTonsLabelDescription();
		SetBloodGainedLabelDescription ();
		//Upgrades
		SetBasicDamageIncreaseDescription();
		SetBasicAttackSpeedIncreaseDescription();
		SetBasicRangeIncreaseDescription();
		SetBloodIncreaseDescription ();
		SetBloodXpIncreaseDescription ();
		SetFuseBloodCostDecreaseDescription ();
	}
}
