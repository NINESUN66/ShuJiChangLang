using UnityEngine;
using UnityEngine.UI;

public class Event_1 : MonoBehaviour
{
    public AudioClip shengyin; // 音效文件的引用  
    public Slider sound_size; // 调节音量的滑行条
    private AudioSource yin_yuan; // 音源组件  
    
    void Start()
    {
        if (!yin_yuan)
        {
            yin_yuan = GetComponent<AudioSource>();   
            if (!yin_yuan)
            {
                yin_yuan = gameObject.AddComponent<AudioSource>(); 
            }
            yin_yuan.clip = shengyin; // 设置音源文件  
        }

        yin_yuan.loop = true;
        yin_yuan.Play(); // 播放音效  
        sound_size.onValueChanged.AddListener(delegate { VolumeChanged(sound_size.value); });//播放音乐
    }

    

    void VolumeChanged(float value)// 调整音频源的音量  
    {
        
        yin_yuan.volume = value;
    }
}
