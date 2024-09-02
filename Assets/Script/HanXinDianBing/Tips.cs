using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//���ŵ���н��볡����ʾ��Ϣ
public class Tips : MonoBehaviour
{
    //������ʾ
    public GameObject dialogPanel;
    public string[] TipsText = new string[4];
    public int idx=0 ;//�����±�
    public Text dialogText;
    public Button ContinueButton;
        
  

    


    void Start()
    {

        TipsText[0] = "���ţ��ҵĺ������θ��ҳ���һ���й��й�ʣ�ඨ����⣬��Ҳ�������㣺\n";
        TipsText[1] = "��Ⱥ��֪������������֮ʣ����������֮ʣ����������֮ʣ�������м��ˣ�(��ʮ����)\n";
        TipsText[2] = "���ִ��ĺ���˵������:��Ⱥ�˲�֪���м���\n�������������˵��������ʣ���ˣ�ÿ��������������ʣ���ˣ��߸��߸�����ʣ����\n��һ�������ˡ�����ʮ���ڣ�\n";
        TipsText[3] = "��Ϸ�����У����Ͻǡ���������ʾ����";
 
        ShowDialog();

    }



    void ShowDialog()
    {
        //�ж�Խ�磬�����ı�
        if (idx <4)
        {
            dialogText.text = TipsText[idx];
            Debug.Log("��ʾOK");
        }
        else
        {
            
            Debug.LogWarning("Խ��������ֵ");
        }

        dialogPanel.SetActive(true);
    }


    public void ContinueWhenButtonIsClicked()
    {
        idx+=1;
        
       
        if (idx < 4)//�ı���������˳���ʾ
        {
            ShowDialog();
            // idx = 0;
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }


    //���水ť�õ��˳�
    public void returnToChosengames(string ScenceName)
    {
            SceneManager.LoadScene(ScenceName);
    }

   



  

    void Update()
    {
        Debug.Log("idx is: " + idx);
    }
}
