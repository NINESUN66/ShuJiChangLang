using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timing : MonoBehaviour
{
    [SerializeField] private GameObject Start;
    [SerializeField] private Text timeShow;
    [SerializeField] private Detect detect;
    private bool is_Counting = false;
    private float mseconds = 0;
    private string time;

    private void Update()
    {
        if (detect.is_Clicked)
        {
            is_Counting = true;
        }

        GameOver();

        if (is_Counting) 
        {
            mseconds += Time.deltaTime;
            Generate_String_Time();
            Update_TimeShow();
        }
    }

    private void GameOver() // �����Ϸ������ֹͣ��ʱ
    {
        if (detect.Is_Failed || detect.Is_Success)
        {
            is_Counting = false;
        }
    }

    private void Generate_String_Time() // ��ʱ
    {
        time = "";
        if(mseconds < 10)
        {
            time += "0" + mseconds.ToString("0.000");
        }
        else
        {
            time += mseconds.ToString("0.000");
        }
        time = time.Replace(".", ":");
    }

    private void Update_TimeShow() // ��ʾ��ʱ������
    {
        timeShow.text = time;
    }
}
