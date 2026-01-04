using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueTyper : Singleton<DialogueTyper>
{
    public TMP_Text text;
    public Image character, introCharacter;
    public GameObject introBG;
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
        introCharacter.gameObject.SetActive(false);
        character.gameObject.SetActive(true);

        foreach (char c in content)
        {
            text.text += c;
            yield return new WaitForSeconds(typeInterval);
        }

        typingCoroutine = null;
    }

    public IEnumerator TypeRoutine(DialogueInfo dialogue)
    {
        text.text = string.Empty;
        string content=dialogue.text;
        if (dialogue.isIntro) {
            introBG.SetActive(true);
            introCharacter.gameObject.SetActive(true);
            character.gameObject.SetActive(false);
            introCharacter.sprite=dialogue.spr;
        } else {
            introBG.SetActive(false);
            introCharacter.gameObject.SetActive(false);
            character.gameObject.SetActive(true);
            character.sprite=dialogue.spr;
        }

        int frameCount=0;
        foreach (char c in content)
        {
            text.text += c;
            if (frameCount>5&&Input.GetKey(KeyCode.Space))
            {
                yield return new WaitForSeconds(typeInterval);
                break;
            }
            ++frameCount;
            yield return new WaitForSeconds(typeInterval);
        }

        text.text=content;
        typingCoroutine = null;
    }

    public void SetDiaplay(bool val)
    {
        text.transform.parent.gameObject.SetActive(val);
    }
}

[System.Serializable]
public class DialogueInfo
{
    public bool isIntro;
    public Sprite spr;
    public string text;
}