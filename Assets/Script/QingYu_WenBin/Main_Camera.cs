using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform Player;//�������

    public void LateUpdate()//������ͷ����ʼ�������Ϊ����
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }
}
