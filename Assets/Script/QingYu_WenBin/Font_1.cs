using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Font_1 : MonoBehaviour
{
    public Text Wen_zi; // UI Text���������  
    public AudioClip sheng_yin; // ��Ч�ļ�������  
    public float typeDelay = 0.1f; // ��Ч����Ƶ��
    private string text = "��ϲ�����������һ�أ���ʶ�����ĹŴ���ѧ�Ļ����ǲ��Ǻ������ۼ�һ��һЩ�Ŵ���ѧ����أ��봩������׼���õ��·������н�������ʱ�մ�Խ�ɣ�"; // �������ı�����  
    private int wenzi_pos = 0; // ��ǰ�ַ�������  
    private AudioSource yin_yuan; // AudioSource���������  
    public GameObject vironment_1;//����һ
    public GameObject vironment_2;//������
    private bool can_next;//�Ƿ���Ե������������ť
    public Text next;//����������ť

    void Start()
    {
        vironment_1.SetActive(false);//����һ��ʧ
       

        
           yin_yuan = gameObject.AddComponent<AudioSource>(); // ���GameObject��û��AudioSource����������һ��  
            
           yin_yuan.clip = sheng_yin; // ������Ч�ļ�  
        

        StartCoroutine(Type_Sound()); // ���ִ���ı���������Ч  
    }

    IEnumerator Type_Sound()//��ʼ����
    {
        yin_yuan.Play(); // ������Ч  
        while (wenzi_pos < text.Length)
        {

            Wen_zi.text += text[wenzi_pos]; // ��ӵ�ǰ�ַ���Text������ı���  
            wenzi_pos++; // �ַ���������
            yield return new WaitForSeconds(typeDelay); // �ȴ�ָ�����ӳ�ʱ��  
        }
        can_next = true;
        yin_yuan.Pause();//��Ч�ر�
    }

    public void click_next()//�������������ť
    {
        if (yin_yuan.isPlaying)
        {
            next.text = "ʱ�մ�Խ�����ڹ��½��ܽ�������е����";
        }

        if (can_next)
        {
            
            vironment_1.SetActive(true);
            vironment_2.SetActive(false);
        } 
    }

    public void returnToChosengames(string ScenceName)//����ѡ�ؽ���
    {
        SceneManager.LoadScene(ScenceName);
    }
}