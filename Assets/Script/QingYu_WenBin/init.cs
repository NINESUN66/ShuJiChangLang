using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    //各种UI
    public GameObject Btn_how;
    public GameObject Btn_music_size;
    public GameObject Btn_back_to_game;
    public GameObject Btn_back_to_choose;
    public GameObject Panel_how;
    public GameObject pannel_music;
    public GameObject pannel_over;
    public GameObject Joystick;
    //场景
    public GameObject Event_1;
    public GameObject Event_2;

    //拼图后保存位置
    public string weizhi_Key = "PlayerPosition"; // 用于保存位置的PlayerPrefs键  
    private Vector3 lastPosition; // 最后保存位置
    private int have_over;//是否结束营救
    private int have_over_Pintu;//是否结束拼图
    
    void Start()
    {
      
        have_over = PlayerPrefs.GetInt("over_save");
        have_over_Pintu = PlayerPrefs.GetInt("over_Pintu");//记录是否完成拼图，若完成拼图，则无需弹出show_over;

        //如果解救完数学家的话，就不弹出故事背景
        if (have_over == 1)
        {
            Event_1.SetActive(true);
            Event_2.SetActive(false);
            if (have_over_Pintu == 1)
                Panel_how.SetActive(false);
            else
            {
                Btn_how.SetActive(false);
                Btn_music_size.SetActive(false);
            }
        }
        else
        {
            Event_1.SetActive(false);
            Event_2.SetActive(true);
            Btn_how.SetActive(false);
            Btn_music_size.SetActive(false);
        }
        // 在游戏开始时检查是否有保存的位置，并恢复它  
        if (PlayerPrefs.HasKey(weizhi_Key))
        {
            string positionString = PlayerPrefs.GetString(weizhi_Key);
            string[] positionArray = positionString.Split(',');
            if (positionArray.Length == 3)
            {
                float x = float.Parse(positionArray[0]);
                float y = float.Parse(positionArray[1]);
                float z = float.Parse(positionArray[2]);
                lastPosition = new Vector3(x, y, z);
                transform.position = lastPosition; // 设置角色到上次保存的位置  
            }
        }


    }

    
    void Update()
    {
        // 可以在这里添加逻辑来实时更新lastPosition，例如当角色移动时  
        lastPosition = transform.position;
#if !UNITY_ANDROID
        Joystick.SetActive(false);
#endif
    }
    public void onClick_How()//点击疑问文档按钮
    {
        Btn_music_size.SetActive(false);
        Panel_how.SetActive(true);
        Btn_how.SetActive(false);
        Btn_back_to_choose.SetActive(false);
        Btn_back_to_game.SetActive(true);
        Joystick.SetActive(false);
    }
    public void onClick_music()//点击音量调节按钮
    {
        pannel_music.SetActive(true);
        Btn_music_size.SetActive(false);
        Panel_how.SetActive(false);
        Btn_how.SetActive(false);
        Btn_back_to_game.SetActive(true);
        Btn_back_to_choose.SetActive(false);
        Joystick.SetActive(false);
    }

    
    public void onClick_How_over()//退出疑问文档
    {
        pannel_music.SetActive(false);
        Btn_music_size.SetActive(true);
        Panel_how.SetActive(false);
        Btn_how.SetActive(true);
        Btn_back_to_choose.SetActive(true);
        Btn_back_to_game.SetActive(false);
        pannel_over.SetActive(false);
        Joystick.SetActive(true);
    }

    public void onClick_Win(string ScenceName)//结束游戏后重置进度
    {
        PlayerPrefs.SetInt("over_Pintu", 0);
        PlayerPrefs.SetInt("have_over", 0);
        PlayerPrefs.SetInt("over_save",0);
        PlayerPrefs.SetInt("Story_1", 0);
        PlayerPrefs.SetInt("Story_2", 0);
        PlayerPrefs.SetInt("Story_3", 0);
        PlayerPrefs.SetInt("Story_4", 0);
        SceneManager.LoadScene(ScenceName);
    }

    void OnDisable() // 当对象被禁用时保存位置，例如在场景切换之前  
    {
        SavePosition();
    }

    void SavePosition()//保存离开场景
    {
        string weizhi = lastPosition.x + "," + lastPosition.y + "," + lastPosition.z;
        PlayerPrefs.SetString(weizhi_Key, weizhi);
        PlayerPrefs.Save(); // 保存更改到PlayerPrefs  
    }

    public void onClick_to_Pintu(string ScenceName)//进入拼图场景
    {
            SceneManager.LoadScene(ScenceName);
    }
}
