using UnityEngine;

public class Set_position : MonoBehaviour
{
    public GameObject obj;//������
    
    Vector3 newPosition ;//��ʱλ�ñ���
   
    
    void Update()//��չʾͼ�����UI����
    {
        newPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 4, obj.transform.position.z);
        transform.position = newPosition;
    }
}
