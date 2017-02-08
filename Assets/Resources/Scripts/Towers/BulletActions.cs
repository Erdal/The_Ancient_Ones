using UnityEngine;
using System.Collections;

public class BulletActions : MonoBehaviour 
{
	public GameObject target; //Store targeted object
	public Vector3 startPosition; //Store starting point of bullet
	public Vector3 targetPosition; //Store targets position

	private float distance; //Store the distance left to travel
	private float startTime; //Store the time our bullet started to move
	private GamePlayController gamePlayManager; //Stores our GamePlayController
	[HideInInspector] //Hide from unity inspector
	public BasicStatsTowers basicStatsTowers; //Store the BasicStatsTowers script here to access towers stats, this will be given by the object that creates this bullet

	// Use this for initialization
	void Start() 
	{
		SetCompoinents ();  //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		gamePlayManager = GameObject.Find("GamePlayController").GetComponent<GamePlayController>(); //Access to GamePlayController script
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
				HealthBar healthBar = target.transform.FindChild ("HealthBar").gameObject.GetComponent<HealthBar> ();
				healthBar.currentHealth -= Mathf.Max ((basicStatsTowers.damage - target.GetComponent<BasicStatsEnemies>().armour), 0); //current unit health -= Tower damange - targets armour

				//If the units health bar is now 0 or below
				if (healthBar.currentHealth <= 0) 
				{
					Destroy (target); //Destroy target
					AudioSource audioSource = target.GetComponent<AudioSource> ();
					AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);
					gamePlayManager.Blood += target.GetComponent<BasicStatsEnemies>().bloodValue; //Add the blood value of the target killed to the total blood the player has in match
					gamePlayManager.XpBlood += target.GetComponent<BasicStatsEnemies> ().xpBloodValue; //Add the xp blood value of the target killed to the total xp blood the player has in match
					gamePlayManager.enemyUnitsLeft -= 1; //Take away a unit from the total
					gamePlayManager.unitsLeftLabel.text = "Units " + gamePlayManager.enemyUnitsLeft; //Reset the unit label to display units that are left
				}
			}
			Destroy (this.gameObject); //Destroy bullet
		}
	}
}
