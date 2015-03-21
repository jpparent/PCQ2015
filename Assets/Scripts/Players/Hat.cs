using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour
{
    public int currentLives;
    public GameObject lastHit;

    // Use this for initialization
    void Awake()
    {
        currentLives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            bool action = col.gameObject.GetComponent<Chase>().IsTackling();
            if (action)
            {
                if (lastHit != null)
                {
                    //lastHit.SendMessage("CanTackle", true);
                }

                lastHit = col.gameObject;
                //col.gameObject.SendMessage("CanTackle", false);
                Hit();
                Debug.Log("hit");
            }
        }
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
        Destroy(gameObject);
    }
}

