using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public Text waveLabel; //Stores a reference to the wave readout at the top right corner of the screen
    public GameObject[] nextWaveLabels; //Stores the two GameObjects that when combined, create an animation you’ll show at the start of a new wave.
    public bool gameOver = false; //store whether the player has lost the game.

    //public Text healthLabel; //Use for lives

    public Text goldLabel;

    private int gold; //store the current gold total
    public int Gold
    {
        get { return gold; } //Get gold amount
        set //Set gold amount
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "GOLD: " + gold; //Set the gold label
        }
    }

    private int wave;
    public int Wave
    {
        get { return wave; }
        set
        {
            wave = value; //Set value
            if (!gameOver) //Is game over or not?
            {
                //Set off the animation for all the wave labels
                for (int i = 0; i < nextWaveLabels.Length; i++)
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1); //Set new wave text
        }
    }

    // Use this for initialization
    void Start()
    {
        Gold = 1000;
        //Health = 5;
        Wave = 0;
    }
	
	// Update is called once per frame
	void Update()
    {
	
	}
}
