using UnityEngine;

public class Set_position : MonoBehaviour
{
    public GameObject obj;//参照物
    
    Vector3 newPosition ;//临时位置变量
   
    
    void Update()//将展示图像放在UI中央
    {
        newPosition = new Vector3(obj.transform.position.x, obj.transform.position.y + 4, obj.transform.position.z);
        transform.position = newPosition;
    }
}
