using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
    
    //Timer
    public Text timerText;
    public int timerCount;
    public string timerString;
    public const int MAX_TIMER = 120;

    //Hotspot
    public GameObject[] hotspots;
    public int activeHotspotIndex = 1;

    public float hotspotActiveTime = 5f; // time that hotspots stay active
    public float hotspotActiveUntil;

    public float hotspotCycleDelay = 5f; // delay between to hotspot's activation
    private float nextHotspotCycle;

	// Use this for initialization
	void Awake () {

        hotspots = GameObject.FindGameObjectsWithTag("Hotspot");
        timerCount = MAX_TIMER;
        StartCoroutine(Timer());
        StartRound();
    
    
    }
	
	// Update is called once per frame
	void Update () {


        // check if it's time to turn off a hotspot
        if (activeHotspotIndex >= 0 && Time.time > hotspotActiveUntil)
        {

            hotspots[activeHotspotIndex].GetComponent<HotSpot>().isActive = false;
            activeHotspotIndex = -1; // hack to know that no hotspots are active

            nextHotspotCycle = Time.time + hotspotCycleDelay; // set time for the next cycle
        }

        // check if it's time to choose another hotspot
        else if (activeHotspotIndex < 0 && Time.time > nextHotspotCycle)
        {

            activeHotspotIndex = Random.Range(0, hotspots.Length); // FIXME make it impossible to choose the same twice in a 
            hotspots[activeHotspotIndex].GetComponent<HotSpot>().isActive = true;

            hotspotActiveUntil = Time.time + hotspotActiveTime;

        }

	}

    void StartRound(){

        // activate a hotspot randomly
        activeHotspotIndex = Random.Range(0, hotspots.Length);
        hotspots[activeHotspotIndex].GetComponent<HotSpot>().isActive = true;
      
    }

    IEnumerator Timer()
    {  //triggers end of turn for hat
        while (timerCount > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timerCount--;
            //timerString = timerString.Format("{0:0}:{1:00}", Mathf.Floor(timerCount/60), timerCount % 60);
            timerText.text = "Timer: " + timerCount;
        } //timer==0 -> end of turn
        
    }

    public void SetTimerText(Text timerT) 
    {
        this.timerText = timerT;
    
    }
    
}
