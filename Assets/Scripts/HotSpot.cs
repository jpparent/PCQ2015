using UnityEngine;
using System.Collections;

public class HotSpot : MonoBehaviour {

    
    private bool isHatInRange = false;

    public bool isActive = false;
    public float scoreIncreaseRate = 1f; // one second delay between score increment
    public float nextScoreIncrease;
    public int scoreValue = 100;    // staying inside the hotspot for [delayBetweenScoreIncrement] seconds increment the hat's score by this amount

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}

    void OnTriggerStay2D( Collider2D other){

        if( other.tag == "playerHat" && Time.time > nextScoreIncrease ){

            nextScoreIncrease = Time.time + scoreIncreaseRate; 

            // gameManager.playerHat ? .score += this.scoreValue

        }
    }


}
