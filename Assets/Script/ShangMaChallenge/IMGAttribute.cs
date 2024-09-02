using UnityEngine;

public class IMGAttribute : MonoBehaviour
{
    public string ImgName; // 图片代表几号数字？
    public bool Is_Clicked; // 该图片是否点击过？

    private void Start()
    {
        Is_Clicked = false;
    }
}
