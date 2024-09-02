using UnityEngine;
using UnityEngine.UI;

public class AdviceButton : MonoBehaviour
{
    //局内问好提示
    public GameObject dialogPanel2;
    public string[] suggestionText = new string[3];
    public Text dialogText1;
    public Button jixuButton;
    public Button adviceButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        suggestionText[0] = "题目：人群不知其数,\n三三数之剩二，五五数之剩三，七七数之剩二。\n问有几人？(三十以内)\n三个一堆地数会剩两人，\n五个一堆数剩三人，\n七个一堆数会剩两人，求一共多少人。\n";
        suggestionText[1] = "操作：将算出的人数拖入场景中间的椭圆区域，\n然后点击“提交”即可。\n";
        suggestionText[2] = "提示：可以试着在场景中排列一下哦\n顶部中间显示当前区域人数哦";
        dialogPanel2.SetActive(false);
    }

    //给问号的-建议提示   点击显示
    public void popMessage()
    {
        dialogPanel2.SetActive(true);
        ShowDialog1();
    }

    //显示文本
    public void ShowDialog1()
    {
        dialogText1.text = suggestionText[0] + suggestionText[1] + suggestionText[2];
    }


    //引号中点叉关闭提示
    public void dispear()
    {
        dialogPanel2.SetActive(false);
    }


}
