using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XboxCtrlrInput;


public  class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Text[] scoreArr;
    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;
    public Text RoundText;
    public int[] scoreTrack;

    GameObject Camera;

    public GameObject RManager;

    //Round Data
    public int round = 1;
    public int[] hatRound = { 1, 2, 3, 4 };

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        { instance = this; }
        else if(instance != null)
        { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        scoreArr = new Text[4] { scoreText1, scoreText2, scoreText3, scoreText4 };
        scoreTrack = new int[4] { 0, 0, 0, 0 };
        resetScore();
        ShuffleHat();
        NextRound();
        RoundText.text = "Round " + round;

    }

    void OnLevelWasLoaded(int index)
    {
        round++;
    }

    public void GameOver()
    {

        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        RoundText.text = "Round " + round;

        if (round > 4)
        {
            Application.LoadLevel("ScoreScene");
        }
        if (round <= 4 && round >= 1)
        {
            setScore(getHat() - 1);
        }
    }


    // move to score manager
    public void addScoreToPlayer(int currPlayer, int newScoreValue)
    {
        scoreTrack[currPlayer] += newScoreValue;
    }

    // move to score manager
    void setScore(int currPlayer)
    {	//s'attend a recevoir le # du joueur
        scoreArr[currPlayer].text = "SCORE: " + scoreTrack[currPlayer];
    }

    // move to score manager
    void resetScore()
    {
        for (int i = 0; i < 4; i++)
        {
            scoreTrack[i] = 0;
            setScore(i);
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
        return hatRound[round - 1];
    }

    public void NextRound()
    {
        round++;

        GameObject[] AllSceneObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject go in AllSceneObjects)
        {
            if (go.activeInHierarchy && go.gameObject.tag != "GameController" && go.layer != LayerMask.NameToLayer("UI") && go.gameObject.tag != "MainCamera" && go.gameObject.tag != "CameraMenu")
            {
                Destroy(go);
            }
            else if (go.gameObject.tag == "CameraMenu")
            {
                Camera = go;

            }
        }
        StartCoroutine(LoadNextLevel);
    }

    IEnumerator LoadNextLevel
    {
        get
        {
            // yield return new WaitForSeconds(1);

            Camera.SetActive(true);
            RoundText.enabled = true;
            yield return new WaitForSeconds(5);
            Application.LoadLevelAdditiveAsync("Final");
            RoundText.enabled = false;
            yield return new WaitForSeconds(1);
            Camera.SetActive(false);

        }
    }
}