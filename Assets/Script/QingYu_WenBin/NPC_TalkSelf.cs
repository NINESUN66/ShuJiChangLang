using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_TalkSelf : MonoBehaviour
{
    public TextMeshPro dialogueText; // 用于显示对话的文本组件
    public string[] dialogueLines; // 存储对话内容的字符串数组
    private int currentLine = 0; // 当前显示的对话行数
    public float dialogueInterval = 5f; // 对话间隔时间（秒）

    void Start()
    {
        dialogueLines = new string[dialogueText.text.Split('\n').Length];

       dialogueLines = dialogueText.text.Split('\n');

       StartCoroutine(ShowDialogue());
    }

    IEnumerator ShowDialogue()
    {
        
        
        while (true)
        {
            dialogueText.text = dialogueLines[currentLine];
            
            currentLine++;
            if (currentLine >= dialogueLines.Length)
            {
                currentLine = 0;
            }
            yield return new WaitForSeconds(dialogueInterval);
            
        }
    }
}
