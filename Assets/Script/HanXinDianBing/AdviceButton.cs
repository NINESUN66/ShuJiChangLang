using UnityEngine;
using UnityEngine.UI;

public class AdviceButton : MonoBehaviour
{
    //�����ʺ���ʾ
    public GameObject dialogPanel2;
    public string[] suggestionText = new string[3];
    public Text dialogText1;
    public Button jixuButton;
    public Button adviceButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        suggestionText[0] = "��Ŀ����Ⱥ��֪����,\n������֮ʣ����������֮ʣ����������֮ʣ����\n���м��ˣ�(��ʮ����)\n����һ�ѵ�����ʣ���ˣ�\n���һ����ʣ���ˣ�\n�߸�һ������ʣ���ˣ���һ�������ˡ�\n";
        suggestionText[1] = "��������������������볡���м����Բ����\nȻ�������ύ�����ɡ�\n";
        suggestionText[2] = "��ʾ�����������ڳ���������һ��Ŷ\n�����м���ʾ��ǰ��������Ŷ";
        dialogPanel2.SetActive(false);
    }

    //���ʺŵ�-������ʾ   �����ʾ
    public void popMessage()
    {
        dialogPanel2.SetActive(true);
        ShowDialog1();
    }

    //��ʾ�ı�
    public void ShowDialog1()
    {
        dialogText1.text = suggestionText[0] + suggestionText[1] + suggestionText[2];
    }


    //�����е��ر���ʾ
    public void dispear()
    {
        dialogPanel2.SetActive(false);
    }


}
