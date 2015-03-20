using UnityEngine;
using System.Collections;

public class HotSpot : MonoBehaviour {

    
    public bool isActive = false;

    float timeInterval = 30f;
    float timeBeforeChange ; 
    public int bonusScore = 100;    // Staying until the end inside the hotspot grant the hat with this bonus.

    GameManager GManager ;

	// Use this for initialization
	void Start () {
        GManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        timeBeforeChange = Time.time + timeInterval;
    
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time >= timeBeforeChange) 
        {
            ChangeHotspot();
        }

        if (isActive)
        {
            GetComponent<ParticleSystem>().enableEmission = true;
        }
        else {
            GetComponent<ParticleSystem>().enableEmission = false;
        
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
                ChangeHotspot();
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
            ChangeHotspot();
        }
       
    }

    void ChangeHotspot() 
    {   
        timeInterval = 30f;
        timeBeforeChange = Time.time + timeInterval;
        GameObject.FindGameObjectWithTag("RoundManager").GetComponent<RoundManager>().ChangeHotspot();
    }

    void GivePoint(int point) 
    {
        GManager.addScoreToPlayer(GManager.getHat() - 1, point);

    }
}
