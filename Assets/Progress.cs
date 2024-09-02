using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Text progress;
    private int num;
    private int have_over=0;
    private bool have_show = false;
    private bool have_hide = false;


    //UI系统
    public GameObject show_pannel;
    public GameObject how_win_pannel;
    public GameObject Btn_back_to_game;
    public GameObject Btn_back_to_choose;
    public GameObject Btn_how_win;
    public GameObject Btn_music_controller;
    public GameObject new_question;
    public GameObject Joystick;
    void Start()
    {
        progress = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        have_over = PlayerPrefs.GetInt("over_Pintu");//记录是否完成拼图，若完成拼图，则无需弹出show_over;
        num = PlayerPrefs.GetInt("Story_1") + PlayerPrefs.GetInt("Story_2") + PlayerPrefs.GetInt("Story_3") + PlayerPrefs.GetInt("Story_4");
        progress.text = "任务进度为："+num.ToString()+"/4";
        if(num==4)//如果解救四人
        {
            if (have_over == 0&&!have_show)//未完成拼图，则提示如何做
            {
                how_win_pannel.SetActive(false);
                Btn_how_win.SetActive(false);
                Btn_music_controller.SetActive(false);
                
                Joystick.SetActive(false);
                PlayerPrefs.SetInt("have_over", 1);
                show_over();
                have_show = true;//防止一直重复显示，无法关闭。
            }
            else if (have_over !=0&&!have_hide)
            {
                Btn_back_to_game.SetActive(false);
                Btn_back_to_choose.SetActive(true);
                Joystick.SetActive(true);
                have_hide = true;
            }
            PlayerPrefs.SetInt("over_save", 1);
        }

    }

    void show_over()
    {
        show_pannel.gameObject.SetActive(true);
        Btn_back_to_choose.SetActive(false);
        Btn_back_to_game.SetActive(true);
    }

    public void click_back_game()
    {
        show_pannel.gameObject.SetActive(false);
        how_win_pannel.SetActive(false);
        new_question.SetActive(false);
        Btn_how_win.SetActive(true);
        Btn_back_to_choose.SetActive(true);
        Btn_back_to_game.SetActive(false);
        Joystick.SetActive(true);
    }
}
