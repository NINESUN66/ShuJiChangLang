using UnityEngine;
using UnityEngine.UI;

public class CheckRight : MonoBehaviour
{
    [SerializeField] private MnistTest mnistTest;
    [SerializeField] private ShowNumber showNumber;
    [SerializeField] private Text score;
    [SerializeField] private DrawManager drawManager;
    [SerializeField] private EndingControl endingControl;
    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip wrongClip;

    private AudioSource audioSource;

    public int WinTimes;
    public int TotalTimes;
    public int WrongTimes;
    public int currentCount;

    private void Start()
    {
        WinTimes = 0;
        WrongTimes = 0;
        TotalTimes = showNumber.TestTimes;

        // 在当前游戏对象上添加一个AudioSource组件
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void ShowScore() // 显示新分数
    {
        string Score = WinTimes.ToString() + "/" + TotalTimes.ToString();
        score.text = Score;
    }

    public void Check()
    {
        // 检测游戏结束
        if (showNumber.currentIndex >= TotalTimes - 1)
        {
            if (WinTimes >= 6)
            {
                Debug.Log("游戏胜利" + showNumber.currentIndex + " " + TotalTimes);
                endingControl.Ending(true);
            }
            else
            {
                Debug.Log("游戏失败" + showNumber.currentIndex + " " + TotalTimes);
                endingControl.Ending(false);
            }
        }

        //如果没写对
        if (mnistTest.result != showNumber.randomNumbers[showNumber.currentIndex].ToString())
        {
            Debug.Log("写错了");
            if (showNumber.currentIndex < TotalTimes - 1)
            {
                showNumber.currentIndex++;
            }
            // 播放错误音效
            audioSource.PlayOneShot(wrongClip);

            drawManager.ClearAllLines();
        }

        // 如果写对了
        else if (mnistTest.result == showNumber.randomNumbers[showNumber.currentIndex].ToString())
        {
            Debug.Log("写对了");
            if (showNumber.currentIndex < TotalTimes - 1)
            {
                showNumber.currentIndex++;
            }
            // 播放正确音效
            audioSource.PlayOneShot(correctClip);

            WinTimes++;
            ShowScore();
            drawManager.ClearAllLines();
        }
    }
}
