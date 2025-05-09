using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public GameObject FaderScreen;
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
            FadeIn();
        Invoke("FadeOff", 2);
    }

   public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut)); 
    }

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }
    public void FadeOff()
    {
        FaderScreen.SetActive(false);
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        Color newColor;
        float timer = 0;
        while (timer < fadeDuration)
        {
            newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        newColor = fadeColor;
        newColor.a = alphaOut;
        rend.material.SetColor("_Color", newColor);
    }

}
