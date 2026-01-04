using System.Collections;
using UnityEngine;

public class BeatHint : Singleton<BeatHint>
{
    public Color normalColor, hintColor;
    [Header("Move Settings")]
    public float moveUpDistance = 1f;
    public float moveDuration = 1f;

    [Header("Rotation Settings")]
    public float rotateAngle = 10f; //less than 10 degrees

    Vector3 startPosition;
    Quaternion startRotation;

    bool rotateLeft = true;
    Coroutine coro;
    SpriteRenderer spr;

    protected override void Awake()
    {
        base.Awake();
        spr=GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        spr.color=normalColor;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Beat() {
        StartCoroutine(DelayAction(() =>
        {
            if(coro!=null)
                StopCoroutine(coro);
            coro=StartCoroutine(MoveRoutine());
        }, 5f-moveDuration*2)); //default delay=5s
    }

    public IEnumerator DelayAction(System.Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

    IEnumerator MoveRoutine()
    {
        spr.color=normalColor;
        //direction: left / right
        float direction = rotateLeft ? -1f : 1f;

        Vector3 targetPos = startPosition + Vector3.up * moveUpDistance;
        Quaternion targetRot =
            Quaternion.Euler(
                startRotation.eulerAngles.x,
                startRotation.eulerAngles.y,
                startRotation.eulerAngles.z + rotateAngle * direction
            );

        // rise + rotate
        yield return MoveAndRotate(startPosition, targetPos, startRotation, targetRot);

        spr.color=hintColor;

        // fall + rotate back
        yield return MoveAndRotate(targetPos, startPosition, targetRot, startRotation);

        // switch left/right
        rotateLeft = !rotateLeft;
        spr.color=normalColor;
        coro=null;
    }

    IEnumerator MoveAndRotate(
        Vector3 fromPos,
        Vector3 toPos,
        Quaternion fromRot,
        Quaternion toRot
    )
    {
        float time = 0f;

        while (time < moveDuration)
        {
            float t = time / moveDuration;

            transform.position = Vector3.Lerp(fromPos, toPos, t);
            transform.rotation = Quaternion.Lerp(fromRot, toRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = toPos;
        transform.rotation = toRot;
    }
}
