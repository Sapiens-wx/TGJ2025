using UnityEngine;
using System.Collections;

public class StarEffect : Singleton<StarEffect>
{
    public Transform star;
    public Transform circle;
    public float circleDuration;
    public float circleScaleAmount;
    public float starDuration;
    public float starXDist;
    public float starYDist, starYSkewFactor;
    void Start()
    {
        star.gameObject.SetActive(false);
        circle.gameObject.SetActive(false);
    }
    public void PlayEffect() {
        StartCoroutine(_StarEffect());
        StartCoroutine(CircleEffect());
    }
    IEnumerator _StarEffect() {
        Vector3 oldPos=star.localPosition;
        star.gameObject.SetActive(true);
        WaitForFixedUpdate wait=new WaitForFixedUpdate();
        float time=Time.time;
        float endTime=Time.time+starDuration;
        while (time < endTime)
        {
            float normalizedTime=1f-(endTime-time)/starDuration;
            Vector3 pos=star.localPosition;
            pos.x=Mathf.Lerp(oldPos.x, oldPos.x+starXDist, normalizedTime);
            pos.y=oldPos.y+Mathf.Sin(Mathf.PI*normalizedTime)*starYDist-normalizedTime*starYSkewFactor;
            star.localPosition=pos;
            time+=Time.fixedDeltaTime;
            yield return wait;
        }
        star.gameObject.SetActive(false);
        star.localPosition=oldPos;
    }
    IEnumerator CircleEffect() {
        circle.gameObject.SetActive(true);
        Vector3 oldScale=circle.localScale;
        WaitForFixedUpdate wait=new WaitForFixedUpdate();
        float time=Time.time;
        float endTime=Time.time+circleDuration;
        while (time < endTime)
        {
            float normalizedTime=1f-(endTime-time)/circleDuration;
            circle.localScale*=circleScaleAmount;
            time+=Time.fixedDeltaTime;
            yield return wait;
        }
        circle.gameObject.SetActive(false);
        circle.localScale=oldScale;
    }
}