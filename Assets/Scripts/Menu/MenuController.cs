using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public CanvasGroup mainCanvas;
    public CanvasGroup customCanvas;
    public Button customizeButton;
    public Button backButton;
    public Button bodyColorButton;
    public Button fanColorButton;
    public GameObject bodyColorButtons;
    public GameObject fanColorButtons;
    public GameObject mainCustomMenu;
    public GameObject mainCanvVisible;
    public GameObject customCanvVisible;
    public GameObject customCanvType;
    public GameObject mainCanvType;
    public Text comingSoonText;

    public GameObject droneBody;
    public List<GameObject> droneFans;

    public float fadeTime;
    public float fadeInDelay;

    void Start()
    {
        mainCanvas.gameObject.SetActive(true);
        customCanvas.gameObject.SetActive(false);
        bodyColorButtons.gameObject.SetActive(false);
        fanColorButtons.gameObject.SetActive(false);
        customCanvas.alpha = 0f;
        comingSoonText.gameObject.SetActive(false);
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, CanvasGroup otherCg)
    {
        cg.interactable = false;
        

        float elapsedTime = 0.0f;
        float totalTime = fadeTime;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / totalTime);
            yield return null;
        }
        cg.interactable = true;
    }

    public IEnumerator FadeText(Text tx, Color start, Color end, Button fadeButton)
    {
        fadeButton.interactable = false;
        float elapsedTime = 0.0f;
        float totalTime = fadeTime;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
            tx.color = Color.Lerp(start, end, elapsedTime / totalTime);
            yield return null;
        }
        tx.gameObject.SetActive(false);
        tx.color = start;
        fadeButton.interactable = true;
    }

    IEnumerator SwitchCanvas(CanvasGroup outCanvas, CanvasGroup inCanvas, GameObject canvType, float delay)
    {
        inCanvas.interactable = false;
        outCanvas.interactable = false;
        yield return StartCoroutine(FadeCanvasGroup(outCanvas, outCanvas.alpha, 0.0f, inCanvas)); // fade out
        outCanvas.gameObject.SetActive(false);
        inCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(delay);

        yield return StartCoroutine(FadeCanvasGroup(inCanvas, inCanvas.alpha, 1.0f, inCanvas)); // fade in 
        inCanvas.gameObject.SetActive(true);

        if (canvType.name == "Customize") 
        {
            bodyColorButton.gameObject.SetActive(true);
            fanColorButton.gameObject.SetActive(true);
        }
        
    }

    public void DisplayColors(Button selectedButton)
    {
        if (selectedButton == bodyColorButton)
        {
            bodyColorButtons.gameObject.SetActive(true);
        }
        else
        {
            fanColorButtons.gameObject.SetActive(true);
        }
        mainCustomMenu.gameObject.SetActive(false);
    }

    public void switchScreens()
    {
        if (mainCanvas.gameObject.activeInHierarchy)
        {
            StartCoroutine(SwitchCanvas(mainCanvas, customCanvas, customCanvType, fadeInDelay));
            mainCanvVisible.name = "False";
            customCanvVisible.name = "True";
        }
        else if (mainCustomMenu.activeInHierarchy)
        {
            StartCoroutine(SwitchCanvas(customCanvas, mainCanvas, mainCanvType, fadeInDelay));
            customCanvVisible.name = "False";
            mainCanvVisible.name = "True";
        }
        else if (bodyColorButtons.activeInHierarchy || fanColorButtons.activeInHierarchy)
        {
            bodyColorButtons.gameObject.SetActive(false);
            fanColorButtons.gameObject.SetActive(false);

            mainCustomMenu.SetActive(true);
        }
    }

    public void setColors(GameObject mat)
    {
        string part = mat.transform.parent.parent.name;

        if (part == bodyColorButtons.name)
        {
            droneBody.GetComponent<MeshRenderer>().material = mat.GetComponent<Image>().material;
        }
        else if (part == fanColorButtons.name)
        {
            foreach (GameObject fan in droneFans)
            {
                fan.GetComponent<MeshRenderer>().material = mat.GetComponent<Image>().material;
            }
        }
    }

    public void comingSoon(Button fadeButton)
    {
        comingSoonText.gameObject.SetActive(true);

        Color newAlpha = new Color(comingSoonText.color.r, comingSoonText.color.g, comingSoonText.color.b, 0);

        StartCoroutine(FadeText(comingSoonText, comingSoonText.color, newAlpha, fadeButton));
    }
}