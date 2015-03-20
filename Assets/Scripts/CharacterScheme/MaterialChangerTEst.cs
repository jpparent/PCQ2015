using UnityEngine;
using System.Collections;

public class MaterialChangerTEst : MonoBehaviour {
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        ControllerManager playerController = GetComponent<ControllerManager>();

        if (playerController.isHat)
        {
            renderer.material.color = Color.blue;
        }
        else
        {

            renderer.material.color = Color.green;
        }


    }
}
