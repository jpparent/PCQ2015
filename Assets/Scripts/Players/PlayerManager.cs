using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    GameManager GManager;
    ControllerManager CManager;

	// Use this for initialization
	void Start () {
        GManager = (GameObject.FindGameObjectWithTag("GameController")).GetComponent<GameManager>();
        CManager = gameObject.GetComponent<ControllerManager>();

        CManager.isHat = false;
        if (GManager.getHat() == CManager.controllerNum) 
        {
            CManager.isHat = true;
        }
    
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
