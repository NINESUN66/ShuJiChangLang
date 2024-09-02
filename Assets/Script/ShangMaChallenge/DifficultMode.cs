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
        if (countDown.activeSelf) // ����ʱ����֮ǰ���Ե�����Ϸ�Ѷ�
        {
            is_Difficult = true;
            button.gameObject.GetComponent<Text>().text = "��";
            button.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    private void Update()
    {
        if (!countDown.activeSelf) // ���û�е����Ѷȣ��򵹼�ʱ����֮�����ٵ���
        {
            button.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void Shade_Pic(Transform Parent) // ��������ͼƬ
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
