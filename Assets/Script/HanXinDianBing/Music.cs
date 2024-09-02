using UnityEngine;
using UnityEngine.UI; 

public class Music : MonoBehaviour
{
    public Slider move; 
    public AudioSource BackGroundMusic;


    public Button MusicButton;


    GameObject TipsPanel;
    public Button dispeat;

    private void Awake()
    {
        TipsPanel = GameObject.Find("PanelM");
    }
    void Start()
    { 

        TipsPanel.SetActive(false);
        // ��ʼ����������ֵΪ��ǰ����
        move.value = BackGroundMusic.volume;
    }

    public void SetVolume()
    {
        
        BackGroundMusic.volume = move.value;
    }

    public void show()
    {
        TipsPanel.SetActive(true);
    }
    public void unshow()
    {
        TipsPanel.SetActive(false);
    }

}

