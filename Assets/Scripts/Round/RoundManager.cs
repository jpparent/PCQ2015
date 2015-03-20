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
    public HotSpot[] hotspots;
    public int[] hotspotIndex = {0,1,2,3};
    public int activeHotspotIndex = 0;

    GameManager GManager;

	// Use this for initialization
	void Awake () {
        ShuffleHotspots();
        timerCount = MAX_TIMER;
        StartCoroutine(Timer());
        ChangeHotspot();
        GManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    
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
        StopRound();
        
    }

    public void SetTimerText(Text timerT) 
    {
        this.timerText = timerT;
    }

    public void ChangeHotspot() {
        if (activeHotspotIndex < hotspots.Length) {
            activeHotspotIndex++;
        }
        else if (activeHotspotIndex >= hotspots.Length)
        {
            activeHotspotIndex =0;
            ShuffleHotspots();
        }
        foreach (HotSpot htspt in hotspots) {
            htspt.isActive = false;
        }
        hotspots[hotspotIndex[activeHotspotIndex]].isActive = true;
        GameObject.FindGameObjectWithTag("PlayerHat").GetComponent<ControllerManager>().hotspot = hotspotIndex[activeHotspotIndex];
    }

    void ShuffleHotspots() {
        for (int i = 0; i < hotspots.Length; i++)
        {
            int j = Random.Range(0, i);
            int source = hotspotIndex[i];
            if (j != i)
            {
                hotspotIndex[i] = hotspotIndex[j];
            }
            hotspotIndex[j] = source;
        }
    
    }

    void StopRound() {

        GManager.NextRound();
    }

}
