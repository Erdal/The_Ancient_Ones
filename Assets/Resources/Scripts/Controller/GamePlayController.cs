using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GamePlayController : MonoBehaviour
{
	//Tower Upgrade Panel Components
	public GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here
	public Button upgradeButton; //Store our UpgradeButton in here from our TowerUpgradePanel
	public Button fuseButton; //Store our FuseButton in here from our TowerUpgradePanel
	public Button sellButton; //Store our SellButton in here from our TowerUpgradePanel

	//Build Tower Panel Components
	public GameObject buildTowerPanel; //Store our BuildTowerPanel in here
	public Button towerOneButton; //Store our TowerOneButton in here from our BuildTowerPanel

	//WaveScrollView Components
	public GameObject waveScrollView; //The WaveScrollView itself in gameplay scene
	public Button openCloseWaveButton; //Store the button used to open and close our wave scroll view


	//Game Status Panel Components
	public GameObject gameStatusPanel; //Store our GameStatusPanel in here
	public Button worldMapButton; //Store our WorldMapButton in here from our GameStatusPanel
	public Text winLossLabel; //Store our WinLossLabel in here from our GameStatusPanel

	[HideInInspector] //Hide from unity inspector
	public string chosenObjectsName; //Here we store the name of the BuildingSpot or tower we wish to upgrade
	[HideInInspector] //Hide from unity inspector
	public int maxWaves; //Stores the number of waves that the user is currently trying to beat
	[HideInInspector] //Hide from unity inspector
	public int currentNumberOfTowers = 0; //Used to help name tower clones so that they can be called differently

	public Text waveLabel; //Stores a reference to the wave readout at the top Left corner of the screen
	public Text unitsLeftLabel; // Store a reference to the UnitsLeftLabel on our gameplay scene
	public Text gameStatusLabel; //Stores a reference to the game status label in the center of the screen
	public Text livesLabel; //Stores a reference to the health label label in the right corner of the screen
	public Text bloodLabel;//Stores a reference to the bloodlabel label in the center top part of the screen
	public Text xpBloodLabel; //Stores a reference to the xpbloodlabel label
    public bool gameOver = false; //store whether the player has lost the game.

	public int enemyUnitsLeft; //Store how many enemy units are left

	//This method allows the user to open or close the scrollview with our future waves in it.
	public void OpenOrCloseWaveScrollView()
	{
		if (waveScrollView.activeSelf == true) 
		{
			waveScrollView.SetActive (false); //Turn off
			openCloseWaveButton.GetComponentInChildren<Text> ().text = ">";
		} 
		else 
		{
			waveScrollView.SetActive (true); //Turn on
			openCloseWaveButton.GetComponentInChildren<Text> ().text = "<";
		}

	}

	private float blood; //Store the current blood total
	public float Blood
    {
		get { return blood; }  //Return value
		set
        {
			blood = (float)Math.Round(value);; //Set blood amount
			bloodLabel.GetComponent<Text>().text = "BLOOD: " + blood; //Set the blood label
        }
    }

	private float xpBlood; //Store the current xp blood total
	public float XpBlood
	{
		get{ return xpBlood; }
		set
		{
			xpBlood = (float)Math.Round(value); //Set value
			xpBloodLabel.GetComponent<Text>().text = "XP Blood: " + xpBlood;
		}
	}

	private int lives; //Store the current amount of lives the user has left
	public int Lives
	{
		get{ return lives;} //Return value
		set
		{
			lives = value; //Set value
			livesLabel.text = "Lives: " + lives.ToString(); //Set lives label to current amount of lives
			//If lives run out
			if (lives <= 0)
			{
				gameOver = true; //Game is over
				winLossLabel.text = "GAME OVER"; //Change text to game over text
				gameStatusPanel.SetActive (true); //Activate our GameStatusPanel panel
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
				unitsLeftLabel.text = "Units " + SpawnEnemy.waves [wave]._maxEnemies.ToString (); //Set the current amount of units that will be in this wave
				enemyUnitsLeft = SpawnEnemy.waves [wave]._maxEnemies; //Set how many units there are for this wave
			} 
			else if ((wave + 1) == maxWaves) //If last wave
			{
				StartCoroutine(GameStatusCoroutine ("LAST WAVE"));
				waveLabel.text = "WAVE: " + (wave + 1); //Set new wave text
				unitsLeftLabel.text = "Units " + SpawnEnemy.waves [wave]._maxEnemies.ToString (); //Set the current amount of units that will be in this wave
				enemyUnitsLeft = SpawnEnemy.waves [wave]._maxEnemies; //Set how many units there are for this wave
			}
			SetWaveScrollView(); //Update scroll view
        }
    }

	void SetWaveScrollView()
	{
		int setWaveButtons = 0; //Used to tell us how many wave buttons can be turned on

		//Used to turn all of our wave buttons off
		for (int i = 1; i < 11; i++) 
		{
			GameObject tempWaveButton = waveScrollView.transform.FindChild ("Viewport").transform.FindChild ("Content").transform.FindChild ("WaveWaitingButton_" + i).gameObject;
			tempWaveButton.SetActive (false);
		}

		//If there are more then 10 waves left
		if ((maxWaves - wave) > 10) 
		{
			setWaveButtons = 10; //Turn on all wave buttons
		} 
		else 
		{
			setWaveButtons = maxWaves - wave; //Turn on only the amount we need
		}

		//Turn on wave buttons and set there components
		for (int i = 1; i < setWaveButtons; i++) 
		{
			GameObject tempWaveButton = waveScrollView.transform.FindChild ("Viewport").transform.FindChild ("Content").transform.FindChild ("WaveWaitingButton_" + i).gameObject; //Store this wave button as a gameobject
			Text [] newText = tempWaveButton.transform.GetComponentsInChildren<Text>(); //Store our 2 text components
			newText [0].text = "Wave: " + (wave + 1 + i); //Here we change the WaveText text field
			newText [1].text = "Units: " + SpawnEnemy.waves [wave + i]._maxEnemies.ToString (); //Here we change the UnitText text field
			tempWaveButton.SetActive (true); //Turn the wave button on
		}
	}

	//Complete our game status message to the player
	public IEnumerator GameStatusCoroutine(string message)
    {
		gameStatusLabel.text = message; //Change label text
        gameStatusLabel.gameObject.SetActive(true); //Activate label
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f)); //wait
        gameStatusLabel.gameObject.SetActive(false); //Deactivate label
    }

    // Use this for initialization
    void Start()
    {
		SetCompoinents (); //Set our varable compoinents
    }

	//Set our varable compoinents
	void SetCompoinents()
	{
		Blood = 300 + ((GamePreferences.GetBloodGainedValueIncrease() + 5) * GamePreferences.GetUnspentTons()); //Here we add on the amount of extra blood the player gets for unspent upgrade tons
		XpBlood = 0;
		Lives = 25;
		Wave = 0;
	}

	public void WorldMapScene()
	{
		SceneManager.LoadScene ("World_Map");
		Time.timeScale = 1; //Unfreeze Time
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
