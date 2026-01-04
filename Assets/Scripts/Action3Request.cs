using UnityEngine;

public class Action3Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(GameManager.inst.action3_key)) {
            OnActionPerformed();
        }
    }
    protected override void OnSuccess()
    {
        base.OnSuccess();
        if(curActionCount==1)
            GameManager.inst.playerAnimator.SetTrigger($"action3_1");
        else if (curActionCount == 2) {
            GameManager.inst.playerAnimator.SetTrigger($"action3_2");
            StarEffect.inst.PlayEffect();
        }
    }
}