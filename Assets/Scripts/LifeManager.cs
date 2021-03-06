﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour {

    public RectTransform Life1;
    public RectTransform Life2;
    public RectTransform Life3;

    int lifeCounter;

	// Update is called once per frame
	void Update () {

        try{
        lifeCounter = GameObject.FindGameObjectWithTag("PlayerHat").GetComponent<Hat>().currentLives;
        }
        catch
        {
            lifeCounter = 0;
        }
        switch (lifeCounter) 
        {
            case 0:
                Life1.GetComponent<Image>().enabled = false;
                Life2.GetComponent<Image>().enabled = false;
                Life3.GetComponent<Image>().enabled = false;
                break;
            case 1:
                Life1.GetComponent<Image>().enabled = true;
                Life2.GetComponent<Image>().enabled = false;
                Life3.GetComponent<Image>().enabled = false;
                break;
            case 2:
                Life1.GetComponent<Image>().enabled = true;
                Life2.GetComponent<Image>().enabled = true;
                Life3.GetComponent<Image>().enabled = false;
                break;
            case 3:
                Life1.GetComponent<Image>().enabled = true;
                Life2.GetComponent<Image>().enabled = true;
                Life3.GetComponent<Image>().enabled = true;
                break;
        
        
        }

	}
}
