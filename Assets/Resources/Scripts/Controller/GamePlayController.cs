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
	public int maxWaves;

	public Text waveLabel; //Stores a reference to the wave readout at the top Left corner of the screen
	public Text gameStatusLabel; //Stores a reference to the game status label in the center of the screen
    public bool gameOver = false; //store whether the player has lost the game.
    //public Text healthLabel; //Use for lives

    public Text bloodLabel;

    private int blood; //store the current gold total
    public int Blood
    {
		get { return blood; } //Get blood amount
		set //Set blood amount
        {
			blood = value;
			bloodLabel.GetComponent<Text>().text = "BLOOD: " + blood; //Set the blood label
        }
    }

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
			Debug.Log (wave);
			Debug.Log (maxWaves);
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
		gameStatusLabel.text = message;
        gameStatusLabel.gameObject.SetActive(true); //Activate label
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f)); //wait
        gameStatusLabel.gameObject.SetActive(false); //Deactivate label
    }

    // Use this for initialization
    void Start()
    {
		Blood = 800;
        //Health = 5;
        Wave = 0;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
