using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void ChangeToScene (string sceneToChangeTo) {
		Application.LoadLevel (sceneToChangeTo);
	}

    public void CallMenu(string menuToCall){

        Application.LoadLevelAdditive(menuToCall);
    
    } 
	public void exit() {
		Application.Quit ();
	}
}
