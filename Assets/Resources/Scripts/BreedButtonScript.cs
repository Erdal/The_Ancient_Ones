using UnityEngine;
using System.Collections;

public class BreedButtonScript : MonoBehaviour 
{
	public int waveNumber; //Store the wave this breed button works for
	int amountToBreedBy; //Store the amount of units we will breed for an enemy wave
	float costOfBreeding; //Store the cost of breeding the wave connected to this button

	private GamePlayController gamePlayController; //Store GamePlayController script in here

	//On start up this is called
	void Start()
	{
		gamePlayController = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Connecting the gameManager to the GamePlayController component
	}

	//Calculate cost
	void CalculateCostOfBreeding()
	{
		amountToBreedBy = 1;
		//TODO: Add an upgrade to effect the cost of CostOfBreeding then connect it to the line of code below (Maybe)
		costOfBreeding = amountToBreedBy * SpawnEnemy.waves [waveNumber]._unitBloodValue; //Cost of breeding will be the amount we can breed by mulitiplied by the blood value of a single unit in this wave
	}

	//Adding units if there is enough blood
	public void IncreaseUnitsInWave()
	{
		//TODO: Connect the amountToBreedBy varable with an upgrade
		CalculateCostOfBreeding(); //Calculate cost

		//If user has enough blood
		if (gamePlayController.Blood >= costOfBreeding) 
		{
			gamePlayController.Blood -= costOfBreeding; //Take away the cost of breeding from users blood
			SpawnEnemy.waves [waveNumber]._maxEnemies += amountToBreedBy; //Increase wave units total
			float tempExtraDangerRating = SpawnEnemy.waves [waveNumber]._unitBloodValue * 0.2f; //So here we want to grab 20% of the current danger rating (Danger rating should be the same number as blood value)
			Waves.instance.ChangingOldEnemyStats (waveNumber, tempExtraDangerRating); //We send through the unit we want to change and the extra damage rating we are adding
			gamePlayController.SetWaveScrollView(); //Re-set the buttons in our wave scroll view
		} 
		else 
		{
			StartCoroutine (gamePlayController.GameStatusCoroutine ("Can't afford to breed")); //Let the user know they couldnt breed the wave
		}
	}

	//On hover for breed buttons
	public void OnHoverObjectDescription(string objectName)
	{
		//TODO: connect this hover description for our breed buttons
	}

	//Off hover for breed buttons
	public void OffHoverObjectDescription()
	{
		//TODO: Turn off our hover panel
	}
}
