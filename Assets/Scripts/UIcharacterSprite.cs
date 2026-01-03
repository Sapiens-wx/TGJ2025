using UnityEngine;
using UnityEngine.UI;

public class UICharacterSprite : MonoBehaviour
{
    public Image characterImage;

    public Sprite[] characterSprites;

    void Start()
    {
        if (characterSprites.Length > 0)
        {
            characterImage.sprite = characterSprites[0];
        }
    }

    public void SetSprite(int index)
    {
        if (index < 0)
        {
            return;
        }

        if (index >= characterSprites.Length)
        {
            return;
        }

        characterImage.sprite = characterSprites[index];
    }
}
