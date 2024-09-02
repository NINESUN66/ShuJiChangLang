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
        BookText[0] = "作者：张丘     时间：公元前2世纪至公元2世纪之间\r\n时期：西汉     内容：这部著作以“算术”为题材，涉及了数学中的算法、方程、几何等多个领域，内容十分广泛。";
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
