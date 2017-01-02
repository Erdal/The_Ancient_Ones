using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour 
{
	public float maxHealth; //Store teh max health of this object
	public float currentHealth; //Store the value of the health as it currently is
	public float originalScale; //Store the original size of our health bar

	// Use this for initialization
	void Start() 
	{
		SetCompoinents (); //Set our varable compoinents
	}

	//Set our varable compoinents
	void SetCompoinents()
	{
		originalScale = gameObject.transform.localScale.x; //Get and store the size of the health bar
		maxHealth = this.transform.parent.GetComponent<BasicStatsEnemies> ().health; //Get the health of the mainparent to determine the health of this object
		currentHealth = maxHealth; //Current health starts off as the max health
	}
	
	// Update is called once per frame
	void Update() 
	{
		Vector3 tempScale = gameObject.transform.localScale; //Create a temporary scale for our healthbar and place the localScale of our health bar inside it
		tempScale.x = currentHealth / maxHealth * originalScale; //Make the x position equal the percentage left over of health
		gameObject.transform.localScale = tempScale; //Change the object itself to match our temporary object
	}
}
