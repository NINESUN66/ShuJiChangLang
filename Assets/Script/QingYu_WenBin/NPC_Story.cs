using System;
using UnityEditor;
using UnityEngine;

public class NPC_Story : MonoBehaviour
{

    //是否进入交互区域
    private bool isPlayerInsign = false;
    private string collider_name;
    private char name_now;
    private string name_now2;

    //各种UI
    public GameObject btn_how;
    public GameObject btn_music;
    public GameObject back_to_choose;
    public GameObject back_to_game;
    public GameObject have_saved_people;
    public GameObject Joystick;

    /// <summary>
    /// 聊天控制
    /// </summary>
    public DIaglog_Manager controller;
    private bool is_over;
    void Start()
    {
        name_now = gameObject.name[gameObject.name.Length - 1];
        name_now2 = "Story_" + name_now;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(collider_name);
        if (isPlayerInsign && this.name ==collider_name&& Input.GetKeyDown(KeyCode.Space))//交互区域内并且输入“Space”按键，则进行交谈
        {
            btn_how.SetActive(false);
            btn_music.SetActive(false);
            back_to_choose.SetActive(false);
            back_to_game.SetActive(true);
            controller.gameObject.SetActive(true);
            Joystick.SetActive(false);
        }
        int num = PlayerPrefs.GetInt(name_now2);
        
        //若已经谈完了，该数学家消失
        if(num == 1)
        {
            have_saved_people.SetActive(true);


            gameObject.SetActive(false);
        }
        is_over = controller.isover();

        //若已经谈完了，设置story_num = 1
        if(is_over)
        {
            PlayerPrefs.SetInt(name_now2, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//进入交互区域判定
    {
        
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = true;
            collider_name = this.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//离开交互区域判定
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = false;
        }
    }

    public void click_jiaohu()
    {
        if (isPlayerInsign && this.name == collider_name)//交互区域内并且点击交互按钮，则进行交谈
        {
            btn_how.SetActive(false);
            btn_music.SetActive(false);
            back_to_choose.SetActive(false);
            back_to_game.SetActive(true);
            controller.gameObject.SetActive(true);
            Joystick.SetActive(false);
        }
    }
    
}
