using UnityEngine;

public abstract class ActionRequestBase : MonoBehaviour
{
    public enum ActionState
    {
        None,
        Successful,
        Failed
    }
    ActionState actionState;
    bool requestActive=false;
    void OnEnable() {
        actionState=ActionState.None;
        requestActive=false;
    }
    void OnDisable() {
        if (actionState != ActionState.Successful) {
            OnFailed();
        }
    }
    protected virtual void OnSuccess() {
        if(actionState==ActionState.Successful) return;
        actionState=ActionState.Successful;
        Debug.Log($"{GetType().Name} Succeeded");
    }
    protected virtual void OnFailed() {
        if(actionState==ActionState.Failed) return;
        actionState=ActionState.Failed;
        GameManager.inst.playerAnimator.SetTrigger("fail");
        Debug.Log($"{GetType().Name} Failed");
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