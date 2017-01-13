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
	public static string BoolRegionMapWon1_1 = "RegionMapWon1_1";

	//Region/Map Score
	//
	public static string RegionScore1_1 = "RegionScore1_1";

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
	public static void SetPlayerLevel(int yesNo)
	{
		if (yesNo == 1) 
		{
			int tempCurrentLevel = GetPlayerLevel ();
			PlayerPrefs.SetInt (GamePreferences.PlayerLevel, tempCurrentLevel + 1);
		}
		else if (yesNo == 0) 
		{
			PlayerPrefs.SetInt (GamePreferences.PlayerLevel, 0);
		} 
	}
	public static int GetPlayerLevel()
	{
		return PlayerPrefs.GetInt (GamePreferences.PlayerLevel);
	}

	public static void SetUnspentTons()
	{
		int tempCurrentLevel = GetPlayerLevel ();
		int tempBloodTons = GetBloodTonsBought ();
		int tempSpentPoints = GetSpentTons ();
		PlayerPrefs.SetInt (GamePreferences.UnspentTons, ((tempCurrentLevel * 5) + tempBloodTons) - tempSpentPoints);
	}

	public static int GetUnspentTons()
	{
		return PlayerPrefs.GetInt (GamePreferences.UnspentTons);
	}

	public static void SetSpentTons(int currentSpentPoints)
	{
		PlayerPrefs.SetInt (GamePreferences.SpentTons, currentSpentPoints);
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
	}

	public static int GetBasicDamageIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicDamageIncrease);
	}

	public static void SetBasicAttackSpeedIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BasicAttackSpeedIncrease, level);
	}

	public static int GetBasicAttackSpeedIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicAttackSpeedIncrease);
	}

	public static void SetBasicRangeIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BasicRangeIncrease, level);
	}

	public static int GetBasicRangeIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicRangeIncrease);
	}

	public static void SetBloodIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BloodIncrease, level);
	}

	public static int GetBloodIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BloodIncrease);
	}

	public static void SetBloodXpIncrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BloodXpIncrease, level);
	}

	public static int GetBloodXpIncrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BloodXpIncrease);
	}

	//Region/Map Won Getters and Setters
	//
	public static void SetBoolRegionMapWon1_1(int yesNo)
	{
		PlayerPrefs.SetInt (GamePreferences.BoolRegionMapWon1_1, yesNo);
	}

	public static int GetBoolRegionMapWon1_1()
	{
		return PlayerPrefs.GetInt (GamePreferences.BoolRegionMapWon1_1);
	}

	//Region/Map Score Getters and Setters
	//
	public static void SetRegionScore1_1(float score)
	{
		PlayerPrefs.SetFloat (GamePreferences.RegionScore1_1, score);
	}

	public static float GetRegionScore1_1()
	{
		return PlayerPrefs.GetFloat (GamePreferences.RegionScore1_1);
	}

	//Reset all preferences
	public static void ResetGame()
	{
		//Player Prefered Settings
		SetBoolMusic(1);

		//Level
		//
		SetPlayerLevel(0);
		SetBloodTonsBought (0);
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
		SetBoolRegionMapWon1_1(0);

		//Region/Map Score
		//
		SetRegionScore1_1(0);
	}
}
