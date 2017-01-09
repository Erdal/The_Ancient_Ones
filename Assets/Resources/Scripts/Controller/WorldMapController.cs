using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldMapController : MonoBehaviour 
{
	public GameObject gameManagerController; //Store our gameManagerController here
	
	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gameManagerController = GameObject.Find ("GameManagerController"); //Store the GameManagerController in here
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}

	public void Region1Map1()
	{
		gameManagerController.GetComponent<RegionConditions> ().RegionOne (); //Set the region one conditions
		SceneManager.LoadScene ("Testing_Stuff"); //Go to scene
	}
}