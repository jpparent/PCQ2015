using UnityEngine;
using System.Collections;

public class Chase : MonoBehaviour
{

    public Vector2 oldPos;
    public Vector2 newPos;

    // tackling -> l'action
    // isTackling -> l'evenement
    bool tackling;
    bool isTackling;
    
    // Use this for initialization
    void Awake()
    {
        tackling = false;
        isTackling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tackling)
        { StartCoroutine("Tackle"); }
    }

    public IEnumerator Tackle()
    {
        tackling = false;
        isTackling = true;
        oldPos = transform.position;
        newPos = Vector2.right + oldPos;

        Debug.Log("start tackle");
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetBool("isKicking", true);
        transform.position = newPos;

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isKicking", false);
        Debug.Log("done tackle");

        transform.position = oldPos;
        isTackling = false;
    }

    public void Tackling()
    {
        tackling = true;
    }

    public bool IsTackling()
    { return isTackling; }
}
