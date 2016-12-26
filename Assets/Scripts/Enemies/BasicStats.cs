using UnityEngine;
using System.Collections;

public class BasicStats : MonoBehaviour
{
    public static BasicStats instance;

    public float health; //Enemy health
    public float speed; //Enemy speed
    public float armour; //Enemy armour

    public float GetSpeed()
    {
        return speed;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
