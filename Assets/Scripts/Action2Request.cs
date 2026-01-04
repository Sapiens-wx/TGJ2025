using UnityEngine;

public class Action2Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(GameManager.inst.action2_key)) {
            OnActionPerformed();
        }
    }
    protected override void OnSuccess()
    {
        base.OnSuccess();
        GameManager.inst.playerAnimator.SetTrigger($"action2_{curActionCount}");
    }
}