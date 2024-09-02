using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Detect : MonoBehaviour
{
    [SerializeField] private int numbernum;
    [SerializeField] private GameObject IMGParent;
    [SerializeField] private DifficultMode difficultmode;
    [SerializeField] private EndingControl endingcontrol;
    private Dictionary<int, bool> imagesOrder;
    public bool Is_Failed = false;
    public bool Is_Success = false;
    public bool is_Clicked = false;

    void Start()
    {
        numbernum = 10;
        imagesOrder = new Dictionary<int, bool>();
        for (int i = 0; i < numbernum; i++)
        {
            imagesOrder.Add(i, false);
        }
    }

    bool Check_Click() // 检测是否点击了任意一张图片
    {
        for (int i = 0; i < imagesOrder.Count; i++)
        {
            if (imagesOrder[i] == true)
            {
                return true;
            }
        }
        return false;
    }

    bool Check_Fail() // 检测游戏失败
    {
        for (int i = 0; i < imagesOrder.Count; i++)
        {
            if (imagesOrder[i] == false)
            {
                for (int j = i; j < imagesOrder.Count; j++)
                {
                    if (imagesOrder[j] == true)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool Check_Success() // 检测游戏成功
    {
        for (int i = 0; i < imagesOrder.Count; i++)
        {
            if(imagesOrder[i] == false)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) // 如果鼠标抬起则进行一次判断
        {
            // 标记点击过的数字
            for (int i = 0; i < 25; i++)
            {
                if(IMGParent.transform.GetChild(i).gameObject.activeSelf) // 在所有位置中找到有图片的物体
                {
                    RawImage child = IMGParent.transform.GetChild(i).GetComponent<RawImage>();
                    IMGAttribute imgAttribute = child.GetComponent<IMGAttribute>(); // 获取显示出的图片的所有属性

                    if (imgAttribute.name != null) 
                    {
                        int number = int.Parse(imgAttribute.ImgName); // 获取该图片显示的数字号
                        bool isClick = imgAttribute.Is_Clicked; // 并且标记这个图片已经被点击过

                        if (imagesOrder.ContainsKey(number)) // 如果这个数字在数组中则在数组中标记已经点击过了
                        {
                             imagesOrder[number] = isClick;
                        }
                        else
                        {
                            Debug.LogError("不存在啊" + imgAttribute.ImgName); // 抛出错误
                        }
                    }
                }
            }

            List<int> keys = new List<int>(imagesOrder.Keys);
            keys.Sort();

            Is_Failed = Check_Fail();

            Is_Success = Check_Success();

            // 如果调整到困难难度则在点击第一张图片之后将所有图片隐藏
            if (!is_Clicked)
            {
                is_Clicked = Check_Click();
                if (is_Clicked && difficultmode.is_Difficult)
                {
                    difficultmode.Shade_Pic(IMGParent.transform);
                }
            }

            if (Is_Failed)
            {
                endingcontrol.Ending(false);
                Debug.Log("游戏失败");
            }

            if (Is_Success)
            {
                endingcontrol.Ending(true);
                Debug.Log("游戏胜利");
            }
        }
    }
}
