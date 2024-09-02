using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Font_1 : MonoBehaviour
{
    public Text Wen_zi; // UI Text组件的引用  
    public AudioClip sheng_yin; // 音效文件的引用  
    public float typeDelay = 0.1f; // 音效演奏频率
    private string text = "恭喜你来到了最后一关，见识了诸多的古代数学文化，是不是很想亲眼见一见一些古代数学大家呢？请穿上我们准备好的衣服，进行接下来的时空穿越吧！"; // 完整的文本内容  
    private int wenzi_pos = 0; // 当前字符的索引  
    private AudioSource yin_yuan; // AudioSource组件的引用  
    public GameObject vironment_1;//场景一
    public GameObject vironment_2;//场景二
    private bool can_next;//是否可以点击“继续”按钮
    public Text next;//“继续”按钮

    void Start()
    {
        vironment_1.SetActive(false);//场景一消失
       

        
           yin_yuan = gameObject.AddComponent<AudioSource>(); // 如果GameObject上没有AudioSource组件，则添加一个  
            
           yin_yuan.clip = sheng_yin; // 设置音效文件  
        

        StartCoroutine(Type_Sound()); // 逐字打出文本并播放音效  
    }

    IEnumerator Type_Sound()//开始打字
    {
        yin_yuan.Play(); // 播放音效  
        while (wenzi_pos < text.Length)
        {

            Wen_zi.text += text[wenzi_pos]; // 添加当前字符到Text组件的文本中  
            wenzi_pos++; // 字符索引增加
            yield return new WaitForSeconds(typeDelay); // 等待指定的延迟时间  
        }
        can_next = true;
        yin_yuan.Pause();//音效关闭
    }

    public void click_next()//点击“继续”按钮
    {
        if (yin_yuan.isPlaying)
        {
            next.text = "时空穿越（请在故事介绍结束后进行点击）";
        }

        if (can_next)
        {
            
            vironment_1.SetActive(true);
            vironment_2.SetActive(false);
        } 
    }

    public void returnToChosengames(string ScenceName)//返回选关界面
    {
        SceneManager.LoadScene(ScenceName);
    }
}