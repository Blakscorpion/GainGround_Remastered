using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneEvent : MonoBehaviour
{
    [SerializeField] private DialogueScriptableObject[] dialogueToPlay;
    [SerializeField] private bool triggerOnlyOnce;
    [SerializeField] private bool triggeredOnPlayer;
    [SerializeField] private bool triggeredOnEnnemy;
    [SerializeField] private GameObject ennemyGameObject;

    private bool alreadyTriggered=false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggeredOnPlayer && !alreadyTriggered){
            if (other.tag=="Player")
            {
                DialogueManager.Instance.PlayInstantDialogue(dialogueToPlay);
                if (triggerOnlyOnce){
                    alreadyTriggered=true;
                }
                ennemyGameObject.GetComponent<EnemyPatrol>().isPatroling=true;
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
