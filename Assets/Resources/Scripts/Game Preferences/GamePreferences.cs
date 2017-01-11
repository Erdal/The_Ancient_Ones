using UnityEngine;
using System.Collections;

public static class GamePreferences
{
	//Player Prefered Settings

	public static string Music = "Music";


	//Upgrades

	//f


	//Region_Map_Score

	public static string RegionScore1_1 = "RegionScore1_1";



	//Region_Map_Score Getters and Setters

	public static void SetRegionScore1_1(float score)
	{
		PlayerPrefs.SetFloat (GamePreferences.RegionScore1_1, score);
	}

	public static float GetRegionScore1_1()
	{
		return PlayerPrefs.GetFloat (GamePreferences.RegionScore1_1);
	}
}
