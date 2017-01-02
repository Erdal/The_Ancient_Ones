using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
	public GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here
	public Button optionOneButton; ////Store our OptionOneButton in here from our TowerUpgradePanel

	[HideInInspector] //Hide from unity inspector
	public string chosenObjectsName; //Here we store the name of the BuildingSpot or tower we wish to upgrade
	[HideInInspector] //Hide from unity inspector
	public int maxWaves; //Stores the number of waves that the user is currently trying to beat

	public Text waveLabel; //Stores a reference to the wave readout at the top Left corner of the screen
	public Text gameStatusLabel; //Stores a reference to the game status label in the center of the screen
	public Text livesLabel; //Stores a reference to the health label label in the right corner of the screen
	public Text bloodLabel;//Stores a reference to the blood label label in the center top part of the screen
    public bool gameOver = false; //store whether the player has lost the game.

    private int blood; //store the current blood total
    public int Blood
    {
		get { return blood; }  //Return value
		set
        {
			blood = value; //Set blood amount
			bloodLabel.GetComponent<Text>().text = "BLOOD: " + blood; //Set the blood label
        }
    }

	private int lives; //Store the current amount of lives the user has left
	public int Lives
	{
		get{ return lives;} //Return value
		set
		{
			lives = value; //Set value
			livesLabel.text = lives.ToString(); //Set lives label to current amount of lives
			//If lives run out
			if (lives <= 0)
			{
				gameOver = true; //Game is over
				StartCoroutine (GameStatusCoroutine ("GAME OVER"));
				Time.timeScale = 0; //Freeze time
			}
		}
	}

    private int wave; //Store current wave
    public int Wave
    {
		get { return wave; }  //Return value
        set
        {
            wave = value; //Set value
			//If game isnt over
			if (!gameOver && (wave + 1) < maxWaves) 
			{
				StartCoroutine (GameStatusCoroutine ("NEXT WAVE"));
				waveLabel.text = "WAVE: " + (wave + 1); //Set new wave text
			} 
			else if ((wave + 1) == maxWaves) //If last wave
			{
				StartCoroutine(GameStatusCoroutine ("LAST WAVE"));
				waveLabel.text = "WAVE: " + (wave + 1); //Set new wave text
			}
        }
    }

	//Complete our game status message to the player
	IEnumerator GameStatusCoroutine(string message)
    {
		gameStatusLabel.text = message; //Change label text
        gameStatusLabel.gameObject.SetActive(true); //Activate label
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f)); //wait
        gameStatusLabel.gameObject.SetActive(false); //Deactivate label
    }

    // Use this for initialization
    void Start()
    {
		Blood = 800;
		Lives = 10;
        Wave = 0;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
