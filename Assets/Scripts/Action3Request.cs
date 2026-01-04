using UnityEngine;

public class Action3Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            OnActionPerformed();
        }
    }
    protected override void OnSuccess()
    {
        base.OnSuccess();
        if(curActionCount==1)
            GameManager.inst.PlayPlayerAnimation(2);
    }
}