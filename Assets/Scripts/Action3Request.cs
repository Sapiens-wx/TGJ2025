using UnityEngine;

public class Action3Request : ActionRequestBase
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.C)) {
            OnActionPerformed();
        }
    }
}