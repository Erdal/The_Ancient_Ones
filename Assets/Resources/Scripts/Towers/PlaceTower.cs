using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour 
{
	GamePlayController gamePlayManager; //Stores our GamePlayController
	GameManagerController gameManagerController; //Store our GameManagerController script

	private GameObject buildTowerPanel; //Store our BuildTowerPanel in here from the GamePlayController
	private Button towerOneButton; //Store our towerOneButton in here from the GamePlayController
	private Button towerTwoButton; //Store our towerTwoButton in here from the GamePlayController
	private Button towerThreeButton; //Store our towerThreeButton in here from the GamePlayController
	public GameObject chosenTower; //Used to store the tower the user chooses
	BasicStatsTowers basicStatsTowers; //Store the BasicStatsTowers script here to access towers stats

	int clicksCounted; //Used for hack solution, preventing the auto click of a tower when place tower panel opens

	//Called when object is clicked
	void OnMouseDown()
	{
		gamePlayManager.chosenObjectsName = gameObject.name; //Save the name of the newly selected BuildSpot
		buildTowerPanel.transform.position = GameObject.Find(gamePlayManager.chosenObjectsName).transform.position; //Moves our upgrade panel to the center of this object
		gamePlayManager.towerUpgradePanel.SetActive(false); //Turn off the towerUpgradePanel if just incase it is open anywhere else
		buildTowerPanel.SetActive (true); //Turn panel on
		towerOneButton.onClick.RemoveAllListeners(); //Remove listeners
		towerTwoButton.onClick.RemoveAllListeners(); //Remove listeners
		towerThreeButton.onClick.RemoveAllListeners(); //Remove listeners
		towerOneButton.onClick.AddListener(() => {OptionOne();}); //Add a onclick method to this button for the OptionOne method
		towerOneButton.GetComponentInChildren<Text>().text = "Tower One: 200"; //Set text
		towerTwoButton.onClick.AddListener(() => {OptionTwo();}); //Add a onclick method to this button for the OptionTwo method
		towerTwoButton.GetComponentInChildren<Text>().text = "Tower Two: 250"; //Set text
		towerThreeButton.onClick.AddListener(() => {OptionThree();}); //Add a onclick method to this button for the OptionThree method
		towerThreeButton.GetComponentInChildren<Text>().text = "Tower Three: 300"; //Set text
	}

	//Tower option one
	void OptionOne()
	{
		if (gamePlayManager.Blood >= 200 && chosenTower == null && gamePlayManager.chosenObjectsName == gameObject.name && clicksCounted > 0) 
		{
			buildTowerPanel.SetActive (false); //Turn panel off, this is for testing purposes
			chosenTower = (Resources.Load ("Prefabs/Towers/TowerOne") as GameObject); //Load TowerOne prefab into chosenTower
			GameObject tempChosenTower = Instantiate (chosenTower); //Use this to create our tower so we dont make perminent changes to our prefabs during runtime
			GameObject useingObject = GameObject.Find (gamePlayManager.chosenObjectsName); //Store object we want to use for changing our new towers spot in scene
			tempChosenTower.transform.position = new Vector3 (useingObject.transform.position.x, useingObject.transform.position.y, 1); //Set the towers position to the same position of the building spot (This gameObject)
			tempChosenTower.transform.rotation = GameObject.Find (gamePlayManager.chosenObjectsName).transform.rotation; //Set the towers rotation to the same rotation of the building spot (This gameObject)
			tempChosenTower.name = "Tower_" + gamePlayManager.currentNumberOfTowers; //Create new name for tower
			gamePlayManager.currentNumberOfTowers++; //Increase the current number of towers
			basicStatsTowers = tempChosenTower.GetComponent<BasicStatsTowers> (); //Grab the BasicStatsTowers of this object and store it
			basicStatsTowers.towerType = "TowerOne"; //Type of tower
			basicStatsTowers.towerLevel = 1; //Change level of tower to 1
			gameManagerController.UpdateTowerPrefabs (tempChosenTower); //Update certain parts of the towers stats according to the current upgrades used
			basicStatsTowers.currentTowerValue = 200; //Towers current value
			basicStatsTowers.sellValueOfTower = 160; //Towers refund value
			basicStatsTowers.costOfUpgrade = 200 + (150 - (GamePreferences.GetFuseBloodCostDecrease () * 5)); //Cost of next upgrade
			gamePlayManager.Blood = gamePlayManager.Blood - 200; //Take away 200 blood for building this tower
			gameObject.GetComponent<SpriteRenderer> ().sprite = null;
			switchToUpgradeScript ();
			clicksCounted = 0;
		} 
		else if (gamePlayManager.Blood < 200 && gamePlayManager.chosenObjectsName == gameObject.name && clicksCounted > 0) 
		{
			StartCoroutine (gamePlayManager.GameStatusCoroutine ("Can't Build TowerOne"));
			clicksCounted = 0;
			//TODO: Mention reason you cant build this tower
		} 
		else 
		{
			clicksCounted++; //Increase by 1
		}
	}

	//Tower option two
	void OptionTwo()
	{
		if (gamePlayManager.Blood >= 250 && chosenTower == null && gamePlayManager.chosenObjectsName == gameObject.name && clicksCounted > 0) 
		{
			buildTowerPanel.SetActive (false); //Turn panel off, this is for testing purposes
			chosenTower = (Resources.Load ("Prefabs/Towers/TowerTwo") as GameObject); //Load TowerOne prefab into chosenTower
			GameObject tempChosenTower = Instantiate (chosenTower); //Use this to create our tower so we dont make perminent changes to our prefabs during runtime
			GameObject useingObject = GameObject.Find (gamePlayManager.chosenObjectsName); //Store object we want to use for changing our new towers spot in scene
			tempChosenTower.transform.position = new Vector3 (useingObject.transform.position.x, useingObject.transform.position.y, 1); //Set the towers position to the same position of the building spot (This gameObject)
			tempChosenTower.transform.rotation = GameObject.Find (gamePlayManager.chosenObjectsName).transform.rotation; //Set the towers rotation to the same rotation of the building spot (This gameObject)
			tempChosenTower.name = "Tower_" + gamePlayManager.currentNumberOfTowers; //Create new name for tower
			gamePlayManager.currentNumberOfTowers++; //Increase the current number of towers
			basicStatsTowers = tempChosenTower.GetComponent<BasicStatsTowers> (); //Grab the BasicStatsTowers of this object and store it
			basicStatsTowers.towerType = "TowerTwo"; //Type of tower
			basicStatsTowers.towerLevel = 1; //Change level of tower to 1
			gameManagerController.UpdateTowerPrefabs (tempChosenTower); //Update certain parts of the towers stats according to the current upgrades used
			basicStatsTowers.currentTowerValue = 250; //Towers current value
			basicStatsTowers.sellValueOfTower = 200; //Towers refund value
			basicStatsTowers.costOfUpgrade = 250 + (150 - (GamePreferences.GetFuseBloodCostDecrease() * 5)); //Cost of next upgrade
			gamePlayManager.Blood = gamePlayManager.Blood - 250; //Take away 200 blood for building this tower
			gameObject.GetComponent<SpriteRenderer> ().sprite = null;
			switchToUpgradeScript ();
			clicksCounted = 0;
		} 
		else if (gamePlayManager.Blood < 250 && gamePlayManager.chosenObjectsName == gameObject.name && clicksCounted > 0)
		{
			StartCoroutine (gamePlayManager.GameStatusCoroutine ("Can't Build TowerTwo"));
			clicksCounted = 0;
			//TODO: Mention reason you cant build this tower
		}
		else 
		{
			clicksCounted++; //Increase by 1
		}
			
	}

	//Tower option three
	void OptionThree()
	{
		if (gamePlayManager.Blood >= 300 && chosenTower == null && gamePlayManager.chosenObjectsName == gameObject.name && clicksCounted > 0) 
		{
			buildTowerPanel.SetActive (false); //Turn panel off, this is for testing purposes
			chosenTower = (Resources.Load ("Prefabs/Towers/TowerThree") as GameObject); //Load TowerOne prefab into chosenTower
			GameObject tempChosenTower = Instantiate (chosenTower); //Use this to create our tower so we dont make perminent changes to our prefabs during runtime
			GameObject useingObject = GameObject.Find (gamePlayManager.chosenObjectsName); //Store object we want to use for changing our new towers spot in scene
			tempChosenTower.transform.position = new Vector3 (useingObject.transform.position.x, useingObject.transform.position.y, 1); //Set the towers position to the same position of the building spot (This gameObject)
			tempChosenTower.transform.rotation = GameObject.Find (gamePlayManager.chosenObjectsName).transform.rotation; //Set the towers rotation to the same rotation of the building spot (This gameObject)
			tempChosenTower.name = "Tower_" + gamePlayManager.currentNumberOfTowers; //Create new name for tower
			gamePlayManager.currentNumberOfTowers++; //Increase the current number of towers
			basicStatsTowers = tempChosenTower.GetComponent<BasicStatsTowers> (); //Grab the BasicStatsTowers of this object and store it
			basicStatsTowers.towerType = "TowerThree"; //Type of tower
			basicStatsTowers.towerLevel = 1; //Change level of tower to 1
			gameManagerController.UpdateTowerPrefabs (tempChosenTower); //Update certain parts of the towers stats according to the current upgrades used
			basicStatsTowers.currentTowerValue = 300; //Towers current value
			basicStatsTowers.sellValueOfTower = 240; //Towers refund value
			basicStatsTowers.costOfUpgrade = 300 + (150 - (GamePreferences.GetFuseBloodCostDecrease() * 5)); //Cost of next upgrade
			gamePlayManager.Blood = gamePlayManager.Blood - 300; //Take away 200 blood for building this tower
			gameObject.GetComponent<SpriteRenderer> ().sprite = null;
			switchToUpgradeScript ();
			clicksCounted = 0;
		} 
		else if (gamePlayManager.Blood < 300 && gamePlayManager.chosenObjectsName == gameObject.name && clicksCounted > 0)
		{
			StartCoroutine (gamePlayManager.GameStatusCoroutine ("Can't Build TowerThree"));
			clicksCounted = 0;
			//TODO: Mention reason you cant build this tower
		}
		else 
		{
			clicksCounted++; //Increase by 1
		}
	}

	//Turn off "PlaceTower" script and turn on "UpgradeTower" script
	void switchToUpgradeScript()
	{
		UpgradeTower tempUpgradeTower = gameObject.GetComponent<UpgradeTower> (); //Store our UpgradeTower script here
		tempUpgradeTower.enabled = true; //Turn script on
		tempUpgradeTower.upgradeThisTowerOnCLick = "Tower_" + (gamePlayManager.currentNumberOfTowers - 1); //Set new towers name
		gameObject.GetComponent<PlaceTower> ().enabled = false; //Turn PlaceTower script off
	}
		

	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gameManagerController = GameObject.Find ("GameManagerController").GetComponent<GameManagerController> (); //Access to GameManagerController script
		gamePlayManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		buildTowerPanel = gamePlayManager.buildTowerPanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		towerOneButton = gamePlayManager.towerOneButton; //Sets optionOneButton to the optionOneButton in GamePlayController, which is connected to the OptionOneButton in scene
		towerTwoButton = gamePlayManager.towerTwoButton; //Sets optionTwoButton to the optionTwoButton in GamePlayController, which is connected to the OptionTwoButton in scene
		towerThreeButton = gamePlayManager.towerThreeButton; //Sets optionThreeButton to the optionThreeButton in GamePlayController, which is connected to the optionThreeButton in scene
		gameObject.GetComponent<UpgradeTower>().enabled = false; //Make sure at the start our UpgradeTower script is turned off
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
