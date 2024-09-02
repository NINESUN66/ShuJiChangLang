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

    bool Check_Click() // ����Ƿ���������һ��ͼƬ
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

    bool Check_Fail() // �����Ϸʧ��
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

    bool Check_Success() // �����Ϸ�ɹ�
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
        if (Input.GetMouseButtonUp(0)) // ������̧�������һ���ж�
        {
            // ��ǵ����������
            for (int i = 0; i < 25; i++)
            {
                if(IMGParent.transform.GetChild(i).gameObject.activeSelf) // ������λ�����ҵ���ͼƬ������
                {
                    RawImage child = IMGParent.transform.GetChild(i).GetComponent<RawImage>();
                    IMGAttribute imgAttribute = child.GetComponent<IMGAttribute>(); // ��ȡ��ʾ����ͼƬ����������

                    if (imgAttribute.name != null) 
                    {
                        int number = int.Parse(imgAttribute.ImgName); // ��ȡ��ͼƬ��ʾ�����ֺ�
                        bool isClick = imgAttribute.Is_Clicked; // ���ұ�����ͼƬ�Ѿ��������

                        if (imagesOrder.ContainsKey(number)) // ���������������������������б���Ѿ��������
                        {
                             imagesOrder[number] = isClick;
                        }
                        else
                        {
                            Debug.LogError("�����ڰ�" + imgAttribute.ImgName); // �׳�����
                        }
                    }
                }
            }

            List<int> keys = new List<int>(imagesOrder.Keys);
            keys.Sort();

            Is_Failed = Check_Fail();

            Is_Success = Check_Success();

            // ��������������Ѷ����ڵ����һ��ͼƬ֮������ͼƬ����
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
                Debug.Log("��Ϸʧ��");
            }

            if (Is_Success)
            {
                endingcontrol.Ending(true);
                Debug.Log("��Ϸʤ��");
            }
        }
    }
}
