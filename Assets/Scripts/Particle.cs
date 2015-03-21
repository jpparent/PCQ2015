using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

    public bool invert;
    public Transform Particle_pad; 
	// Update is called once per frame
	void Update () {
        if (invert)
        {
            transform.position = new Vector3((-Mathf.Sin(Time.time) / 3) + Particle_pad.position.x, 0, (-Mathf.Cos(Time.time) / 3) + Particle_pad.position.z);
        }
        else {
            transform.position = new Vector3((Mathf.Sin(Time.time) / 3) + Particle_pad.position.x, 0, (Mathf.Cos(Time.time) / 3) + Particle_pad.position.z);
        }
        
        //this.GetComponent<ParticleSystem>().startSpeed = Mathf.Abs(Mathf.Sin(Time.time*10 +1))/5;

        
	}
}
