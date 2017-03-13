using UnityEngine;
using System.Collections;
using System;

public class BasicStatsTowers : MonoBehaviour 
{ 
	//Almost none of this is correct, this is being done for now just to get other elements of the game working

	public string towerType; //Type of tower
	public int towerLevel; //Level of tower
	public float damage; //Towers damage
	public float attackSpeed; //Towers attack speed
	public float range; //Towers attack range

	public float currentTowerValue; //Store current money spent on tower
	public float costOfUpgrade; //Store the price of next tower upgrade
	public float sellValueOfTower; //Store the current sell value of tower

	public void UpgradeTower()
	{
		//If the cost of the next upgrade is incorrect (This should never happen. but its a fall back if it does)
		if(costOfUpgrade != (currentTowerValue + (150 - (GamePreferences.GetFuseBloodCostDecrease() * 5))))
		{
			costOfUpgrade = currentTowerValue + (150 - (GamePreferences.GetFuseBloodCostDecrease() * 5)); //Set new cost of upgrade
		}
		currentTowerValue += costOfUpgrade; //Set new value of current tower
		costOfUpgrade = currentTowerValue + (150 - (GamePreferences.GetFuseBloodCostDecrease() * 5)); //Set new cost of upgrade
		sellValueOfTower = currentTowerValue * 0.8f; //Only want the sell value to be 80% of the towers current value
		towerLevel++; //Increase tower level
		damage = damage * 2; //Double tower damage
		attackSpeed = attackSpeed * 2; //Double tower attack speed
		range = (float)Math.Round((range * 1.2f), 2); //Increase range by 50% and round to 2 decimal places
		gameObject.GetComponent<CircleCollider2D>().radius = range; //Sets the circle colliders range
	}
}
