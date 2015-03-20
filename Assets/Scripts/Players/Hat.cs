﻿using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour {

    public int currentLives;

	// Use this for initialization
	void Awake () {
	    currentLives = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Hit()
    {
        currentLives--;
        if (currentLives == 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        //Does Dead stuff
    }
}

