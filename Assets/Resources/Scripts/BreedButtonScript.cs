using UnityEngine;
using System.Collections;

public class BreedButtonScript : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnMouseEnter()
	{
		Debug.Log ("ENTER");
	}

	void OnMouseExit()
	{
		Debug.Log ("EXIT");
	}

	void OnMouseDown()
	{
		Debug.Log ("DOWN");
	}

	void OnMouseUp()
	{
		Debug.Log ("UP");
	}
}
