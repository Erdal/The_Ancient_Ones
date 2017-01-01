using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour 
{
	GamePlayController gameManager; //Stores our GamePlayController

	private GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here from the GamePlayController
	private Button optionOneButton; //Store our OptionOneButton in here from the GamePlayController
	GameObject chosenTower; //Used to store the tower the user chooses

	//Called when object is clicked
	void OnMouseDown()
	{
		gameManager.chosenObjectsName = gameObject.name;
		towerUpgradePanel.transform.position = GameObject.Find(gameManager.chosenObjectsName).transform.position; //Moves our upgrade panel to the center of this object
		towerUpgradePanel.SetActive (true); //Turn panel on
		optionOneButton.onClick.AddListener(() => {OptionOne();}); //Add a onclick method to this button for the OptionOne method
	}

	//Tower option one
	void OptionOne()
	{
		if(gameManager.Blood >= 200 && chosenTower == null && gameManager.chosenObjectsName == gameObject.name)
		{
			towerUpgradePanel.SetActive (false); //Turn panel off, this is for testing purposes
			chosenTower = Instantiate((Resources.Load("Prefabs/Towers/TowerOne") as GameObject)); //Load TowerOne prefab into chosenTower
			chosenTower.transform.position = GameObject.Find(gameManager.chosenObjectsName).transform.position; //Set the towers position to the same position of the building spot (This gameObject)
			chosenTower.transform.rotation = GameObject.Find(gameManager.chosenObjectsName).transform.rotation; //Set the towers rotation to the same rotation of the building spot (This gameObject)
			Destroy (GameObject.Find(gameManager.chosenObjectsName)); //Destroy this gameObject so that the building spot no longer exists
			gameManager.Blood = gameManager.Blood - 200;
		}
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
		towerUpgradePanel = gameManager.towerUpgradePanel; //Sets towerUpgradePanel to the towerUpgradePanel in GamePlayController, which is connected to the TowerUpgradePanel in scene
		optionOneButton = gameManager.optionOneButton; //Sets optionOneButton to the optionOneButton in GamePlayController, which is connected to the OptionOneButton in scene
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
