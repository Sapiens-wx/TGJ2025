using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tutorial : Singleton<Tutorial>
{
    public PlayableDirector[] actions;
    public string[] pre_action1_dialogue;
    public string[] pre_action2_dialogue;
    public string[] pre_action3_dialogue;
    public string[] post_action3_dialogue;
    public GameObject tutorialBG;

    [HideInInspector][NonSerialized] public ActionState actionState;
    void Start()
    {
        if (GameManager.inst.withTutorial) {
            tutorialBG.SetActive(true);
            StartCoroutine(TutorialCoro());
        } else
            tutorialBG.SetActive(false);
    }
    public void SetActionRequestSuccess()
    {
        if(Tutorial.inst.actionState==Tutorial.ActionState.Fail) return;
        Tutorial.inst.actionState=Tutorial.ActionState.Success;
    }
    IEnumerator TutorialCoro() {
        IEnumerator routine;
        List<string[]> dialogues=new List<string[]>() {
            pre_action1_dialogue,
            pre_action2_dialogue,
            pre_action3_dialogue
        };
        for(int i=0;i<dialogues.Count;++i) {
            DialogueTyper.inst.SetDiaplay(true);
            string[] dialogue=dialogues[i];
            // dialogue
            foreach(string s in dialogue)
            {
                routine=DialogueTyper.inst.TypeRoutine(s);
                while(routine.MoveNext())
                    yield return routine.Current;
                while (true)
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                        break;
                    yield return 0;
                }
                yield return 0;
            }
            DialogueTyper.inst.SetDiaplay(false);
            // action
            do
            {
                actionState=ActionState.None;
                actions[i].Play();
                yield return new WaitUntil(()=>actionState!=ActionState.None);
                yield return new WaitForSeconds(.7f); //wait for a second for the animation to end
                if (actionState == ActionState.Fail)
                {
                    actions[i].Stop();
                    routine=DialogueTyper.inst.TypeRoutine("let's try again!");
                    while(routine.MoveNext())
                        yield return routine.Current;
                } else
                    break;
            } while(true);
            yield return 0;
        }
        // dialogue before starting the game
        DialogueTyper.inst.SetDiaplay(true);
        foreach(string s in post_action3_dialogue)
        {
            routine=DialogueTyper.inst.TypeRoutine(s);
            while(routine.MoveNext())
                yield return routine.Current;
            while (true)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                    break;
                yield return 0;
            }
            yield return 0;
        }
        DialogueTyper.inst.SetDiaplay(false);
        tutorialBG.SetActive(false);
        // start the actual game
        GameManager.inst.StartActualGame();
    }
    public enum ActionState
    {
        None,
        Success,
        Fail
    }
}