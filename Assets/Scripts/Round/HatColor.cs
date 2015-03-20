using UnityEngine;
using System.Collections;

public class HatColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        ControllerManager c = gameObject.GetComponent<ControllerManager>();
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        if (c.isHat)
        {
            sr.color = Color.green;
        }
        else {
            sr.color = Color.red;
        }
        
	}
}
