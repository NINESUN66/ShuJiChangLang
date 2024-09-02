using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DifficultMode : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject countDown;

    public bool is_Difficult = false;

    public void Change_Difficult()
    {
        if (countDown.activeSelf) // 倒计时结束之前可以调整游戏难度
        {
            is_Difficult = true;
            button.gameObject.GetComponent<Text>().text = "难";
            button.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    private void Update()
    {
        if (!countDown.activeSelf) // 如果没有调整难度，则倒计时结束之后不能再调整
        {
            button.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void Shade_Pic(Transform Parent) // 隐藏所有图片
    {
        foreach(Transform pic in Parent.transform)
        {
            if(pic.gameObject.activeSelf)
            {
                pic.GetComponent<RawImage>().texture = null;
            }
        }
    }
}
