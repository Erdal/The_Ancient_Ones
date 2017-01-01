using UnityEngine;
using System.Collections;

public class BulletActions : MonoBehaviour 
{
	public GameObject target; //Store targeted object
	public Vector3 startPosition; //Store starting point of bullet
	public Vector3 targetPosition; //Store targets position

	private float distance; //Store the distance left to travel
	private float startTime; //Store the time our bullet started to move
	private GamePlayController gameManager; //Stores our GamePlayController
	private BasicStatsTowers basicStatsTowers; //Store the BasicStatsTowers script here to access towers stats

	// Use this for initialization
	void Start() 
	{
		SetCompoinents ();  //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gameManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
		basicStatsTowers = this.gameObject.GetComponent<BasicStatsTowers>(); //Acces to our towers stats script
		startTime = Time.time; //Set start time to current time since this starts right away
		distance = Vector3.Distance (startPosition, targetPosition); //Set our distance to what is between our two varables
	}
	
	// Update is called once per frame
	void Update() 
	{
		float timeInterval = Time.time - startTime; //How long its been
		gameObject.transform.position = Vector3.Lerp (startPosition, targetPosition, timeInterval * basicStatsTowers.attackSpeed / distance); //Interplate between start and end positions
		if (this.gameObject.transform.position.Equals (targetPosition)) 
		{
			if (target != null) 
			{
				
			}
			Destroy (this.gameObject);
		}
	}
}
