using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
	
	public Text[] scoreArr;
	public Text scoreText1;
	public Text scoreText2;
	public Text scoreText3;
	public Text scoreText4;
	public int[] scoreTrack;

    public HotSpot[] hotspots;
    public int activeHotspotIndex = 1;

    public float hotspotActiveTime = 5f; // time that hotspots stay active
    public float hotspotActiveUntil;
   
    public float hotspotCycleDelay = 5f; // delay between to hotspot's activation
    private float nextHotspotCycle;


	
	// Use this for initialization
	void Start() {
		scoreArr = new Text[4] {scoreText1, scoreText2, scoreText3, scoreText4};
		scoreTrack = new int[4] {11, 2, 0, 0};
		//setScore (1);
		resetScore();

        // activate a hotspot randomly
        activeHotspotIndex = Random.Range(0, hotspots.Length);
        hotspots[activeHotspotIndex].isActive = true;

	}
	
	// Update is called once per frame
	void Update() {

        // check if it's time to turn off a hotspot
        if (activeHotspotIndex >= 0 && Time.time > hotspotActiveUntil) {

            hotspots[activeHotspotIndex].isActive = false;
            activeHotspotIndex = -1; // hack to know that no hotspots are active

            nextHotspotCycle = Time.time + hotspotCycleDelay; // set time for the next cycle
        }
        
        // check if it's time to choose another hotspot
        else if (activeHotspotIndex < 0 && Time.time > nextHotspotCycle) {

            activeHotspotIndex = Random.Range(0, hotspots.Length); // FIXME make it impossible to choose the same twice in a 
            hotspots[activeHotspotIndex].isActive = true;

            hotspotActiveUntil = Time.time + hotspotActiveTime;

        }

	}
	
	public void addScoreToPlayer(int currPlayer, int newScoreValue) {
		scoreTrack[currPlayer] += newScoreValue;
	}
	
	void setScore(int currPlayer) {	//s'attend a recevoir le # du joueur
		scoreArr[currPlayer].text = "score: " + scoreTrack[currPlayer];
	}
	
	void resetScore() {
		for (int i=0; i<4; i++) {
			scoreTrack[i] = 0;
			setScore (i);
		}
	}
}