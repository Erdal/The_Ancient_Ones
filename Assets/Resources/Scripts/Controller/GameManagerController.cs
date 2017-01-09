using UnityEngine;
using System.Collections;

public class GameManagerController : MonoBehaviour 
{
	public static GameManagerController instance;

	// Use this for initialization
	void Start() 
	{
	
	}
	                                                                                                                                                                                                                                                                                       
	void Awake()
	{
		MakeSingleton ();
	}

	void MakeSingleton()
	{
		if (instance != null) 
		{
			Destroy (gameObject); //Destroy the new object, we do not need it
		} 
		else 
		{
			instance = this; //Make the instnce of this class equel this current class
			DontDestroyOnLoad (gameObject); //Don't destroy object
		}
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}
}
