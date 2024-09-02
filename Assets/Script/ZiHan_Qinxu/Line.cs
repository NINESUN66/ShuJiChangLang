using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float resolution;
    [SerializeField] private float minAngle;

    // 获取线条上最后一个点的位置
    public Vector2 LastPos => lineRenderer.GetPosition(lineRenderer.positionCount - 1);

    // 添加一个新的pos到线条上
    public void AddPosition(Vector2 pos)
    {
        lineRenderer.positionCount ++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
    }

    // 检查是否可以向线条添加新的点
    public bool CanAppend(Vector2 pos)
    {
        // 如果线条上没有点，则直接返回 true
        if (lineRenderer.positionCount == 0) return true;

        // 如果线条上只有一个点，则根据距离判断是否可以添加新的点
        if (lineRenderer.positionCount == 1)
        {
            return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > resolution;
        }

        // 如果线条上有多个点，则根据距离和角度判断是否可以添加新的点
        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > resolution && !IsAcuteAngle(pos);
    }

    // 检查新点与前两个点之间的角度是否为锐角
    public bool IsAcuteAngle(Vector2 pos)
    {
        if (lineRenderer.positionCount < 2) return false;

        Vector2 v1 = lineRenderer.GetPosition(lineRenderer.positionCount - 2) - lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        Vector2 v2 = pos - (Vector2)lineRenderer.GetPosition(lineRenderer.positionCount - 1);
        float angle = Vector2.Angle(v1, v2);
        return angle > 0 && angle < minAngle;
    }
}
