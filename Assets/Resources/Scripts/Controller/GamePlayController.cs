using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;

public class GamePlayController : MonoBehaviour
{
	//Tower Upgrade Panel Components
	[HideInInspector] //Hide from unity inspector
	public GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here
	[HideInInspector] //Hide from unity inspector
	public Button upgradeButton; //Store our UpgradeButton in here from our TowerUpgradePanel
	[HideInInspector] //Hide from unity inspector
	public Button fuseButton; //Store our FuseButton in here from our TowerUpgradePanel
	[HideInInspector] //Hide from unity inspector
	public Button sellButton; //Store our SellButton in here from our TowerUpgradePanel

	//Build Tower Panel Components
	[HideInInspector] //Hide from unity inspector
	public GameObject buildTowerPanel; //Store our BuildTowerPanel in here
	[HideInInspector] //Hide from unity inspector
	public Button towerOneButton; //Store our TowerOneButton in here from our BuildTowerPanel
	[HideInInspector] //Hide from unity inspector
	public Button towerTwoButton; //Store our TowerTwoButton in here from our BuildTowerPanel
	[HideInInspector] //Hide from unity inspector
	public Button towerThreeButton; //Store our TowerThreeButton in here from our BuildTowerPanel

	//WaveScrollView Components
	[HideInInspector] //Hide from unity inspector
	public GameObject waveScrollView; //The WaveScrollView itself in gameplay scene
	[HideInInspector] //Hide from unity inspector
	public Button openCloseWaveButton; //Store the button used to open and close our wave scroll view


	//Game Status Panel Components
	[HideInInspector] //Hide from unity inspector
	public GameObject gameStatusPanel; //Store our GameStatusPanel in here
	[HideInInspector] //Hide from unity inspector
	public Button worldMapButton; //Store our WorldMapButton in here from our GameStatusPanel
	[HideInInspector] //Hide from unity inspector
	public Text winLossLabel; //Store our WinLossLabel in here from our GameStatusPanel

	[HideInInspector] //Hide from unity inspector
	public string chosenObjectsName; //Here we store the name of the BuildingSpot or tower we wish to upgrade
	[HideInInspector] //Hide from unity inspector
	public int maxWaves; //Stores the number of waves that the user is currently trying to beat
	[HideInInspector] //Hide from unity inspector
	public int currentNumberOfTowers = 0; //Used to help name tower clones so that they can be called differently

	[HideInInspector] //Hide from unity inspector
	public Text waveLabel; //Stores a reference to the wave readout at the top Left corner of the screen
	[HideInInspector] //Hide from unity inspector
	public Text unitsLeftLabel; // Store a reference to the UnitsLeftLabel on our gameplay scene
	[HideInInspector] //Hide from unity inspector
	public Text gameStatusLabel; //Stores a reference to the game status label in the center of the screen
	[HideInInspector] //Hide from unity inspector
	public Text livesLabel; //Stores a reference to the health label label in the right corner of the screen
	[HideInInspector] //Hide from unity inspector
	public Text bloodLabel;//Stores a reference to the bloodlabel label in the center top part of the screen
	[HideInInspector] //Hide from unity inspector
	public Text xpBloodLabel; //Stores a reference to the xpbloodlabel label
	[HideInInspector] //Hide from unity inspector
    public bool gameOver = false; //store whether the player has lost the game.

	//OnHoverPanel
	[HideInInspector] //Hide from unity inspector
	public GameObject onHoverPanel; //Store our OnHoverPanel here
	[HideInInspector] //Hide from unity inspector
	public Text onHoverText; //Store our OnHoverText from our OnHoverPanel here

	[HideInInspector] //Hide from unity inspector
	public int enemyUnitsLeft; //Store how many enemy units are left

	Type prefTypeHoverDescription; //Used to store the HoverDescriptions class we wish to connect to using MethodInfo class

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
		prefTypeHoverDescription = typeof(HoverDescriptions); //Get type of class HoverDescriptions

		//Set Components (Decription up at top of class)
		towerUpgradePanel = GameObject.Find ("Canvas").transform.Find("TowerUpgradePanel").gameObject;
		buildTowerPanel = GameObject.Find ("Canvas").transform.Find("BuildTowerPanel").gameObject;
		waveScrollView = GameObject.Find ("Canvas").transform.Find("WaveScrollView").gameObject;
		gameStatusPanel = GameObject.Find ("Canvas").transform.Find("GameStatusPanel").gameObject;
		onHoverPanel = GameObject.Find ("Canvas").transform.Find ("OnHoverPanel").gameObject;

		bloodLabel =  GameObject.Find ("Canvas").transform.Find("BloodLabel").GetComponent<Text>();
		waveLabel =  GameObject.Find ("Canvas").transform.Find("WaveLabel").GetComponent<Text>();
		unitsLeftLabel =  GameObject.Find ("Canvas").transform.Find("UnitsLeftLabel").GetComponent<Text>();
		gameStatusLabel =  GameObject.Find ("Canvas").transform.Find("GameStatusLabel").GetComponent<Text>();
		livesLabel =  GameObject.Find ("Canvas").transform.Find("LivesLabel").GetComponent<Text>();
		winLossLabel =  gameStatusPanel.transform.Find("WinLossLabel").GetComponent<Text>();
		onHoverText =  onHoverPanel.transform.Find("OnHoverText").GetComponent<Text>();

		towerOneButton = buildTowerPanel.transform.Find ("TowerOneButton").GetComponent<Button> ();
		towerTwoButton = buildTowerPanel.transform.Find ("TowerTwoButton").GetComponent<Button> ();
		towerThreeButton = buildTowerPanel.transform.Find ("TowerThreeButton").GetComponent<Button> ();
		upgradeButton = towerUpgradePanel.transform.Find ("UpgradeButton").GetComponent<Button> ();
		fuseButton = towerUpgradePanel.transform.Find ("FuseButton").GetComponent<Button> ();
		sellButton = towerUpgradePanel.transform.Find ("SellButton").GetComponent<Button> ();
		openCloseWaveButton =  GameObject.Find ("Canvas").transform.Find ("OpenCloseWaveScrollViewButton").GetComponent<Button> ();
		worldMapButton = gameStatusPanel.transform.Find ("WorldMapButton").GetComponent<Button> ();

		//Set in game components
		Blood = 3000 + ((GamePreferences.GetBloodGainedValueIncrease() + 5) * GamePreferences.GetUnspentTons()); //Here we add on the amount of extra blood the player gets for unspent upgrade tons
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

	public void OnHoverObjectDescription(string objectName)
	{
		MethodInfo methodInfoGet = prefTypeHoverDescription.GetMethod ("Get" + objectName + "Description"); //Get this method by name
		onHoverText.text = methodInfoGet.Invoke (null, null).ToString(); //Invoke method and use its return as the text for onHoverText
		onHoverPanel.gameObject.SetActive (true); //Turn on our panel
	}

	public void OffHoverObjectDescription()
	{
		onHoverPanel.gameObject.SetActive (false);
	}
}
