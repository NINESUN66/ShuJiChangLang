using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Introduce : MonoBehaviour
{
    public Text showText;

    public string[] textsToDisplay;
    public float Speed = 0.1f;
    private int idx = 0;
    private Coroutine typingCoroutine = null; // �浱ǰ���е�TypeText

    void Start()
    {
        if (textsToDisplay.Length > 0)
        {
            typingCoroutine = StartCoroutine(TypeText(textsToDisplay[idx]));
        }
    }

    public void IndPlus()
    {
        if (idx + 1 < textsToDisplay.Length) // ȷ��idx���ᳬ�����鷶Χ
        {
            idx = (idx + 1);
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // ֹͣ��ǰ�������е�TypeTextЭ��
            }
            typingCoroutine = StartCoroutine(TypeText(textsToDisplay[idx])); // �����µ�TypeTextЭ��
        }
        else
        {
            // ���������ﴦ�������������߼�������ѭ����ʼ������ʾһ���ض�����Ϣ
            // idx = 0; // ����idxΪ0��ѭ������
            // typingCoroutine = StartCoroutine(TypeText(textsToDisplay[idx]));
        }
    }

    IEnumerator TypeText(string textToType)
    {
        showText.text = ""; // clear the text
        for (int i = 0; i < textToType.Length; i++)
        {
            showText.text += textToType[i];
            yield return new WaitForSeconds(Speed);
        }
    }
}
