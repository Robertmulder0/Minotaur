using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public PlayerController player;

    void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player")){
            Debug.Log("Player picked up ammo");
            player.slingShotAmmo += 1;
            Destroy(this.gameObject);
        }
    }
}
