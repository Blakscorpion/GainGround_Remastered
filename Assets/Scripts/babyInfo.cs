using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyInfo : MonoBehaviour
{

    [SerializeField] HeroesManager.Hero HeroFollowing;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetName(HeroesManager.Hero hero)
    {
        HeroFollowing = hero;
    }
}
