using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {
	
	public Text[] scoreArr;
	public Text   scoreText1;
	public Text   scoreText2;
	public Text   scoreText3;
	public Text   scoreText4;
	public int[]  scoreTrack;
	public Text   timerText;
	public int    timerCount;
	public string timerString;
	public const int MAX_TIMER = 120;

    public HotSpot[] hotspots;
    public int activeHotspotIndex = 1;

    public float hotspotActiveTime = 5f; // time that hotspots stay active
    public float hotspotActiveUntil;
   
    public float hotspotCycleDelay = 5f; // delay between to hotspot's activation
    private float nextHotspotCycle;

    //Round Data
    public int round;
    int[] hatRound;
	
	// Use this for initialization
	void Awake() {
		scoreArr = new Text[4] {scoreText1, scoreText2, scoreText3, scoreText4};
		scoreTrack = new int[4] {0, 0, 0, 0};
		resetScore();
		timerCount = MAX_TIMER;
		StartCoroutine (Timer ());

        // activate a hotspot randomly
        activeHotspotIndex = Random.Range(0, hotspots.Length);
        hotspots[activeHotspotIndex].isActive = true;

        if (round == 1) { 
        hatRound = new int[4] { 1, 2, 3, 4 };
        ShuffleHat();
        }

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
	
	IEnumerator Timer() {  //triggers end of turn for hat
		while (timerCount>0) {
			yield return new WaitForSeconds(1.0f);
			timerCount--;
			//timerString = timerString.Format("{0:0}:{1:00}", Mathf.Floor(timerCount/60), timerCount % 60);
			timerText.text = "Timer: " + timerCount;
		} //timer==0 -> end of turn
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

    void ShuffleHat()
    {
        for (int i = 0; i < hatRound.Length; i++)
        {
            int j = Random.Range(0, i);
            int source = hatRound[i];
            if (j != i)
            {
                hatRound[i] = hatRound[j];
            }
            hatRound[j] = source;
        }
    }

    int getHat()
    {
        return hatRound[round];
    }

    void NextRound()
    {
        round++;
    }
}