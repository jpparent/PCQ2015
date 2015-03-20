using UnityEngine;
using System.Collections;

public class HotSpot : MonoBehaviour {

    
    private bool isHatInRange = false;

    public bool isActive = false;
    public float scoreIncreaseRate = 1f; // one second delay between score increment
    private float nextScoreIncrease = 0f;
    public int scoreValue = 100;    // staying inside the hotspot for [delayBetweenScoreIncrement] seconds increment the hat's score by this amount

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	}

    void OnTriggerStay2D( Collider2D other){
    
        if( isActive && other.tag == "PlayerHat" && Time.time > nextScoreIncrease ){

            nextScoreIncrease = Time.time + scoreIncreaseRate;

            Debug.Log("score increase!!! YOU RULE!");
            
            // gameManager.playerHat ? .score += this.scoreValue

        }
    }


}
