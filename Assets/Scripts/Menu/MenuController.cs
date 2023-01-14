using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public CanvasGroup mainCanvas;
    public Button customizeButton;
    public float fadeTime;

    void Start()
    {
        customizeButton.onClick.AddListener(FadeOut);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(mainCanvas, mainCanvas.alpha, 0.0f));
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(mainCanvas, mainCanvas.alpha, 1.0f));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        if (end > 0 && !cg.interactable)
        {
            cg.interactable = true;
        }
        else if (end == 0 && cg.interactable)
        {
            cg.interactable = false;
        }

        float elapsedTime = 0.0f;
        float totalTime = fadeTime;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / totalTime);
            yield return null;
        }
    }
}