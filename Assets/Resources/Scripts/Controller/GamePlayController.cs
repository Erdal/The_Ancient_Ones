using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
	public GameObject towerUpgradePanel; //Store our TowerUpgradePanel in here
	public Button optionOneButton; ////Store our OptionOneButton in here from our TowerUpgradePanel

	[HideInInspector] //Hide from unity inspector
	public string chosenObjectsName; //Here we store the name of the BuildingSpot or tower we wish to upgrade

	public Text waveLabel; //Stores a reference to the wave readout at the top Left corner of the screen
    public Text nextWaveLabel; //Stores a reference to the next wave label at the top of the screen
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
            wave = value; //Set value
            if (!gameOver) //If game isnt over
            {
                StartCoroutine(NextWaveCoroutine());
                waveLabel.text = "WAVE: " + (wave + 1); //Set new wave text
            }
        }
    }

    IEnumerator NextWaveCoroutine()
    {
        nextWaveLabel.gameObject.SetActive(true); //Activate label
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(.7f)); //wait
        nextWaveLabel.gameObject.SetActive(false); //Deactivate label
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
