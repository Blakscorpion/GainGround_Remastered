using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeaveStage : MonoBehaviour
{
    private GameObject PlayerWhoExits;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player"))
        {
            PlayerWhoExits = collider.gameObject;
            PlayerWhoExits.GetComponent<Collider2D>().enabled=false;
            PlayerWhoExits.GetComponent<Animator>().enabled = false;
            PlayerWhoExits.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log(collider.name + " PASSED !");
            Destroy(PlayerWhoExits, 1f);

            GameManager.Instance.UpdateGameState(GameState.ExitSuccess);
        }
    }
}
