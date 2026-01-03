using UnityEngine;
using UnityEngine.UI;

public class UIbgSwitch : MonoBehaviour
{
    public Image bgImage;
    public Sprite bg1;
    public Sprite bg2;
    public float switchTime = 0.2f;

    float timer = 0f;
    bool use1 = true;

    void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer >= switchTime)
        {
            if (use1)
            {
                bgImage.sprite = bg2;
            }
            else
            {
                bgImage.sprite = bg1;
            }

            use1 = !use1;
            timer = 0f;
        }
    }
}
