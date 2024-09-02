using UnityEngine;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    //����UI
    public GameObject Btn_how;
    public GameObject Btn_music_size;
    public GameObject Btn_back_to_game;
    public GameObject Btn_back_to_choose;
    public GameObject Panel_how;
    public GameObject pannel_music;
    public GameObject pannel_over;
    public GameObject Joystick;
    //����
    public GameObject Event_1;
    public GameObject Event_2;

    //ƴͼ�󱣴�λ��
    public string weizhi_Key = "PlayerPosition"; // ���ڱ���λ�õ�PlayerPrefs��  
    private Vector3 lastPosition; // ��󱣴�λ��
    private int have_over;//�Ƿ����Ӫ��
    private int have_over_Pintu;//�Ƿ����ƴͼ
    
    void Start()
    {
      
        have_over = PlayerPrefs.GetInt("over_save");
        have_over_Pintu = PlayerPrefs.GetInt("over_Pintu");//��¼�Ƿ����ƴͼ�������ƴͼ�������赯��show_over;

        //����������ѧ�ҵĻ����Ͳ��������±���
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
        // ����Ϸ��ʼʱ����Ƿ��б����λ�ã����ָ���  
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
                transform.position = lastPosition; // ���ý�ɫ���ϴα����λ��  
            }
        }


    }

    
    void Update()
    {
        // ��������������߼���ʵʱ����lastPosition�����統��ɫ�ƶ�ʱ  
        lastPosition = transform.position;
#if !UNITY_ANDROID
        Joystick.SetActive(false);
#endif
    }
    public void onClick_How()//��������ĵ���ť
    {
        Btn_music_size.SetActive(false);
        Panel_how.SetActive(true);
        Btn_how.SetActive(false);
        Btn_back_to_choose.SetActive(false);
        Btn_back_to_game.SetActive(true);
        Joystick.SetActive(false);
    }
    public void onClick_music()//����������ڰ�ť
    {
        pannel_music.SetActive(true);
        Btn_music_size.SetActive(false);
        Panel_how.SetActive(false);
        Btn_how.SetActive(false);
        Btn_back_to_game.SetActive(true);
        Btn_back_to_choose.SetActive(false);
        Joystick.SetActive(false);
    }

    
    public void onClick_How_over()//�˳������ĵ�
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

    public void onClick_Win(string ScenceName)//������Ϸ�����ý���
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

    void OnDisable() // �����󱻽���ʱ����λ�ã������ڳ����л�֮ǰ  
    {
        SavePosition();
    }

    void SavePosition()//�����뿪����
    {
        string weizhi = lastPosition.x + "," + lastPosition.y + "," + lastPosition.z;
        PlayerPrefs.SetString(weizhi_Key, weizhi);
        PlayerPrefs.Save(); // ������ĵ�PlayerPrefs  
    }

    public void onClick_to_Pintu(string ScenceName)//����ƴͼ����
    {
            SceneManager.LoadScene(ScenceName);
    }
}
