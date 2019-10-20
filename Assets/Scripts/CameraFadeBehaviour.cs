using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFadeBehaviour : MonoBehaviour
{
    Image blackScreen;

    private void Awake()
    {
        blackScreen = GetComponent<Image>();
    }

    private void Start()
    {
        FadeFromBlack();
    }

    public void FadeToBlack()
    {
        blackScreen.color = Color.black;
        blackScreen.canvasRenderer.SetAlpha(0.0f);
        blackScreen.CrossFadeAlpha(1.0f, 3.0f, false);
    }

    public void FadeFromBlack()
    {
        blackScreen.color = Color.black;
        blackScreen.canvasRenderer.SetAlpha(1.0f);
        blackScreen.CrossFadeAlpha(0.0f, 7.0f, false);
    }
}
