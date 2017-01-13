using UnityEngine;
using System.Collections;

public static class HoverDescriptions
{
	//Upgrade Descriptions
	//
	public static string BasicDamageIncreaseDescription = "BasicDamageIncreaseDescription";
	public static string BasicAttackSpeedIncreaseDescription = "BasicAttackSpeedIncreaseDescription";
	public static string BasicRangeIncreaseDescription = "BasicRangeIncreaseDescription";
	public static string BloodIncreaseDescription = "BloodIncreaseDescription";
	public static string BloodXpIncreaseDescription = "BloodXpIncreaseDescription";

	//Upgrade Descriptions Getters and Setters
	//
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
}
