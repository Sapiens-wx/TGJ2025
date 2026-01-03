using System.Collections;
using UnityEngine;

public class BeatHint : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveUpDistance = 1f;
    public float moveDuration = 1f;

    [Header("Rotation Settings")]
    public float rotateAngle = 10f; //less than 10 degrees

    Vector3 startPosition;
    Quaternion startRotation;

    bool rotateLeft = true;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
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

            // fall + rotate back
            yield return MoveAndRotate(targetPos, startPosition, targetRot, startRotation);

            // switch left/right
            rotateLeft = !rotateLeft;
        }
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
