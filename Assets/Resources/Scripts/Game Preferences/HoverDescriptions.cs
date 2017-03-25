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
	public static string BloodGainedValueIncreaseDescription = "BloodGainedValueIncreaseDescription";
	public static string BreedUnitAmountIncreaseDescription = "BreedUnitAmountIncreaseDescription";

	//GamePlay Scene Description
	//
	//Basic
	public static string BloodLabelDescription = "BloodLabelDescription";
	public static string XPBloodLabelDescription = "XPBloodLabelDescription";
	public static string WaveLabelDescription = "WaveLabelDescription";
	public static string UnitsLeftLabelDescription = "UnitsLeftLabelDescription";
	public static string LivesLabelDescription = "LivesLabelDescription";

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
		int tempCalculater = GamePreferences.GetBloodGainedValueIncrease() + 5;
		PlayerPrefs.SetString (HoverDescriptions.BloodGainedLabelDescription, "You gain " + tempCalculater + " points of blood for every unspent ton of blood you have at the begining of any map you play, with your current unspent points you will start with " + (tempCalculater * GamePreferences.GetUnspentTons()) + " at the beggining of your next game!");
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

	public static void SetBloodGainedValueIncreaseDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.BloodGainedValueIncreaseDescription, "You currently gain " + (5 + GamePreferences.GetBloodGainedValueIncrease())+ " blood points from each unspent upgrade point, this upgrade gives an extra point of blood for each point every upgrade");
	}

	public static string GetBloodGainedValueIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BloodGainedValueIncreaseDescription);
	}

	public static void SetBreedUnitAmountIncreaseDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.BreedUnitAmountIncreaseDescription, "Each upgrade increase the amount in which the units of a wave will increase everytime you breed it by 1. Every time you breed a wave it will add " + (1 + GamePreferences.GetBreedUnitAmountIncrease())+ " units to that wave as well as increase its stats." );
	}

	public static string GetBreedUnitAmountIncreaseDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BreedUnitAmountIncreaseDescription);
	}

	//GamePlay Scene Description Getters and Setters
	//
	//Basic
	public static void SetBloodLabelDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.BloodLabelDescription, "BloodLabelDescription");
	}

	public static string GetBloodLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.BloodLabelDescription);
	}

	public static void SetXPBloodLabelDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.XPBloodLabelDescription, "XPBloodLabelDescription");
	}

	public static string GetXPBloodLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.XPBloodLabelDescription);
	}

	public static void SetWaveLabelDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.WaveLabelDescription, "WaveLabelDescription");
	}

	public static string GetWaveLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.WaveLabelDescription);
	}

	public static void SetUnitsLeftLabelDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.UnitsLeftLabelDescription, "UnitsLeftLabelDescription");
	}

	public static string GetUnitsLeftLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.UnitsLeftLabelDescription);
	}

	public static void SetLivesLabelDescription()
	{
		PlayerPrefs.SetString (HoverDescriptions.LivesLabelDescription, "LivesLabelDescription");
	}

	public static string GetLivesLabelDescription()
	{
		return PlayerPrefs.GetString (HoverDescriptions.LivesLabelDescription);
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
		SetBloodGainedValueIncreaseDescription ();
		SetBreedUnitAmountIncreaseDescription ();

		//GamePlay Scene Description
		//
		//Basic
		SetBloodLabelDescription();
		SetXPBloodLabelDescription ();
		SetWaveLabelDescription ();
		SetUnitsLeftLabelDescription ();
		SetLivesLabelDescription ();
	}
}
