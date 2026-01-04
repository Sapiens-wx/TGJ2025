using UnityEngine;

public abstract class ActionRequestBase : MonoBehaviour
{
    public int actionCount = 1;
    public AudioSource failAudio;
    public enum ActionState
    {
        None,
        Successful,
        Failed
    }
    ActionState actionState;
    bool requestActive=false;
    protected int curActionCount;
    public static ActionRequestBase activeActionRequest;
    void Awake()
    {
        curActionCount=0;
    }
    void OnEnable() {
        if(curActionCount>=actionCount) curActionCount=0;
        actionState=ActionState.None;
        requestActive=false;
        activeActionRequest=this;
    }
    void OnDisable() {
        if (actionState != ActionState.Successful) {
            OnFailed();
        }
        activeActionRequest=null;
    }
    protected virtual void OnSuccess() {
        if(actionState==ActionState.Successful) return;
        curActionCount++;
        actionState=ActionState.Successful;
    }
    protected virtual void OnFailed() {
        if(actionState==ActionState.Failed) return;
        actionState=ActionState.Failed;
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
}