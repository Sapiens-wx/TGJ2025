using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Tutorial : Singleton<Tutorial>
{
    public PlayableDirector[] actions;
    public DialogueInfo[] pre_action1_dialogue;
    public DialogueInfo[] pre_action2_dialogue;
    public DialogueInfo[] pre_action3_dialogue;
    public DialogueInfo[] post_action3_dialogue;
    public GameObject tutorialBG;

    [HideInInspector][NonSerialized] public ActionState actionState;
    void Start()
    {
        if (GameManager.inst.withTutorial) {
            tutorialBG.SetActive(true);
            StartCoroutine(TutorialCoro());
        } else {
            tutorialBG.SetActive(false);
            DialogueTyper.inst.SetDiaplay(false);
        }
    }
    public void SetActionRequestSuccess()
    {
        if(Tutorial.inst.actionState==Tutorial.ActionState.Fail) return;
        Tutorial.inst.actionState=Tutorial.ActionState.Success;
    }
    IEnumerator TutorialCoro() {
        IEnumerator routine;
        List<DialogueInfo[]> dialogues=new List<DialogueInfo[]>() {
            pre_action1_dialogue,
            pre_action2_dialogue,
            pre_action3_dialogue
        };
        for(int i=0;i<dialogues.Count;++i) {
            DialogueTyper.inst.SetDiaplay(true);
            DialogueInfo[] dialogue=dialogues[i];
            // dialogue
            foreach(DialogueInfo s in dialogue)
            {
                routine=DialogueTyper.inst.TypeRoutine(s);
                while(routine.MoveNext())
                    yield return routine.Current;
                while (true)
                {
                    if(Input.anyKeyDown)
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
                    DialogueTyper.inst.SetDiaplay(true);
                    routine=DialogueTyper.inst.TypeRoutine("let's try again!");
                    while(routine.MoveNext())
                        yield return routine.Current;
                    DialogueTyper.inst.SetDiaplay(false);
                } else
                    break;
            } while(true);
            yield return 0;
        }
        // dialogue before starting the game
        DialogueTyper.inst.SetDiaplay(true);
        foreach(DialogueInfo s in post_action3_dialogue)
        {
            routine=DialogueTyper.inst.TypeRoutine(s);
            while(routine.MoveNext())
                yield return routine.Current;
            while (true)
            {
                if(Input.anyKeyDown)
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