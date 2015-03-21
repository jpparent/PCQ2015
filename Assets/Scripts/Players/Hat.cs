using UnityEngine;
using System.Collections;

public class Hat : MonoBehaviour
{
    public int currentLives;
    public GameObject lastHit;
    GameManager GM;
    private Animator animator;


    // Use this for initialization
    void Start() {

        animator = GetComponent<Animator>();
    }
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
        animator.SetTrigger("PlayerIsHit");
        currentLives--;
        if (currentLives == 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        //Does Dead stuff
        this.enabled = false;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(LoadNextLevel); 
        
       
    }

    IEnumerator LoadNextLevel 
    {
        get {
            yield return new WaitForSeconds(2);
            GM.NextRound();
            }
    
    }
}

