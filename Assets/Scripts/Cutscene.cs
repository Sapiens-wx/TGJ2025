using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cutscene : Singleton<Cutscene>
{
    [Header("in game cutscene")]
    public Image inGameSpr;
    public Sprite[] inGameFrames;
    public float inGameFrameDuration;
    public float inGameStartTime;
    [Header("end game cutscene")]
    public Image endGameSpr;
    public Sprite[] endGameFrames;
    public int endGameFrameDuration;

    void Start()
    {
        inGameSpr.gameObject.SetActive(false);
        endGameSpr.gameObject.SetActive(false);
    }

    public void PlayInGameCutscene()
    {
        StartCoroutine(IEnumInGame());
    }
    public void PlayEndGameCutscene()
    {
        StartCoroutine(IEnumEndGame());
    }
    IEnumerator IEnumInGame()
    {
        inGameSpr.gameObject.SetActive(true);
        foreach(Sprite img in inGameFrames)
        {
            inGameSpr.sprite=img;
            yield return new WaitForSeconds(inGameFrameDuration);
        }
        inGameSpr.gameObject.SetActive(false);
    }
    IEnumerator IEnumEndGame()
    {
        endGameSpr.gameObject.SetActive(true);
        foreach(Sprite img in endGameFrames)
        {
            endGameSpr.sprite=img;
            yield return new WaitForSeconds(endGameFrameDuration);
        }
    }
}