using UnityEngine;
using System.Collections;

public class HotSpot : MonoBehaviour {

    
    public bool isActive = false;

    float timeInterval = 30f;
    float timeBeforeChange ; 
    public int bonusScore = 100;    // Staying until the end inside the hotspot grant the hat with this bonus.

    GameManager GManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

	// Use this for initialization
	void Start () {
        timeBeforeChange = Time.time + timeInterval;
    
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Time.time >= timeBeforeChange) 
        {
            ChangeHotspot();
        }

	}

    void OnTriggerEnter(Collider other) {
        if (isActive && other.tag == "PlayerHat")
        {
            timeInterval = 10f;
            timeBeforeChange = Time.time + timeInterval;
        }
    }

    void OnTriggerStay( Collider other){
        if( isActive && other.tag == "PlayerHat" ){

            if (Time.time >= timeBeforeChange) 
            {
                GivePoint(bonusScore);
            }
            else if (timeBeforeChange > 0 && Time.time < timeBeforeChange) {
                GivePoint(1);
            }
        }
    }

    void OnTriggerExit( Collider other) 
    {
        if (isActive && other.tag == "PlayerHat")
        { 
            timeInterval = 30f;
            ChangeHotspot();
           
        }
       
    }

    void ChangeHotspot() 
    {
        timeBeforeChange = Time.time + timeInterval;
        GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>().ChangeHotspot();
    }

    void GivePoint(int point) 
    {
        GManager.addScoreToPlayer(GManager.getHat() - 1, point);

    }
}
