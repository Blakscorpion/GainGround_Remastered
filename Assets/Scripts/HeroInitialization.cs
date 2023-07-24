using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInitialization : MonoBehaviour
{
    HeroScriptableObject HeroScriptableObject;
    public HeroesManager.Hero HeroName;
    void InitHero(HeroesManager.Hero HeroToCreate)
    {
        //Search into all scriptable objects
        HeroScriptableObject = Resources.Load<HeroScriptableObject>("Heroes/" + HeroToCreate.ToString());
        HeroName = HeroScriptableObject.HeroName;
    }
}
