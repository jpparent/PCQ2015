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
	
	// Use this for initialization
	void Start() {
		scoreArr = new Text[4] {scoreText1, scoreText2, scoreText3, scoreText4};
		scoreTrack = new int[4] {0, 0, 0, 0};
		resetScore();
		timerCount = MAX_TIMER;
		StartCoroutine (Timer ());
	}
	
	// Update is called once per frame
	void Update() {
		
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
}