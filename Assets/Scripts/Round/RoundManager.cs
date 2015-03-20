using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {

    int round;
    int[] hatRound = {1,2,3,4};

	// Use this for initialization
	void Start () {
        round = 1;
        ShuffleHat();
	}
	
	// Update is called once per frame
	void Update () {
	
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
            hatRound[j]=source;
        }
    }

    int getHat()
    {
        return hatRound[round];
    }

    void NextRound() {
        round++;

    }
}
