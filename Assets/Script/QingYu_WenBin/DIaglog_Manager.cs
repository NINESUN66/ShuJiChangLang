using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DIaglog_Manager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// csv��ʽ���Ի��ı�
    /// </summary>
    public TextAsset dialogDateFile;

    

    //����Ի�UI
    public GameObject talk_ui;
    public GameObject next;

    /// <summary>
    /// ��ɫͼ��
    /// </summary>
    public SpriteRenderer sprite_people;

    /// <summary>
    /// ��ɫ�����ı�
    /// </summary>
    public Text nameText;

    /// <summary>
    /// �Ի������ı�
    /// </summary>
    public Text dialogText;

    /// <summary>
    /// ��ɫͼƬ�б�
    /// </summary>
    public List<Sprite> sprites = new List<Sprite>();

    /// <summary>
    /// ��ǰ����ֵ
    /// </summary>
    public int dialogIndex=0;

    /// <summary>
    /// �Ի��ı������зָ�
    /// </summary>
    public string[] dialogRows;

    public bool is_over=false;


    Dictionary<int, Sprite> imageDic = new Dictionary<int, Sprite>();

    //�رհ�ť
    public GameObject back_to_choose;
    public GameObject back_to_game;
    public GameObject btn_how;
    public GameObject btn_music;
    public GameObject Joystick;




    private void OnEnable()//�����Ի�
    {
        talk_ui.SetActive(true);
        next.SetActive(true);
        sprite_people.gameObject.SetActive(true);
        readText(dialogDateFile);
        showDialog();
    }
    

    public void  UpdateText(string _name,string _text)//������һ�����
    {
        nameText.text = _name;
        dialogText.text = _text; 
    }
    public void UpdateImage(string _position)//����ͼ��
    {
        if(_position == "��")
        {

            sprite_people.sprite = sprites[1];
        }
        else 
        {

            sprite_people.sprite = sprites[0];
        }

    }

    public void readText(TextAsset _textAsset)//��ȡcsv�ļ�
    {
        dialogRows = _textAsset.text.Split('\n');
    }

    private void MyFunction()//��������ʧ
    {
        gameObject.SetActive(false);
    }
    public void showDialog()//չʾ�Ի�����
    {
        if(dialogRows.Length == dialogIndex+1)//�Ի����
        {

           
            talk_ui.SetActive(false);
            next.SetActive(false);
            sprite_people.gameObject.SetActive(false);
            onClickOver();

            dialogIndex = 0;//�������λ��

            is_over = true;//��ɸ���ѧ��Ӫ��

            gameObject.SetActive(false);

            return;
        }
       
        for (int i =0; i < dialogRows.Length; i++)//�����Ի�����
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

    public void OnClickNext()//�����������ť��
    {
        showDialog();
    }

    public void onClickOver()//����رհ�ť
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
