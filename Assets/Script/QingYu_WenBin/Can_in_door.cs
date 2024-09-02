using UnityEngine;
using UnityEngine.UI;

public class Can_in_door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //UIע��
    public GameObject Btn_how;
    public GameObject Btn_music_size;
    public GameObject Btn_back_to_game;
    public GameObject Btn_back_to_choose;
    public GameObject Joystick;

    //�����Ƿ���Խ��ŵ���ʾ
    public TextMesh Tips;
    public GameObject door;//������
    public GameObject Quetion_new;//ƴͼ��Ϸ��ʾ

    //�Ƿ���Խ���
    private bool isPlayerInsign;
    //�Ƿ����ƴͼ��Ϸ
    private int over_Pintu;
    

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Story_1") == 1 && PlayerPrefs.GetInt("Story_2") == 1 && PlayerPrefs.GetInt("Story_3") == 1 && PlayerPrefs.GetInt("Story_4") == 1)//�������������ѧ��
        {
            Tips.text = "��������񣡻�ӭ����";
            over_Pintu = PlayerPrefs.GetInt("over_Pintu");//���������

            if(isPlayerInsign &&Input.GetKeyDown(KeyCode.Space))//���������ʶ��Χ������Space����
            {
                if (over_Pintu > 0)//���Ѿ����ƴͼ��Ϸ
                {
                    door.SetActive(false);//����ײ����ʧ
                }
                else//����UI�ı�
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

    

    private void OnTriggerEnter2D(Collider2D collision)//������ʶ��Χ���б��ǲ������
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInsign = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)//�뿪ʶ��Χ
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
        if (PlayerPrefs.GetInt("Story_1") == 1 && PlayerPrefs.GetInt("Story_2") == 1 && PlayerPrefs.GetInt("Story_3") == 1 && PlayerPrefs.GetInt("Story_4") == 1)//�������������ѧ��
        {
            
            if (isPlayerInsign)//���������ʶ��Χ������Space����
            {
                if (over_Pintu > 0)//���Ѿ����ƴͼ��Ϸ
                {
                    door.SetActive(false);//����ײ����ʧ
                }
                else//����UI�ı�
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
