using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Button startGameButton;
    public AudioSource audio1, audio2;
    public bool withTutorial;
    public Animator playerAnimator, aiAnimator;
    public float[] playerDelay; //base delay: 5 sec
    public float[] aiDelay; //base delay: 5 sec
    // the action requests 1-3
    public PlayableDirector[] actions;
    public CinemachineVirtualCamera[] cams;
    [Header("Key Binds")]
    public KeyCode action1_key;
    public KeyCode action2_key;
    public KeyCode action3_key;

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
    void Update() {
        if(ActionRequestBase.activeActionRequest==null && TutorialActionRequest.activeActionRequest == null) {
            if(Input.GetKeyDown(action1_key))
                playerAnimator.SetTrigger("action1");
            else if (Input.GetKeyDown(action2_key)) {
                playerAnimator.SetTrigger("action2_1");
            } else if (Input.GetKeyDown(action3_key)) {
                playerAnimator.SetTrigger("action3");
            }
        }
    }
    public void StartActualGame() {
        startGameButton.gameObject.SetActive(true);
    }
    public void OnClickStartGame()
    {
        startGameButton.gameObject.SetActive(false);
        audio1.Play();
        audio2.Play();
        gamePlayDirector.Play();
    }
    public void DelayPlayCue(int index) {
        StartCoroutine(DelayAction(PlayCue, index, aiDelay[index]));
    }
    public void DelayPlayAction(int index) {
        StartCoroutine(DelayAction(PlayAction, index, playerDelay[index]));
    }
    public void PlayCue(int index) {
        aiAnimator.SetTrigger($"action{index+1}_ai");
        if(index!=1)
            SetCam(index);
    }
    public void PlayAction(int index) {
        actions[index].Play();
    }
    public void PlayPlayerAnimation(int index) {
        playerAnimator.SetTrigger($"action{index+1}");
    }
    public void SetCam(int index)
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
