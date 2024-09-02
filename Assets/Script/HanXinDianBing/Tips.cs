using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//韩信点兵中进入场景提示信息
public class Tips : MonoBehaviour
{
    //开局提示
    public GameObject dialogPanel;
    public string[] TipsText = new string[4];
    public int idx=0 ;//索引下标
    public Text dialogText;
    public Button ContinueButton;
        
  

    


    void Start()
    {

        TipsText[0] = "韩信：我的好友萧何给我出了一道有关中国剩余定理的题，我也来考考你：\n";
        TipsText[1] = "人群不知其数，三三数之剩二，五五数之剩三，七七数之剩二。问有几人？(三十以内)\n";
        TipsText[2] = "用现代的汉语说来就是:这群人不知道有几个\n但是三个三个人地数，最后剩两人；每次五个五个地数，剩三人；七个七个数，剩两人\n问一共多少人。（三十以内）\n";
        TipsText[3] = "游戏场景中，右上角“？”是提示内容";
 
        ShowDialog();

    }



    void ShowDialog()
    {
        //判断越界，更新文本
        if (idx <4)
        {
            dialogText.text = TipsText[idx];
            Debug.Log("显示OK");
        }
        else
        {
            
            Debug.LogWarning("越界啦，改值");
        }

        dialogPanel.SetActive(true);
    }


    public void ContinueWhenButtonIsClicked()
    {
        idx+=1;
        
       
        if (idx < 4)//文本输出完了退出提示
        {
            ShowDialog();
            // idx = 0;
        }
        else
        {
            dialogPanel.SetActive(false);
        }
    }


    //给叉按钮用的退出
    public void returnToChosengames(string ScenceName)
    {
            SceneManager.LoadScene(ScenceName);
    }

   



  

    void Update()
    {
        Debug.Log("idx is: " + idx);
    }
}
