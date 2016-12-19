using UnityEngine;
using System.Collections;

public class Movment : MonoBehaviour
{
    public float speed = 8f, maxVelocity = 4f;

    //The rigid body of what ever object we are effecting
    private Rigidbody2D myBody;

    //Use to controll animation
    private Animator anim;

    void Awake()
    {
        //Attaching the objects ridgidbody to this varable for future use
        myBody = GetComponent<Rigidbody2D>();
        //Attaching the objects Animator to this varable for future use
        anim = GetComponent<Animator>();
    }

    //Called every 3-4 frames, good movments
    void FixedUpdate()
    {
        
    }

    // Use this for initialization
    void Start()
    {
	
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
