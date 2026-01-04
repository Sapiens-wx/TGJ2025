using UnityEngine;

public class TutorialActionRequest : MonoBehaviour
{
    public int actionCount;
    public int actionIndex;
    public AudioSource failAudio;
    ActionState actionState;
    bool requestActive=false;
    KeyCode key;
    int curActionCount=0;
    public static TutorialActionRequest activeActionRequest;
    void Awake()
    {
        if(actionIndex==1)
            key=GameManager.inst.action1_key;
        else if(actionIndex==2)
            key=GameManager.inst.action2_key;
        else if(actionIndex==3)
            key=GameManager.inst.action3_key;
    }
    void OnEnable() {
        actionState=ActionState.None;
        requestActive=false;
        if(curActionCount>=actionCount) curActionCount=0;
        activeActionRequest=this;
    }
    void OnDisable() {
        if (actionState != ActionState.Successful) {
            OnFailed();
        }
        activeActionRequest=null;
    }
    void Update()
    {
        if(Input.GetKeyDown(key))
            OnActionPerformed();
    }
    protected virtual void OnSuccess() {
        if(actionState==ActionState.Successful) return;
        actionState=ActionState.Successful;
        ++curActionCount;
        if(actionCount==1)
            GameManager.inst.playerAnimator.SetTrigger($"action{actionIndex}");
        else
            GameManager.inst.playerAnimator.SetTrigger($"action{actionIndex}_{curActionCount}");
        if(actionIndex==3 &&curActionCount==2)
            StarEffect.inst.PlayEffect();
    }
    protected virtual void OnFailed() {
        if(actionState==ActionState.Failed) return;
        actionState=ActionState.Failed;
        Tutorial.inst.actionState=Tutorial.ActionState.Fail;
        GameManager.inst.playerAnimator.SetTrigger("fail");
        failAudio.Play();
        curActionCount=0;
    }
    protected void OnActionPerformed() {
        if(actionState==ActionState.Failed) return;
        if (requestActive && actionState == ActionState.None) {
            OnSuccess();
        } else {
            OnFailed();
        }
    }
    public void SetRequestActive(bool value) {
        requestActive=value;
    }
    public enum ActionState
    {
        None,
        Successful,
        Failed
    }
}