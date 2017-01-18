using UnityEngine;
using System.Collections;

public static class GamePreferences
{
	//Note: Due to there not being a Bool varable with PlayerPrefs i am using int instead. 1 means true, 0 means false
	
	//Player Prefered Settings
	//
	public static string BoolMusic = "BoolMusic";

	//Level
	public static string PlayerLevel = "PlayerLevel";
	public static string LeftOverExperence = "LeftOverExperence";
	public static string UnspentTons = "UnspentTons";
	public static string SpentTons = "SpentTons";
	public static string BloodTonsBought = "BloodTonsBought";

	//Upgrades
	//
	public static string BasicDamageIncrease = "BasicDamageIncrease";
	public static string BasicAttackSpeedIncrease = "BasicAttackSpeedIncrease";
	public static string BasicRangeIncrease = "BasicRangeIncrease";
	public static string BloodIncrease = "BloodIncrease";
	public static string BloodXpIncrease = "BloodXpIncrease";

	//Region/Map Won
	//
	public static string BoolMap1_1Won = "BoolMap1_1Won";

	//Region/Map Score
	//
	public static string Map1_1Score = "Map1_1Score";

	//Player Prefered Settings Getters and Setters
	//
	public static void SetBoolMusic(int yesNo)
	{
		PlayerPrefs.SetInt (GamePreferences.BoolMusic, yesNo);
	}

	public static int GetBoolMusic()
	{
		return PlayerPrefs.GetInt (GamePreferences.BoolMusic);
	}

	//Level
	//
	public static void SetPlayerLevel(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.PlayerLevel, level);
		//TODO: Set new description here when description is created
	}

	public static int GetPlayerLevel()
	{
		return PlayerPrefs.GetInt (GamePreferences.PlayerLevel);
	}

	public static void SetLeftOverExperence(float experence)
	{
		PlayerPrefs.SetFloat (GamePreferences.LeftOverExperence, experence);
		//TODO: Set new description here when description is created
	}

	public static float GetLeftOverExperence()
	{
		return PlayerPrefs.GetFloat (GamePreferences.LeftOverExperence);
	}

	public static void SetUnspentTons()
	{
		int tempCurrentLevel = GetPlayerLevel ();
		int tempBloodTons = GetBloodTonsBought ();
		int tempSpentPoints = GetSpentTons ();
		PlayerPrefs.SetInt (GamePreferences.UnspentTons, ((tempCurrentLevel * 5) + tempBloodTons) - tempSpentPoints);
		HoverDescriptions.SetUnspentTonsLabelDescription(); //Set the new description
	}

	public static int GetUnspentTons()
	{
		return PlayerPrefs.GetInt (GamePreferences.UnspentTons);
	}

	public static void SetSpentTons(int currentSpentPoints)
	{
		PlayerPrefs.SetInt (GamePreferences.SpentTons, currentSpentPoints);
		//TODO: Set new description here when description is created
	}

	public static int GetSpentTons()
	{
		return PlayerPrefs.GetInt (GamePreferences.SpentTons);
	}

	//Never re-set
	public static void SetBloodTonsBought(int additionalPoints)
	{
		int tempCurrentBloodTons = GetBloodTonsBought ();
		PlayerPrefs.SetInt (GamePreferences.BloodTonsBought, tempCurrentBloodTons + additionalPoints);
		//TODO: Set new description here when description is created
	}

	public static int GetBloodTonsBought()
	{
		return PlayerPrefs.GetInt (GamePreferences.BloodTonsBought);
	}

	//Upgrade Getters and Setters
	//
	public static void SetBasicDamageIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BasicDamageIncrease, level);
		HoverDescriptions.SetBasicDamageIncreaseDescription (); //Set the new description
	}

	public static int GetBasicDamageIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicDamageIncrease);
	}

	public static void SetBasicAttackSpeedIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BasicAttackSpeedIncrease, level);
		HoverDescriptions.SetBasicAttackSpeedIncreaseDescription (); //Set the new description
	}

	public static int GetBasicAttackSpeedIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicAttackSpeedIncrease);
	}

	public static void SetBasicRangeIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BasicRangeIncrease, level);
		HoverDescriptions.SetBasicRangeIncreaseDescription (); //Set the new description
	}

	public static int GetBasicRangeIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicRangeIncrease);
	}

	public static void SetBloodIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BloodIncrease, level);
		HoverDescriptions.SetBloodIncreaseDescription (); //Set the new description
	}

	public static int GetBloodIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BloodIncrease);
	}

	public static void SetBloodXpIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BloodXpIncrease, level);
		HoverDescriptions.SetBloodXpIncreaseDescription(); //Set the new description
	}

	public static int GetBloodXpIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BloodXpIncrease);
	}

	//Region/Map Won Getters and Setters
	//
	public static void SetBoolMap1_1Won(int yesNo)
	{
		PlayerPrefs.SetInt (GamePreferences.BoolMap1_1Won, yesNo);
		//TODO: Set new description here when description is created
	}

	public static int GetBoolMap1_1Won()
	{
		return PlayerPrefs.GetInt (GamePreferences.BoolMap1_1Won);
	}

	//Region/Map Score Getters and Setters
	//
	public static void SetMap1_1Score(float score)
	{
		PlayerPrefs.SetFloat (GamePreferences.Map1_1Score, score);
		//TODO: Set new description here when description is created
	}

	public static float GetMap1_1Score()
	{
		return PlayerPrefs.GetFloat (GamePreferences.Map1_1Score);
	}

	//Reset all preferences
	public static void ResetGame()
	{
		//Player Prefered Settings
		SetBoolMusic(1);

		//Level
		//
		SetPlayerLevel(0);
		SetLeftOverExperence (0);
		//SetBloodTonsBought (0); // Dont reset this
		SetUnspentTons();
		SetSpentTons (0);

		//Upgrades
		//
		SetBasicDamageIncrease(0);
		SetBasicAttackSpeedIncrease (0);
		SetBasicRangeIncrease (0);
		SetBloodIncrease (0);
		SetBloodXpIncrease (0);

		//Region/Map Won
		//
		SetBoolMap1_1Won(0);

		//Region/Map Score
		//
		SetMap1_1Score(0);
	}
}
