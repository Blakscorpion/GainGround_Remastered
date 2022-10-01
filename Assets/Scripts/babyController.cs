using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyController : MonoBehaviour
{
    GameObject CurrentPlayer;
    bool isFollowingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player")
        {
            isFollowingPlayer=true;
            this.transform.position = CurrentPlayer.transform.position;
        }
    }

    
}
