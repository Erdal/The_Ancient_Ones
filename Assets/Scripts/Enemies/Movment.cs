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
        PlayerMoveWithKeyboard();
    }

    //Move object by keyboard
    void PlayerMoveWithKeyboard()
    {
        float velX = Mathf.Abs(myBody.velocity.x);
        float velY = Mathf.Abs(myBody.velocity.y);

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //If not 0 then that means player is moving Horizontal
        if (x != 0)
        {
            //Information we need to perform our task
            MovmentForPlayer("Horizontal", velX, 0f, "x");
        }
        else if(y != 0) //If not 0 then that means player is moving Vertical
        {
            //Information we need to perform our task
            MovmentForPlayer("Vertical", velY, 0f, "y");
        }
    }

    //Movement for all 4 directions
    void MovmentForPlayer(string vOrH, float velocity, float force, string xOrY)
    {
        float current = Input.GetAxisRaw(vOrH); //We set if it is Horizontal or Vertical

        if (current > 0) //If unit is going right or up
        {
            //Making sure we have not reached max speed
            if (velocity < maxVelocity)
            {
                force = speed; //increase speed
            }

            Vector3 temp = transform.localScale; //Our temp
            if(xOrY == "x") //If x then increase temp.x
            {
                temp.x = 1f;
            }
            else //If y then increase temp.y
            {
                temp.y = 1f;
            }
            transform.localScale = temp; //Assign back 

            anim.SetBool("Walk", true);
        }
        else if (current < 0) //If unit is going left or down
        {
            if (velocity < maxVelocity)
            {
                force = -speed;
            }

            Vector3 temp = transform.localScale;
            if (xOrY == "x")
            {
                temp.x = -1f;
            }
            else
            {
                temp.y = -1f;
            }
            transform.localScale = temp;

            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (xOrY == "x")
        {
            myBody.velocity = new Vector2(force, 0);
        }
        else if (xOrY == "y")
        {
            myBody.velocity = new Vector2(0, force);
        }
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
