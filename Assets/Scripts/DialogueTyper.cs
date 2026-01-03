using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueTyper : Singleton<DialogueTyper>
{
    public TMP_Text text;
    public float typeInterval = 0.05f;

    Coroutine typingCoroutine;

    public void PlayDialogue(string content)
    {
        // 如果上一次还在打字，先停掉
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeRoutine(content));
    }

    public IEnumerator TypeRoutine(string content)
    {
        text.text = string.Empty;

        foreach (char c in content)
        {
            text.text += c;
            yield return new WaitForSeconds(typeInterval);
        }

        typingCoroutine = null;
    }

    public void SetDiaplay(bool val)
    {
        text.transform.parent.gameObject.SetActive(val);
    }
}