using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour 
{
	GamePlayController gameManager; //Stores our GamePlayController

	private GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here from the GamePlayController
	private Button upgradeButton; //Store our upgradeButton in here from the GamePlayController
	private Button fuseButton; ////Store our fuseButton in here from our GamePlayController
	private Button sellButton; ////Store our sellButton in here from our GamePlayController
	BasicStatsTowers basicStatsTowers; //Store the BasicStatsTowers script here to access towers stats
	public string upgradeThisTowerOnCLick; //Store the tower this script will upgrade

	void OnMouseDown()
	{
		gameManager.chosenObjectsName = upgradeThisTowerOnCLick; //Save the name of the newly selected BuildSpot
		towerUpgradePanel.transform.position = GameObject.Find(gameManager.chosenObjectsName).transform.position; //Moves our upgrade panel to the center of this object
		gameManager.buildTowerPanel.SetActive(false); //Turn off the build panel if active anywhere
		towerUpgradePanel.SetActive (true); //Turn panel on
	}

	void UpgradeThisTower()
	{
		if (gameManager.Blood >= basicStatsTowers.costOfUpgrade && gameManager.chosenObjectsName == upgradeThisTowerOnCLick) 
		{
			basicStatsTowers.UpgradeTower();
			gameManager.Blood -= basicStatsTowers.costOfUpgrade;
			towerUpgradePanel.SetActive (false);
		} 
		else if(gameManager.Blood < basicStatsTowers.costOfUpgrade && gameManager.chosenObjectsName == upgradeThisTowerOnCLick)
		{
			StartCoroutine (gameManager.GameStatusCoroutine ("Cant Upgrade"));
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
		gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		basicStatsTowers = GameObject.Find(upgradeThisTowerOnCLick).GetComponent("BasicStatsTowers") as BasicStatsTowers; //We get the BasicStats script from this unit and attach it to our varable
		towerUpgradePanel = gameManager.towerUpgradePanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		upgradeButton = gameManager.upgradeButton; //Sets upgradeButton to the upgradeButton in GamePlayController, which is connected to the upgradeButton in scene
		upgradeButton.onClick.AddListener(() => {UpgradeThisTower();}); //Add a onclick method to this button for the UpgradeThisTower method
		fuseButton = gameManager.fuseButton; //Sets fuseButton to the fuseButton in GamePlayController, which is connected to the fuseButton in scene
		sellButton = gameManager.sellButton; //Sets sellButton to the sellButton in GamePlayController, which is connected to the sellButton in scene
	}


	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
