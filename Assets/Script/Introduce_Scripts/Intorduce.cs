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
    private Coroutine typingCoroutine = null; // 存当前运行的TypeText

    void Start()
    {
        if (textsToDisplay.Length > 0)
        {
            typingCoroutine = StartCoroutine(TypeText(textsToDisplay[idx]));
        }
    }

    public void IndPlus()
    {
        if (idx + 1 < textsToDisplay.Length) // 确保idx不会超出数组范围
        {
            idx = (idx + 1);
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // 停止当前正在运行的TypeText协程
            }
            typingCoroutine = StartCoroutine(TypeText(textsToDisplay[idx])); // 启动新的TypeText协程
        }
        else
        {
            // 可以在这里处理数组结束后的逻辑，例如循环开始或者显示一个特定的消息
            // idx = 0; // 重置idx为0来循环文字
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
