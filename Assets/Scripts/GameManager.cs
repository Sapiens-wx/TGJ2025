using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
    public bool withTutorial;
    public Animator playerAnimator, aiAnimator;
    public float[] playerDelay; //base delay: 5 sec
    public float[] aiDelay; //base delay: 5 sec
    // the action requests 1-3
    public PlayableDirector[] actions;
    public CinemachineVirtualCamera[] cams;

    PlayableDirector gamePlayDirector;
    protected override void Awake()
    {
        base.Awake();
        gamePlayDirector=GetComponent<PlayableDirector>();
    }
    void Start()
    {
        if(!withTutorial)
            StartActualGame();
    }
    public void StartActualGame() {
        gamePlayDirector.Play();
    }
    public void DelayPlayCue(int index) {
        StartCoroutine(DelayAction(PlayCue, index, aiDelay[index]));
    }
    public void DelayPlayAction(int index) {
        StartCoroutine(DelayAction(PlayAction, index, playerDelay[index]));
    }
    public void PlayCue(int index) {
        Debug.Log("Cue "+index.ToString());
        aiAnimator.SetTrigger($"action{index+1}_ai");
        SetCam(index);
    }
    public void PlayAction(int index) {
        Debug.Log("Action "+index.ToString());
        actions[index].Play();
    }
    public void PlayPlayerAnimation(int index) {
        playerAnimator.SetTrigger($"action{index+1}");
    }
    void SetCam(int index)
    {
        for(int i = 0; i < cams.Length; ++i) {
            cams[i].Priority=1;
        }
        cams[index].Priority=2;
    }
    IEnumerator DelayAction(System.Action<int> action, int args, float sec)
    {
        yield return new WaitForSeconds(sec);
        action(args);
    }
}
