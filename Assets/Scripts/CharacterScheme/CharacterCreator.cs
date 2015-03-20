using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class CharacterCreator : MonoBehaviour {

    public int RoundNumber = 0;
    int before_round;
    public int[] roundList = {1,2,3,4};
    GameObject[] PlayerList;

     

    void Awake() 
    {
        if (RoundNumber == 0)
        {
            ShuffleRound();
            
        }
       
        RoundStart();

        DontDestroyOnLoad(this);
        GameObject[] ControllerList = GameObject.FindGameObjectsWithTag("GameController");

        if (ControllerList.Length > 0)
        {
            foreach (GameObject c in ControllerList)
            {
                CharacterCreator cc = c.GetComponent<CharacterCreator>();
                if (cc.RoundNumber == 0)
                {
                    Destroy(c);
                }
            }
        }
    }

    void Update() {
    
    if(XCI.GetButtonDown(XboxButton.B)){
        RoundEnd();
    }

   // if (RoundNumber > 4) { RoundNumber = 1; }
    }

    void RoundStart() 
    {
        RoundNumber++;
        PlayerList = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in PlayerList) 
        {
            ControllerManager player_CM = p.GetComponent<ControllerManager>();
            if (player_CM.controllerNum == RoundNumber)
            {
                player_CM.isHat = true;
            }
            else 
            {
                player_CM.isHat = false;
            }
        
        }

        
    }

    void ShuffleRound() 
    {
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
