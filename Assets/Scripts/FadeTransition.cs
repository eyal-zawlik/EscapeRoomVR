using System.Collections;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    #region Fade Effect

    [Header("Object References")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [Header("Attributes")]
    [SerializeField] private float fadeDuration;


    private void Start()
    {
        StartFadeTransition(false);
    }

    public void StartFadeTransition(bool fadeIn = true)
    {
        StartCoroutine(DecreaseAlpha(fadeIn ? 0 : 1, fadeIn ? 1 : 0, fadeDuration));
    }
    
    private IEnumerator DecreaseAlpha(float start, float end, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            fadeCanvas.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        fadeCanvas.alpha = end;
    }

    #endregion
}
