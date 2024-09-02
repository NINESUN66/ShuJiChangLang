using UnityEngine;
using UnityEngine.UI;

public class Can_in_door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //UI注释
    public GameObject Btn_how;
    public GameObject Btn_music_size;
    public GameObject Btn_back_to_game;
    public GameObject Btn_back_to_choose;
    public GameObject Joystick;

    //门上是否可以进门的提示
    public TextMesh Tips;
    public GameObject door;//门物体
    public GameObject Quetion_new;//拼图游戏提示

    //是否可以进入
    private bool isPlayerInsign;
    //是否完成拼图游戏
    private int over_Pintu;
    

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Story_1") == 1 && PlayerPrefs.GetInt("Story_2") == 1 && PlayerPrefs.GetInt("Story_3") == 1 && PlayerPrefs.GetInt("Story_4") == 1)//若解救完所有数学家
        {
            Tips.text = "已完成任务！欢迎进入";
            over_Pintu = PlayerPrefs.GetInt("over_Pintu");//已完成任务

            if(isPlayerInsign &&Input.GetKeyDown(KeyCode.Space))//如果进入门识别范围并按“Space”键
            {
                if (over_Pintu > 0)//若已经完成拼图游戏
                {
                    door.SetActive(false);//门碰撞体消失
                }
                else//各种UI改变
                {
                    Quetion_new.SetActive(true);
                    Btn_music_size.SetActive(false);
                    Btn_how.SetActive(false);
                    Btn_back_to_choose.SetActive(false);
                    Btn_back_to_game.SetActive(true);
                    Joystick.SetActive(false);
                }
            }
            
        }

    }

    

    private void OnTriggerEnter2D(Collider2D collision)//若进入识别范围，判别是不是玩家
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//离开识别范围
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)//
    {
        Tips.gameObject.SetActive(true);
    }

    public void click_jiaohu()
    {
        if (PlayerPrefs.GetInt("Story_1") == 1 && PlayerPrefs.GetInt("Story_2") == 1 && PlayerPrefs.GetInt("Story_3") == 1 && PlayerPrefs.GetInt("Story_4") == 1)//若解救完所有数学家
        {
            
            if (isPlayerInsign)//如果进入门识别范围并按“Space”键
            {
                if (over_Pintu > 0)//若已经完成拼图游戏
                {
                    door.SetActive(false);//门碰撞体消失
                }
                else//各种UI改变
                {
                    Quetion_new.SetActive(true);
                    Btn_music_size.SetActive(false);
                    Btn_how.SetActive(false);
                    Btn_back_to_choose.SetActive(false);
                    Btn_back_to_game.SetActive(true);
                    Joystick.SetActive(false);
                }
            }

        }
    }
}
