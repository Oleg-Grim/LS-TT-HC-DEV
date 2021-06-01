using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuTween : MonoBehaviour
{
    public Text TTSText;
    public bool tweening = false;

    private void OnEnable()
    {
        if(!tweening)
        {
            FadeOut();
            tweening = true;
        }
    }

    private void FadeOut()
    {
        TTSText.DOFade(0.1f, 1.0f).OnComplete(FadeIn);
    }

    private void FadeIn()
    {
        TTSText.DOFade(1.0f, 1.0f).OnComplete(FadeOut);
    }
}
