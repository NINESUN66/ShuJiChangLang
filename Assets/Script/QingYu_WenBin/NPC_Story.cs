using System;
using UnityEditor;
using UnityEngine;

public class NPC_Story : MonoBehaviour
{

    //�Ƿ���뽻������
    private bool isPlayerInsign = false;
    private string collider_name;
    private char name_now;
    private string name_now2;

    //����UI
    public GameObject btn_how;
    public GameObject btn_music;
    public GameObject back_to_choose;
    public GameObject back_to_game;
    public GameObject have_saved_people;
    public GameObject Joystick;

    /// <summary>
    /// �������
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
        if (isPlayerInsign && this.name ==collider_name&& Input.GetKeyDown(KeyCode.Space))//���������ڲ������롰Space������������н�̸
        {
            btn_how.SetActive(false);
            btn_music.SetActive(false);
            back_to_choose.SetActive(false);
            back_to_game.SetActive(true);
            controller.gameObject.SetActive(true);
            Joystick.SetActive(false);
        }
        int num = PlayerPrefs.GetInt(name_now2);
        
        //���Ѿ�̸���ˣ�����ѧ����ʧ
        if(num == 1)
        {
            have_saved_people.SetActive(true);


            gameObject.SetActive(false);
        }
        is_over = controller.isover();

        //���Ѿ�̸���ˣ�����story_num = 1
        if(is_over)
        {
            PlayerPrefs.SetInt(name_now2, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//���뽻�������ж�
    {
        
        if(collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = true;
            collider_name = this.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//�뿪���������ж�
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = false;
        }
    }

    public void click_jiaohu()
    {
        if (isPlayerInsign && this.name == collider_name)//���������ڲ��ҵ��������ť������н�̸
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
