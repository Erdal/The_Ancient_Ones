using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UpgradeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	//Go to the World_Map scene
	public void WorldMap()
	{
		SceneManager.LoadScene("World_Map"); //Go to scene
	}
}
