using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	// Use this for initialization
	void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

	//Go to the World_Map scene
    public void WorldMap()
    {
        SceneManager.LoadScene("World_Map"); //Go to scene
    }
}
