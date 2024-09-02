using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_TalkSelf : MonoBehaviour
{
    public TextMeshPro dialogueText; // ������ʾ�Ի����ı����
    public string[] dialogueLines; // �洢�Ի����ݵ��ַ�������
    private int currentLine = 0; // ��ǰ��ʾ�ĶԻ�����
    public float dialogueInterval = 5f; // �Ի����ʱ�䣨�룩

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
