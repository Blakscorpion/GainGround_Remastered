using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyInfo : MonoBehaviour
{
    public HeroesManager.Hero babyHeroName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetName(HeroesManager.Hero babyName)
    {
        babyHeroName = babyName;
    }
}
