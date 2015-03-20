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
	
	// Use this for initialization
	void Start() {
		scoreArr = new Text[4] {scoreText1, scoreText2, scoreText3, scoreText4};
		scoreTrack = new int[4] {11, 2, 0, 0};
		//setScore (1);
		resetScore();
	}
	
	// Update is called once per frame
	void Update() {
		
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