using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject player;
    public bool hasBeenEntered;

    void Start(){
        hasBeenEntered = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player")){
            Debug.Log("Player entered end goal");
            hasBeenEntered = true;
        }
    }
}
