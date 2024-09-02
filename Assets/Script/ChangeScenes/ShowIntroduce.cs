using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShowIntroduce : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    private Text textMeshPro;
    private Text introduceText;
    public float fadeDuration = 1f;
    private bool isFading;

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = button.GetComponentInChildren<Text>();
        }
        if (introduceText == null)
        {
            introduceText = button.transform.GetChild(0).GetComponentInChildren<Text>();
        }
    }

    // 鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("鼠标进入");
        if (isFading)
        {
            StopAllCoroutines(); // 停止当前的渐变
        }
        StartCoroutine(FadeText(textMeshPro, 1f, 0f)); // 将textMeshPro逐渐隐藏
        StartCoroutine(FadeText(introduceText, 0f, 1f)); // 将introduceText逐渐显示出来
    }

    // 鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标离开");
        if (isFading)
        {
            StopAllCoroutines(); // 停止当前的渐变
        }
        StartCoroutine(FadeText(textMeshPro,0f, 1f)); // 将textMeshPro逐渐显示出来
        StartCoroutine(FadeText(introduceText, 1f, 0f)); // 将introduceText逐渐隐藏
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
