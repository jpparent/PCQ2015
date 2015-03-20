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


   

    //Round Data
    public int round;
  public int[] hatRound = {1, 2, 3, 4};
	
	// Use this for initialization
	void Awake() {
		scoreArr = new Text[4] {scoreText1, scoreText2, scoreText3, scoreText4};
		scoreTrack = new int[4] {0, 0, 0, 0};
		resetScore();

        ShuffleHat();

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

    public int getHat()
    {
        return hatRound[round];
    }

    public void NextRound()
    {
        round++;
    }
}