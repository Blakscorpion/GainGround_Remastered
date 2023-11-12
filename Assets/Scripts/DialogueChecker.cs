using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChecker{

    //Check if the structure of an array of dialogues is technically valid
    public static void checkListOfDialoguesStructure(DialogueScriptableObject[] dialogueToAnalyse){
        if (dialogueToAnalyse!= null && dialogueToAnalyse.Length!=0){
            foreach (DialogueScriptableObject dialogue in dialogueToAnalyse){
                checkSingleDialogueStructure(dialogue);
            }
        }
    }

    //Check if the structure of a dialogue is technically valid
    public static void checkSingleDialogueStructure(DialogueScriptableObject dialogueToAnalyse){
        if (dialogueToAnalyse != null && dialogueToAnalyse.dialogues.Length!=0){
            for(int i = 0; i<dialogueToAnalyse.dialogues.Length; i+=1){
                if (dialogueToAnalyse.dialogues[i].dialogueLine == "" || dialogueToAnalyse.dialogues[i].dialogueLine ==null){
                    int iterationNumber = i+1;
                    Debug.LogWarning("Dialogue "+dialogueToAnalyse.name+" has the sentence number "+iterationNumber+" empty. Dialogue is skipped !");
                    dialogueToAnalyse.isStructurallyOk=false;
                    break;
                }}}
        else{
            Debug.LogWarning("Dialogue NOT valid : " + dialogueToAnalyse.name);
            dialogueToAnalyse.isStructurallyOk=false;
        }
    }   

    public static bool isHeroesMatchingForStartingDialogues(DialogueScriptableObject StartingLevelDialogues){
        if (StartingLevelDialogues.alreadyPlayed==true){
            Debug.LogWarning(StartingLevelDialogues.name + " already played ! It will not be displayed");
            return false;
        }
        List<HeroesManager.Hero> heroesAlive = HeroesManager.Instance.ListOfHeroesAlive;
        if(isHeroStateModifierOK(StartingLevelDialogues)){
            for(int i = 0 ; i < StartingLevelDialogues.dialogues.Length ; i++){
                if(!heroesAlive.Contains(StartingLevelDialogues.dialogues[i].HeroName)){
                    Debug.LogWarning(StartingLevelDialogues.name + " can't be displayed, because some heroes in the dialogues are not alive");
                    return false;
                }
            }
        }
        else{
            return false;
        }
        return true;
    }

    public static bool isHeroesMatchingForOnDeathDialogues(DialogueScriptableObject onDeathDialogues, HeroesManager.Hero DeadHero){
        if (onDeathDialogues.alreadyPlayed==true){
            Debug.LogWarning(onDeathDialogues.name + " already played ! It will not be displayed");
            return false;
        }
        if (onDeathDialogues.triggerDialogueOnDeath==false || DeadHero != onDeathDialogues.deadHero){
            Debug.Log(onDeathDialogues.name + ": This dialogue is not triggered on death, or is triggered for another hero death");
            return false;
        }
        for(int i = 0 ; i < onDeathDialogues.dialogues.Length ; i++){
            if(onDeathDialogues.dialogues[i].HeroName == DeadHero){
                Debug.LogWarning(onDeathDialogues.name + " can't be displayed because your hero who just died is talking in this dialogue");
                return false;
            }
        }
        if(!isHeroStateModifierOK(onDeathDialogues)){
            Debug.Log("Dialogue can't be displayed because not enough heroes alive, or trying to make a dialogue with an unavailable hero");
            return false;
        }
        return true;
    }

    public static bool isHeroesMatchingForEvacuatingDialogues(DialogueScriptableObject EvacuatingLevelDialogues){
        if (EvacuatingLevelDialogues.alreadyPlayed==true){
            Debug.LogWarning(EvacuatingLevelDialogues.name + " already played ! It will not be displayed");
            return false;
        }
        //We create a temporary list cointain 
        List<HeroesManager.Hero> heroesEvacuatedAndAlive = new List<HeroesManager.Hero>();
        foreach (HeroesManager.Hero heroEscaped in HeroesManager.Instance.ListOfEscapedHeros){
            heroesEvacuatedAndAlive.Add(heroEscaped);
        }
        foreach (HeroesManager.Hero heroAlive in HeroesManager.Instance.ListOfHeroesAlive){
            heroesEvacuatedAndAlive.Add(heroAlive);
        }

        if(isHeroStateModifierOK(EvacuatingLevelDialogues)){
            for(int i = 0 ; i < EvacuatingLevelDialogues.dialogues.Length ; i++){
                if(!heroesEvacuatedAndAlive.Contains(EvacuatingLevelDialogues.dialogues[i].HeroName)){
                    Debug.LogWarning(EvacuatingLevelDialogues.name + " can't be displayed, because some heroes in the dialogues are not alive");
                    return false;
                }
            }
        }
        else{
            return false;
        }
        return true;
    }

    public static bool isHeroesMatchingForEndingDialogues(){
        return true;
    }

    private static bool isHeroStateModifierOK(DialogueScriptableObject dialogue){
        int any1Found=0;
        int any2Found=0;
        int any3Found=0;
        int any4Found=0;
        int currentFound=0;
        int numberOfDifferentHeroesNeeded=0;
        HeroesManager.Hero any1Mapping = HeroesManager.Hero.NONE;
        HeroesManager.Hero any2Mapping = HeroesManager.Hero.NONE;
        HeroesManager.Hero any3Mapping = HeroesManager.Hero.NONE;
        HeroesManager.Hero any4Mapping = HeroesManager.Hero.NONE;

        List<HeroesManager.Hero> tmpHeroesAlive = new List<HeroesManager.Hero>(HeroesManager.Instance.ListOfHeroesAlive);
        tmpHeroesAlive.AddRange(HeroesManager.Instance.ListOfEscapedHeros);

        for (int i = 0; i < dialogue.dialogues.Length; i++)
        {
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.CURRENT){
            currentFound=1;
            }
        }
        for (int i = 0; i < dialogue.dialogues.Length; i++)
        {
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.CURRENT){
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY1){
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY2){
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY3){
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY4){
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.NONE){
                Debug.LogError("The dialogue '" + dialogue.name + "' has a hero name assigned to NONE, it should be forbiden!");
                return false;}
            if(!tmpHeroesAlive.Contains(dialogue.dialogues[i].HeroName)){
                return false;
            }
            
        }
        for (int i = 0; i < dialogue.dialogues.Length; i++)
        {
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.CURRENT){
                currentFound=1;
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY1){
                any1Found=1;
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY2){
                any2Found=1;
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY3){
                any3Found=1;
                continue;}
            if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY4){
                any4Found=1;
                continue;}
            if(currentFound!=1){
                Debug.Log("Liste heroe alive tmp " + tmpHeroesAlive);
                Debug.Log("Hero en cours d'analyse : " + dialogue.dialogues[i].HeroName);
                Debug.Log("Index du Heros en cours de delete du tmp " + tmpHeroesAlive.IndexOf(dialogue.dialogues[i].HeroName));
                if (tmpHeroesAlive.IndexOf(dialogue.dialogues[i].HeroName) != -1){
                    tmpHeroesAlive.RemoveAt(tmpHeroesAlive.IndexOf(dialogue.dialogues[i].HeroName));
                }
            }
            if(currentFound==1 && dialogue.dialogues[i].HeroName!=HeroesManager.Instance.CurrentHero){
                if (tmpHeroesAlive.IndexOf(dialogue.dialogues[i].HeroName) != -1){
                    tmpHeroesAlive.RemoveAt(tmpHeroesAlive.IndexOf(dialogue.dialogues[i].HeroName));
                }
            }
        }
        numberOfDifferentHeroesNeeded = any1Found + any2Found + any3Found + any4Found + currentFound;
        if(numberOfDifferentHeroesNeeded!=0){
            if (tmpHeroesAlive.Count>=numberOfDifferentHeroesNeeded){
                
                if (currentFound==1){
                    if(HeroesManager.Instance.CurrentHero==HeroesManager.Hero.NONE){
                        Debug.LogError("Current Hero is NONE, can't display a dialogue with CURRENT hero");
                        return false;
                    }
                    for (int i=0; i<dialogue.dialogues.Length; i++){
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.CURRENT){
                            dialogue.dialogues[i].HeroName=HeroesManager.Instance.CurrentHero;
                        }
                    }
                    int indexCurrHero = tmpHeroesAlive.IndexOf(HeroesManager.Instance.CurrentHero);
                    if (indexCurrHero >= 0){
                        tmpHeroesAlive.RemoveAt(indexCurrHero);
                    }
                    else{
                        Debug.LogError("Can't Find the current hero in the list of heroes alive... returned value of IndexOf function is inferior to zero...");
                    }
                    for (int i=0; i < dialogue.dialogues.Length; i++){
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY1){
                            if (any1Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any1Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any1Mapping;
                            }
                        continue;}
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY2){
                            if (any2Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any2Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any2Mapping;
                            }
                        continue;}
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY3){
                            if (any3Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any3Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any3Mapping;
                            }
                        continue;}
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY4){
                            if (any4Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any4Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any4Mapping;
                            }
                        }
                    } 
                }
                else{
                    for (int i=0; i < dialogue.dialogues.Length; i++){
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY1){
                            if (any1Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any1Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any1Mapping;
                            }
                        continue;}
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY2){
                            if (any2Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any2Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any2Mapping;
                            }
                        continue;}
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY3){
                            if (any3Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any3Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any3Mapping;
                            }
                        continue;}
                        if(dialogue.dialogues[i].HeroName == HeroesManager.Hero.ANY4){
                            if (any4Mapping==HeroesManager.Hero.NONE){
                                dialogue.dialogues[i].HeroName=tmpHeroesAlive[0];
                                any4Mapping=tmpHeroesAlive[0];
                                tmpHeroesAlive.RemoveAt(0);
                            }
                            else{
                                dialogue.dialogues[i].HeroName=any4Mapping;
                            }
                        }
                    }
                } 
            }
            else{
            return false;
            }
        }
        return true;
    }
}
