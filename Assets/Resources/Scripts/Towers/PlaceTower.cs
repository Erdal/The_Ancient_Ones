using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour 
{
	GamePlayController gameManager; //Stores our GamePlayController

	private GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here from the GamePlayController
	private Button optionOneButton; //Store our OptionOneButton in here from the GamePlayController

	//Called when object is clicked
	void OnMouseDown()
	{
		towerUpgradePanel.transform.position = gameObject.transform.position; //Moves our upgrade panel to the center of this object
		towerUpgradePanel.SetActive (true); //Turn panel on
		optionOneButton.onClick.AddListener(() => {EndThisf();}); //Add a onclick method to this button
	}

	void EndThisf()
	{
		towerUpgradePanel.SetActive (false); //Turn panel off, this is for testing purposes
	}
		

	// Use this for initialization
	void Start() 
	{
		SetCompoinents ();
	}

	void SetCompoinents()
	{
		gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		towerUpgradePanel = gameManager.towerUpgradePanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		optionOneButton = gameManager.optionOneButton; //Sets optionOneButton to the optionOneButton in GamePlayController, which is connected to the OptionOneButton in scene
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
