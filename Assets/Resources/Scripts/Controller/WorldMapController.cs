using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WorldMapController : MonoBehaviour 
{
	// Use this for initialization
	void Start() 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}

	public void Region1Map1()
	{
		SceneManager.LoadScene ("Testing_Stuff");
	}
}