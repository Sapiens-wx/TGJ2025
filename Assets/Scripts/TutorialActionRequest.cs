using UnityEngine;

public class TutorialActionRequest : MonoBehaviour
{
    public KeyCode key;
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
    void Update()
    {
        if(Input.GetKeyDown(key))
            OnActionPerformed();
    }
    protected virtual void OnSuccess() {
        if(actionState==ActionState.Successful) return;
        actionState=ActionState.Successful;
        Debug.Log($"{GetType().Name} Succeeded");
    }
    protected virtual void OnFailed() {
        if(actionState==ActionState.Failed) return;
        actionState=ActionState.Failed;
        Tutorial.inst.actionState=Tutorial.ActionState.Fail;
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
    public enum ActionState
    {
        None,
        Successful,
        Failed
    }
}