using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShowHowToPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    [SerializeField] private Text textMeshPro;
    public float fadeDuration = 1f;
    private bool isFading;

    // 鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerEnter == button.gameObject)
        {
            Debug.Log("鼠标进入");
            if (isFading)
            {
                StopAllCoroutines(); // 停止当前的渐变
            }
            StartCoroutine(FadeText(textMeshPro, 0f, 1f));
        }
    }

    // 鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标离开");
        if (isFading)
        {
            StopAllCoroutines(); // 停止当前的渐变
        }
        StartCoroutine(FadeText(textMeshPro, 1f, 0f)); // 将textMeshPro逐渐显示出来
    }

    // 文本透明度渐变
    private System.Collections.IEnumerator FadeText(Text text, float startAlpha, float targetAlpha)
    {
        isFading = true;
        float timer = 0f;
        Color startColor = text.color;
        Color targetColor = new(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        text.color = targetColor;
        isFading = false;
    }
}
