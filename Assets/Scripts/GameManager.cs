using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XboxCtrlrInput;


public class GameManager : MonoBehaviour {
	
	public Text[] scoreArr;
	public Text   scoreText1;
	public Text   scoreText2;
	public Text   scoreText3;
	public Text   scoreText4;
    public Text   RoundText;
	public int[]  scoreTrack;

    GameObject Camera;

    public GameObject RManager;

    //Round Data
    public int round;
  public int[] hatRound = {1, 2, 3, 4};
	
	// Use this for initialization
	void Awake() {
		scoreArr = new Text[4] {scoreText1, scoreText2, scoreText3, scoreText4};
		scoreTrack = new int[4] {0, 0, 0, 0};
		resetScore();
        ShuffleHat();
        NextRound();
        RoundText.text = "Round " + round.ToString();

	}
	
	// Update is called once per frame
	void Update() {
        RoundText.text = "Round " + round.ToString();

        if (round > 4) 
        {
            Application.LoadLevel("ScoreScene");
        }
        if (round <= 4 && round >= 1)
        {
            setScore(getHat()-1);
            Debug.Log(getHat());
        }
	}
	
	
	
	public void addScoreToPlayer(int currPlayer, int newScoreValue) {
		scoreTrack[currPlayer] += newScoreValue;
	}
	
	void setScore(int currPlayer) {	//s'attend a recevoir le # du joueur
		scoreArr[currPlayer].text = "SCORE: " + scoreTrack[currPlayer];
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
        return hatRound[round-1];
    }

    public void NextRound()
    {
       round++;
       GameObject[] AllSceneObjects = GameObject.FindObjectsOfType<GameObject>();

       foreach (GameObject go in AllSceneObjects) {
           if (go.activeInHierarchy && go.gameObject.tag != "GameController" && go.layer != LayerMask.NameToLayer("UI") && go.gameObject.tag != "MainCamera") 
           {
               Destroy(go);
           }
           else if (go.gameObject.tag == "MainCamera"){
               Camera = go;

           }
       }
      
       StartCoroutine(LoadNextLevel);
    }

    IEnumerator LoadNextLevel {
        
        get
        {
            RoundText.enabled = true;
            yield return new WaitForSeconds(5);
            Application.LoadLevelAdditiveAsync("Final");
            
            yield return new WaitForSeconds(2);
            RoundText.enabled = false;
            Destroy(Camera);

        }
    }
}