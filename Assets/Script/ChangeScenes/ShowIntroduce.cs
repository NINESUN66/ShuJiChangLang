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

    // ������
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("������");
        if (isFading)
        {
            StopAllCoroutines(); // ֹͣ��ǰ�Ľ���
        }
        StartCoroutine(FadeText(textMeshPro, 1f, 0f)); // ��textMeshPro������
        StartCoroutine(FadeText(introduceText, 0f, 1f)); // ��introduceText����ʾ����
    }

    // ����뿪
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("����뿪");
        if (isFading)
        {
            StopAllCoroutines(); // ֹͣ��ǰ�Ľ���
        }
        StartCoroutine(FadeText(textMeshPro,0f, 1f)); // ��textMeshPro����ʾ����
        StartCoroutine(FadeText(introduceText, 1f, 0f)); // ��introduceText������
    }

    // �ı�͸���Ƚ���
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
