using UnityEngine;

public class Action1Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            OnActionPerformed();
        }
    }
}