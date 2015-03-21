using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public GameManager gameMan;
    public Transform spawnPoint;
    public ControllerManager contMan;
    public PlayerNumber playerNum;
    public bool hatSpawnPoint;

    int hatTurn;

    // Use this for initialization
    void Start()
    {
        gameMan = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        contMan = player.GetComponent<ControllerManager>();
        Debug.Log(gameMan.round);
        hatTurn = gameMan.hatRound[gameMan.round];
        Debug.Log("num " + hatTurn);

        if ((int)playerNum == hatTurn-1)
        {
            contMan.isHat = true;
            spawnPoint = GameObject.Find("HatSpawner").transform;
        }
        else
        {
            contMan.isHat = false;
        }

            Spawn();
        
    }

    void Spawn()
    {
        Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        contMan.playerNum = playerNum;
    }
}
