using UnityEngine;
using System.Collections;

public static class GamePreferences
{
	//Player Prefered Settings
	//
	public static string BoolMusic = "BoolMusic";

	//Upgrades
	//
	public static string BasicDamageIncrease = "BasicDamageIncrease";
	public static string BasicAttackSpeedIncrease = "BasicAttackSpeedIncrease";
	public static string BasicRangeInscrease = "BasicRangeInscrease";
	public static string BloodIncrease = "BloodIncrease";
	public static string BloodXpIncrease = "BloodXpIncrease";

	//Region_Map_Score
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

	public static void SetBasicRangeInscrease(int level)
	{
		PlayerPrefs.SetInt (GamePreferences.BasicRangeInscrease, level);
	}

	public static int GetBasicRangeInscrease()
	{
		return PlayerPrefs.GetInt (GamePreferences.BasicRangeInscrease);
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

	//Region_Map_Score Getters and Setters
	//
	public static void SetRegionScore1_1(float score)
	{
		PlayerPrefs.SetFloat (GamePreferences.RegionScore1_1, score);
	}

	public static float GetRegionScore1_1()
	{
		return PlayerPrefs.GetFloat (GamePreferences.RegionScore1_1);
	}

	public static void ResetGame()
	{
		
	}
}
