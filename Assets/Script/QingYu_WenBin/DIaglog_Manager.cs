using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DIaglog_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// csv格式，对话文本
    /// </summary>
    public TextAsset dialogDateFile;

    

    //人物对话UI
    public GameObject talk_ui;
    public GameObject next;

    /// <summary>
    /// 角色图像
    /// </summary>
    public SpriteRenderer sprite_people;

    /// <summary>
    /// 角色名字文本
    /// </summary>
    public Text nameText;

    /// <summary>
    /// 对话内容文本
    /// </summary>
    public Text dialogText;

    /// <summary>
    /// 角色图片列表
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// 当前索引值
    /// </summary>
    public int dialogIndex=0;

    /// <summary>
    /// 对话文本，按行分割
    /// </summary>
    public string[] dialogRows;

    public bool is_over=false;


    Dictionary<int, Sprite> imageDic = new Dictionary<int, Sprite>();

    //关闭按钮
    public GameObject back_to_choose;
    public GameObject back_to_game;
    public GameObject btn_how;
    public GameObject btn_music;
    public GameObject Joystick;




    private void OnEnable()//开启对话
    {
        talk_ui.SetActive(true);
        next.SetActive(true);
        sprite_people.gameObject.SetActive(true);
        readText(dialogDateFile);
        showDialog();
    }
    

    public void  UpdateText(string _name,string _text)//更新下一条语句
    {
        nameText.text = _name;
        dialogText.text = _text; 
    }
    public void UpdateImage(string _position)//更新图像
    {
        if(_position == "左")
        {

            sprite_people.sprite = sprites[1];
        }
        else 
        {

            sprite_people.sprite = sprites[0];
        }

    }

    public void readText(TextAsset _textAsset)//读取csv文件
    {
        dialogRows = _textAsset.text.Split('\n');
    }

    private void MyFunction()//该物体消失
    {
        gameObject.SetActive(false);
    }
    public void showDialog()//展示对话内容
    {
        if(dialogRows.Length == dialogIndex+1)//对话完毕
        {

           
            talk_ui.SetActive(false);
            next.SetActive(false);
            sprite_people.gameObject.SetActive(false);
            onClickOver();

            dialogIndex = 0;//重置语句位置

            is_over = true;//完成该数学家营救

            gameObject.SetActive(false);

            return;
        }
       
        for (int i =0; i < dialogRows.Length; i++)//遍历对话内容
        {
            string[] cell = dialogRows[i].Split(',');
            if (int.Parse(cell[0]) == dialogIndex )
            {
                UpdateText(cell[1], cell[3]);
                UpdateImage( cell[2]);
                dialogIndex = int.Parse(cell[4]);
                
                break;
            }
        }
    }

    public void OnClickNext()//点击“继续按钮”
    {
        showDialog();
    }

    public void onClickOver()//点击关闭按钮
    {
        talk_ui.SetActive(false);
        next.SetActive(false);
        sprite_people.gameObject.SetActive(false);
        back_to_choose.SetActive(true);
        back_to_game.SetActive(false);
        btn_music.SetActive(true);
        btn_how.SetActive(true);
        dialogIndex = 0;
        Joystick.SetActive(true);
        gameObject.gameObject.SetActive(false);
    }

    public bool isover()
    {
        return is_over;
    }
}
