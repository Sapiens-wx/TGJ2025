using System.Collections;
using UnityEngine;

public class Action1Request : ActionRequestBase
{
    public float tipsDisplayDuration;
    public GameObject[] tips;
    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            OnActionPerformed();
        }
    }
    protected override void OnSuccess()
    {
        base.OnSuccess();
        GameManager.inst.StartCoroutine(DisplayTips());
    }
    IEnumerator DisplayTips()
    {
        foreach(GameObject tip in tips)
            tip.SetActive(true);
        yield return new WaitForSeconds(tipsDisplayDuration);
        foreach(GameObject tip in tips)
            tip.SetActive(false);
    }
}