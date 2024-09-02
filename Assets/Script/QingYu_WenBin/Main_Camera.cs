using UnityEngine;

public class Main_Camera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform Player;//玩家物体

    public void LateUpdate()//将摄像头控制始终以玩家为中心
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }
}
