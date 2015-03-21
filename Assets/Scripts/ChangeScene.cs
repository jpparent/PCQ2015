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

    public void RemoveMenu() 
    {
        GameObject[] menuList =  GameObject.FindGameObjectsWithTag("Menu");
        foreach (GameObject m in menuList) { Destroy(m); }
        
    
    }
}
