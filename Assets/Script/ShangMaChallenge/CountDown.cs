using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] private Text textNum;
    [SerializeField] private GameObject countDownCanvas;

    private int number;
    private float start;

    private bool is_Counting = false;

    private void Start()
    {
        // 读取倒计时起始数字
        if (int.TryParse(textNum.text, out number))
        {
            start = number * 1.0f;
            is_Counting = true;
        }
        else
        {
            Debug.LogError("秒数输入错误");
        }
    }

    private void Update()
    {
        if (is_Counting)
        {
            start -= Time.deltaTime;
            textNum.text = Mathf.CeilToInt(start).ToString(); // 实时更新倒计时文字且不显示小数位
            if (start <= 0)
            {
                is_Counting = false;
                End_Count_Down();
            }
        }
    }

    void End_Count_Down()
    {
        countDownCanvas.SetActive(false);
    }
}
