using UnityEngine;
using UnityEngine.UI;
public class TextContent : MonoBehaviour
{
    public string[] BookText = new string[7];
    public Text Text1;
    int n = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BookText[0] = "���ߣ�����     ʱ�䣺��Ԫǰ2��������Ԫ2����֮��\r\nʱ�ڣ�����     ���ݣ��ⲿ�����ԡ�������Ϊ��ģ��漰����ѧ�е��㷨�����̡����εȶ����������ʮ�ֹ㷺��";
        BookText[1] = "";
        BookText[2] = "";
        BookText[3] = "";
        BookText[4] = "";
        BookText[5] = "";
        BookText[6] = "";
        ShowText1(n);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void  ShowText1(int n)
    {
        Text1.text = BookText[n];
    }
}
