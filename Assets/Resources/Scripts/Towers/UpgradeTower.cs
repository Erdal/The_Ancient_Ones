using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour 
{
	GamePlayController gamePlayController; //Stores our GamePlayController

	private GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here from the GamePlayController
	private Button upgradeButton; //Store our upgradeButton in here from the GamePlayController
	private Button fuseButton; ////Store our fuseButton in here from our GamePlayController
	private Button sellButton; ////Store our sellButton in here from our GamePlayController
	BasicStatsTowers basicStatsTowers; //Store the BasicStatsTowers script here to access towers stats
	public string upgradeThisTowerOnCLick; //Store the tower this script will upgrade

	void OnMouseDown()
	{
		if (gameObject.GetComponent<UpgradeTower>().enabled == true) 
		{
			gamePlayController.chosenObjectsName = upgradeThisTowerOnCLick; //Save the name of the newly selected BuildSpot
			towerUpgradePanel.transform.position = GameObject.Find(gamePlayController.chosenObjectsName).transform.position; //Moves our upgrade panel to the center of this object
			gamePlayController.buildTowerPanel.SetActive(false); //Turn off the build panel if active anywhere
			upgradeButton.GetComponentInChildren<Text>().text = "Level " + (basicStatsTowers.towerLevel + 1).ToString() + ": " + basicStatsTowers.costOfUpgrade.ToString(); //Show level and cost of next upgrade on upgrade button
			sellButton.GetComponentInChildren<Text>().text =  basicStatsTowers.sellValueOfTower.ToString(); //We want to only have 80% of the cost of the tower to be refunded
			towerUpgradePanel.SetActive (true); //Turn panel on

			upgradeButton.onClick.RemoveAllListeners (); //Remove listeners
			fuseButton.onClick.RemoveAllListeners (); //Remove listeners
			sellButton.onClick.RemoveAllListeners (); //Remove listeners

			SetCompoinents (); //Objects need to be reset
		}
	}

	//Used to upgrade tower
	void UpgradeThisTower()
	{
		//If user has enough blood and the script is connected to the right tower
		if (gamePlayController.Blood >= basicStatsTowers.costOfUpgrade && gamePlayController.chosenObjectsName == upgradeThisTowerOnCLick) 
		{
			gamePlayController.Blood -= basicStatsTowers.costOfUpgrade;
			basicStatsTowers.UpgradeTower();
			towerUpgradePanel.SetActive (false);
		} 
		else if(gamePlayController.Blood < basicStatsTowers.costOfUpgrade && gamePlayController.chosenObjectsName == upgradeThisTowerOnCLick)
		{
			StartCoroutine (gamePlayController.GameStatusCoroutine ("CAN'T UPGRADE. You need " + basicStatsTowers.costOfUpgrade + " blood to upgrade"));
		}
		upgradeButton.GetComponentInChildren<Text>().text = "Level " + (basicStatsTowers.towerLevel + 1).ToString() + ": " + basicStatsTowers.costOfUpgrade.ToString(); //Show level and cost of next upgrade on upgrade button
		sellButton.GetComponentInChildren<Text>().text =  basicStatsTowers.sellValueOfTower.ToString(); //We want to only have 80% of the cost of the tower to be refunded
	}

	//Fuse chosen tower
	void FuseTower()
	{
		Debug.Log ("1");
	}

	//Sell chosen tower
	void SellTower()
	{
		if (gamePlayController.chosenObjectsName == upgradeThisTowerOnCLick) 
		{
			gamePlayController.Blood += basicStatsTowers.sellValueOfTower; //Refund the sellValueTower to the player
			Destroy(GameObject.Find(upgradeThisTowerOnCLick)); //Destroy sold tower
			PlaceTower tempPlaceTower = gameObject.GetComponent<PlaceTower>(); //Store our PlaceTower script here
			tempPlaceTower.enabled = true; //Turn on script
			tempPlaceTower.chosenTower = null; //Delete the tower that was attached to this build spot
			upgradeThisTowerOnCLick = null; //Delete the tower that was attrached to the upgrade script
			gameObject.GetComponent<UpgradeTower>().enabled = false; //Turn off upgrade script
			gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("Sprites/Openspot"); //Give our BuildSpot a sprite again
			towerUpgradePanel.SetActive(false); //Turn off panel
		}
	}

	// Use this for initialization
	void Start () 
	{
		SetCompoinents ();
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gamePlayController = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		basicStatsTowers = GameObject.Find(upgradeThisTowerOnCLick).GetComponent("BasicStatsTowers") as BasicStatsTowers; //We get the BasicStats script from this unit and attach it to our varable
		towerUpgradePanel = gamePlayController.towerUpgradePanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		upgradeButton = gamePlayController.upgradeButton; //Sets upgradeButton to the upgradeButton in GamePlayController, which is connected to the upgradeButton in scene
		upgradeButton.GetComponentInChildren<Text>().text = "Upgrade: " + basicStatsTowers.costOfUpgrade.ToString(); //Show cost of next upgrade on upgrade button
		upgradeButton.onClick.AddListener(() => {UpgradeThisTower();}); //Add a onclick method to this button for the UpgradeThisTower method
		fuseButton = gamePlayController.fuseButton; //Sets fuseButton to the fuseButton in GamePlayController, which is connected to the fuseButton in scene
		fuseButton.onClick.AddListener(() => {FuseTower();}); //Add a onclick method to this button for the FuseTower method
		sellButton = gamePlayController.sellButton; //Sets sellButton to the sellButton in GamePlayController, which is connected to the sellButton in scene
		sellButton.GetComponentInChildren<Text>().text =  basicStatsTowers.sellValueOfTower.ToString(); //We want to only have 80% of the cost of the tower to be refunded
		sellButton.onClick.AddListener(() => {SellTower();}); //Add a onclick method to this button for the SellTower method
	}


	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
