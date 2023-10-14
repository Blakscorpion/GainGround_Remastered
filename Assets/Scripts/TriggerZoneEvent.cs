using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneEvent : MonoBehaviour
{
    [SerializeField] private DialogueScriptableObject dialogueToPlay;
    [SerializeField] private bool triggerOnlyOnce;
    [SerializeField] private bool triggeredOnPlayer;
    [SerializeField] private bool triggeredOnEnnemy;
    [SerializeField] private GameObject ennemyGameObject;
    [SerializeField] private GameObject playerGameObject;

    private bool alreadyTriggered=false;
    private bool playerNotFound=true;


    // Start is called before the first frame update
    void Update()
    {
        if (playerNotFound)
        {
            playerGameObject = GameObject.FindWithTag("Player");
            if (playerGameObject != null)
            {
                playerGameObject.GetComponent<shootingFreeAim>().enabled=false;
                playerNotFound=false;
            }
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggeredOnPlayer && !alreadyTriggered){
            if (other.tag=="Player")
            {
                if (triggerOnlyOnce){
                    alreadyTriggered=true;
                }
                ennemyGameObject.GetComponent<EnemyPatrol>().isPatroling=true;
                playerGameObject.GetComponent<PlayerMovement>().enabled=false;
                playerGameObject.GetComponent<Dash>().enabled=false;
                DialogueManager.Instance.PlayInstantDialogue(dialogueToPlay);
            }
        }
        if (triggeredOnEnnemy && !alreadyTriggered){
            if (other.tag=="Ennemy")
            {
                DialogueManager.Instance.PlayInstantDialogue(dialogueToPlay);
                if (triggerOnlyOnce){
                    alreadyTriggered=true;
                }
            }
        }
    }
}
