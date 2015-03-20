using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class CharacterCreator : MonoBehaviour {

    public int RoundNumber = 1 ;
    int before_round;
    public int[] roundList = {1,2,3,4};
    GameObject[] PlayerList;

    void Awake() 
    {
        //DontDestroyOnLoad(this);
        //RoundStart();
        ShuffleRound();
    }

    void Update() {
    
    if(XCI.GetButtonDown(XboxButton.B)){
        RoundStart();
        //RoundEnd();
    }

    if (RoundNumber > 4) { RoundNumber = 1; }
   // Debug.Log(RoundNumber);
    }

    void RoundStart() 
    {
       
       //ShuffleRound();
        PlayerList = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in PlayerList) 
        {
            ControllerManager player_CM = p.GetComponent<ControllerManager>();
            if (player_CM.controllerNum == roundList[RoundNumber])
            {
                player_CM.isHat = true;
            }
            else 
            {
                player_CM.isHat = false;
            }
        
        }

        RoundNumber++;
    }

    void ShuffleRound() 
    {
       bool isGood = true;

       for (int i = 0; i < roundList.Length;i++)
       {
           int j = Random.Range(0, i);
           int cont_i = roundList[i];
           if (i != j) 
           {
               roundList[i] = roundList[j];
           }
           roundList[j] = cont_i;
       }
    }

    void SpawnCharacter() 
    {
    
    }

    void RoundEnd() 
    {
        Application.LoadLevel(Application.loadedLevelName);
    
    }
}
