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

        // �ڵ�ǰ��Ϸ���������һ��AudioSource���
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void ShowScore() // ��ʾ�·���
    {
        string Score = WinTimes.ToString() + "/" + TotalTimes.ToString();
        score.text = Score;
    }

    public void Check()
    {
        // �����Ϸ����
        if (showNumber.currentIndex >= TotalTimes - 1)
        {
            if (WinTimes >= 6)
            {
                Debug.Log("��Ϸʤ��" + showNumber.currentIndex + " " + TotalTimes);
                endingControl.Ending(true);
            }
            else
            {
                Debug.Log("��Ϸʧ��" + showNumber.currentIndex + " " + TotalTimes);
                endingControl.Ending(false);
            }
        }

        //���ûд��
        if (mnistTest.result != showNumber.randomNumbers[showNumber.currentIndex].ToString())
        {
            Debug.Log("д����");
            if (showNumber.currentIndex < TotalTimes - 1)
            {
                showNumber.currentIndex++;
            }
            // ���Ŵ�����Ч
            audioSource.PlayOneShot(wrongClip);

            drawManager.ClearAllLines();
        }

        // ���д����
        else if (mnistTest.result == showNumber.randomNumbers[showNumber.currentIndex].ToString())
        {
            Debug.Log("д����");
            if (showNumber.currentIndex < TotalTimes - 1)
            {
                showNumber.currentIndex++;
            }
            // ������ȷ��Ч
            audioSource.PlayOneShot(correctClip);

            WinTimes++;
            ShowScore();
            drawManager.ClearAllLines();
        }
    }
}
