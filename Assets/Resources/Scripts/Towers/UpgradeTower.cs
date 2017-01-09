using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpgradeTower : MonoBehaviour 
{
	GamePlayController gamePlayManager; //Stores our GamePlayController

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
			gamePlayManager.chosenObjectsName = upgradeThisTowerOnCLick; //Save the name of the newly selected BuildSpot
			towerUpgradePanel.transform.position = GameObject.Find(gamePlayManager.chosenObjectsName).transform.position; //Moves our upgrade panel to the center of this object
			gamePlayManager.buildTowerPanel.SetActive(false); //Turn off the build panel if active anywhere
			towerUpgradePanel.SetActive (true); //Turn panel on
		}
	}

	void UpgradeThisTower()
	{
		if (gamePlayManager.Blood >= basicStatsTowers.costOfUpgrade && gamePlayManager.chosenObjectsName == upgradeThisTowerOnCLick) 
		{
			basicStatsTowers.UpgradeTower();
			gamePlayManager.Blood -= basicStatsTowers.costOfUpgrade;
			towerUpgradePanel.SetActive (false);
		} 
		else if(gamePlayManager.Blood < basicStatsTowers.costOfUpgrade && gamePlayManager.chosenObjectsName == upgradeThisTowerOnCLick)
		{
			StartCoroutine (gamePlayManager.GameStatusCoroutine ("Cant Upgrade"));
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
		gamePlayManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		basicStatsTowers = GameObject.Find(upgradeThisTowerOnCLick).GetComponent("BasicStatsTowers") as BasicStatsTowers; //We get the BasicStats script from this unit and attach it to our varable
		towerUpgradePanel = gamePlayManager.towerUpgradePanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		upgradeButton = gamePlayManager.upgradeButton; //Sets upgradeButton to the upgradeButton in GamePlayController, which is connected to the upgradeButton in scene
		upgradeButton.onClick.AddListener(() => {UpgradeThisTower();}); //Add a onclick method to this button for the UpgradeThisTower method
		fuseButton = gamePlayManager.fuseButton; //Sets fuseButton to the fuseButton in GamePlayController, which is connected to the fuseButton in scene
		sellButton = gamePlayManager.sellButton; //Sets sellButton to the sellButton in GamePlayController, which is connected to the sellButton in scene
	}


	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
