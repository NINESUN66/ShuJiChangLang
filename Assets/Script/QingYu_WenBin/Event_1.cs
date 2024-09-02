using UnityEngine;
using UnityEngine.UI;

public class Event_1 : MonoBehaviour
{
    public AudioClip shengyin; // ��Ч�ļ�������  
    public Slider sound_size; // ���������Ļ�����
    private AudioSource yin_yuan; // ��Դ���  
    
    void Start()
    {
        if (!yin_yuan)
        {
            yin_yuan = GetComponent<AudioSource>();   
            if (!yin_yuan)
            {
                yin_yuan = gameObject.AddComponent<AudioSource>(); 
            }
            yin_yuan.clip = shengyin; // ������Դ�ļ�  
        }

        yin_yuan.loop = true;
        yin_yuan.Play(); // ������Ч  
        sound_size.onValueChanged.AddListener(delegate { VolumeChanged(sound_size.value); });//��������
    }

    

    void VolumeChanged(float value)// ������ƵԴ������  
    {
        
        yin_yuan.volume = value;
    }
}
