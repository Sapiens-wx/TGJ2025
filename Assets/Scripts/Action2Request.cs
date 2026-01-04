using UnityEngine;

public class Action2Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            OnActionPerformed();
        }
    }
    protected override void OnSuccess()
    {
        base.OnSuccess();
        if (curActionCount == 1) {
            Debug.Log("Player action2"+Time.time);
            GameManager.inst.PlayPlayerAnimation(1);
        }
    }
}