using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour 
{
	GamePlayController gameManager; //Stores our GamePlayController

	private GameObject buildTowerPanel; //Store our BuildTowerPanel in here from the GamePlayController
	private Button towerOneButton; //Store our towerOneButton in here from the GamePlayController
	GameObject chosenTower; //Used to store the tower the user chooses
	BasicStatsTowers basicStatsTowers; //Store the BasicStatsTowers script here to access towers stats

	//Called when object is clicked
	void OnMouseDown()
	{
		gameManager.chosenObjectsName = gameObject.name; //Save the name of the newly selected BuildSpot
		buildTowerPanel.transform.position = GameObject.Find(gameManager.chosenObjectsName).transform.position; //Moves our upgrade panel to the center of this object
		gameManager.towerUpgradePanel.SetActive(false); //Turn off the towerUpgradePanel if just incase it is open anywhere else
		buildTowerPanel.SetActive (true); //Turn panel on
		towerOneButton.onClick.AddListener(() => {OptionOne();}); //Add a onclick method to this button for the OptionOne method
		towerOneButton.GetComponentInChildren<Text>().text = "Tower One"; //Set text
	}

	//Tower option one
	void OptionOne()
	{
		if(gameManager.Blood >= 200 && chosenTower == null && gameManager.chosenObjectsName == gameObject.name)
		{
			buildTowerPanel.SetActive (false); //Turn panel off, this is for testing purposes
			chosenTower = (Resources.Load("Prefabs/Towers/TowerOne") as GameObject); //Load TowerOne prefab into chosenTower
			GameObject tempChosenTower = Instantiate(chosenTower); //Use this to create our tower so we dont make perminent changes to our prefabs during runtime
			GameObject useingObject = GameObject.Find(gameManager.chosenObjectsName);
			tempChosenTower.transform.position = new Vector3 (useingObject.transform.position.x, useingObject.transform.position.y, 1); //Set the towers position to the same position of the building spot (This gameObject)
			tempChosenTower.transform.rotation = GameObject.Find(gameManager.chosenObjectsName).transform.rotation; //Set the towers rotation to the same rotation of the building spot (This gameObject)
			tempChosenTower.name = "Tower_" + gameManager.currentNumberOfTowers; //Create new name for tower
			gameManager.currentNumberOfTowers++; //Increase teh current number of towers
			basicStatsTowers = tempChosenTower.GetComponent<BasicStatsTowers>(); //Grab the BasicStatsTowers of this object and store it
			basicStatsTowers.towerLevel = 1; //Change level of tower to 1
			basicStatsTowers.damage = 10; //Tower has an attack of 10
			basicStatsTowers.attackSpeed = 10; //Tower has an attack speed of 10
			basicStatsTowers.currentTowerValue = 200; //Towers current value
			gameManager.Blood = gameManager.Blood - 200; //Take away 200 blood for building this tower
			switchToUpgradeScript();
		}
	}

	//Turn off "PlaceTower" script and turn on "UpgradeTower" script
	void switchToUpgradeScript()
	{
		UpgradeTower tempUpgradeTower = gameObject.GetComponent<UpgradeTower> ();
		tempUpgradeTower.enabled = true;
		tempUpgradeTower.upgradeThisTowerOnCLick = "Tower_" + (gameManager.currentNumberOfTowers - 1);
		gameObject.GetComponent<PlaceTower> ().enabled = false;
	}
		

	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		buildTowerPanel = gameManager.buildTowerPanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		towerOneButton = gameManager.towerOneButton; //Sets optionOneButton to the optionOneButton in GamePlayController, which is connected to the OptionOneButton in scene
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
