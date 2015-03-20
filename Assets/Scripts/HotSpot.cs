using UnityEngine;
using System.Collections;

public class HotSpot : MonoBehaviour {

    
    public bool isActive = false;
    public float scoreIncreaseRate = 1f; // one second delay between score increment
    private float nextScoreIncrease = 0f;
    public int scoreValue = 100;    // staying inside the hotspot for [delayBetweenScoreIncrement] seconds increment the hat's score by this amount

    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
	}
	
	// Update is called once per frame
	void Update () {


	}

    void OnTriggerStay( Collider other){
    
        if( isActive && other.tag == "PlayerHat" && Time.time > nextScoreIncrease ){

            nextScoreIncrease = Time.time + scoreIncreaseRate;

            Debug.Log("score increase!!! YOU RULE!");

            gameManager.GetComponent<GameManager>().addScoreToPlayer(gameManager.GetComponent<GameManager>().getHat()-1,1);

        }
    }


}
