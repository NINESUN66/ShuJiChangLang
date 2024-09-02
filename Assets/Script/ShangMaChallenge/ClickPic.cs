using UnityEngine;
using UnityEngine.UI;

public class ClickPic : MonoBehaviour
{
    private RawImage Img;

    public void IsClicked()
    {
        Img = GetComponent<RawImage>();
        Img.GetComponent<IMGAttribute>().Is_Clicked = true;
        // 点击图片就让他旋转90°
        Quaternion newRotation = Quaternion.Euler(0f, 90f, 0f);
        Img.transform.rotation = newRotation;
    }
}
