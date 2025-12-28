using UnityEngine;

public class Action2Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            OnActionPerformed();
        }
    }
}