using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject minotaur;
    public GoalManager goalManager;

    public bool gameWon;
    public bool playerLost;

    public TextMeshProUGUI Text;

    // Start is called before the first frame update
    void Start()
    {
        gameWon = false;
        playerLost = false;
        Text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(minotaur.transform.position, player.transform.position) < 1.5){
            playerLost = true;
        }

        if (goalManager.hasBeenEntered){
            gameWon = true;
        }


        //set text
        if (gameWon == true){
            Text.text = "You Escaped!";
        } else if (playerLost == true) {
            Text.text = "You Were Caught!";
        } else {
            Text.text = "";
        }
    }
}
