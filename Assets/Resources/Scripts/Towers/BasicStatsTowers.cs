using UnityEngine;
using System.Collections;

public class BasicStatsTowers : MonoBehaviour 
{ 
	//Almost none of this is correct, this is being done for now just to get other elements of the game working

	public int towerLevel; //Level of tower
	public float damage; //Towers damage
	public float attackSpeed; //Towers attack speed
	public float range; //Towers attack range

	public float currentTowerValue; //Store current money spent on tower
	public float costOfUpgrade; //Store the price of next tower upgrade

	public void UpgradeTower()
	{
		costOfUpgrade = currentTowerValue + 100; //Set new cost of upgrade
		currentTowerValue = currentTowerValue + costOfUpgrade; //Set new value of current tower
		towerLevel++; //Increase tower level
		damage = damage * 2; //Double tower damage
		attackSpeed = attackSpeed * 2; //Double tower attack speed
		range = range * 1.2f; //Increase range by 50%
		gameObject.GetComponent<CircleCollider2D>().radius = range; //Sets the circle colliders range
	}
}
