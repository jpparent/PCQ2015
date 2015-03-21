using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
    
    //Timer
    public Text timerText;
    public float timerCount;
    public string timerString;

    public const int MAX_TIMER = 20;

    bool timerDone;

    //Hotspot
    public HotSpot[] hotspots;
    public int[] hotspotIndex = {0,1,2,3};
    public int activeHotspotIndex = 0;

	// Use this for initialization
	void Awake () {
        ShuffleHotspots();
        timerCount = MAX_TIMER;
        StartCoroutine(Timer());
        ChangeHotspot();
    
    }
	
    IEnumerator Timer()
    {  //triggers end of turn for hat
        timerDone = false;
        while (timerCount > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timerCount--;
            float minutes = Mathf.Floor(timerCount / 60);
            float seconds = timerCount % 60.0f; 
            timerText.text = "TIMER: " + string.Format(minutes + ":" + seconds);
            Debug.Log(timerCount);
        } StopRound();
    }

    void Update() 
    {
        Debug.Log(timerDone);
    if (timerDone) { StopRound(); }
    
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
        GameObject.FindGameObjectWithTag("PlayerHat").GetComponent<ControllerManager>().hotspot = hotspotIndex[activeHotspotIndex]+1;
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

        GameObject.Find("GameManager").GetComponent<GameManager>().NextRound();
    }

}
