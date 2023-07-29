using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeOverlay : MonoBehaviour
{
    [SerializeField]
    private float defaultFadeTime = 0.25f;
    private static float fadeTime = 0.25f;
    private static Image fadeImage;

    private void OnEnable()
    {
        fadeImage = GetComponent<Image>();
        fadeTime = defaultFadeTime;
    }

    public static void FadeIn(System.Action callback = null)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(0, fadeTime).OnComplete(() =>
        {
            fadeImage.raycastTarget = false;
            if (callback != null)
            {
                callback();
            }
        });
    }

    public static void FadeOut(System.Action callback = null)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(1, fadeTime).OnComplete(() =>
        {
            fadeImage.raycastTarget = false;
            if (callback != null)
            {
                callback();
            }
        });
    }

    public static void FadeIn(float fadeTime, System.Action callback = null)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(0, fadeTime).OnComplete(() =>
        {
            fadeImage.raycastTarget = false;
            if (callback != null)
            {
                callback();
            }
        });
    }

    public static void FadeOut(float fadeTime, System.Action callback = null)
    {
        fadeImage.raycastTarget = true;
        fadeImage.DOFade(1, fadeTime).OnComplete(() =>
        {
            fadeImage.raycastTarget = false;
            if (callback != null)
            {
                callback();
            }
        });
    }
}
