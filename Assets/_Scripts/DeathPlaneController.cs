using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DeathPlaneController
//Andrew Trinidad
//301021154
//Last Modified: Oct 4, 2019
//Program Description: This controller detects if the player 
//has collided with it and sends them to a checkpoint.

[System.Serializable]
public class DeathPlaneController : MonoBehaviour
{
    public Transform activeCheckpoint;
    public GameObject player;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = activeCheckpoint.position;

        }
    }
}
