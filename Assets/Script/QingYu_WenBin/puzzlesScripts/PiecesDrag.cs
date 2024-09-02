using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PiecesDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image maskimage;
    Vector3 offset;
    Vector3 originpos;
    Vector3 outpos;
    RectTransform recttrans;
    public Transform PieceArea;

    public AudioClip startSound; // 拼图开始的音效
    public AudioClip finishSound; // 拼图结束的音效
    public AudioClip fSound; // 拼图失败的音效

    private AudioSource audioSource;

    void Start()
    {
        maskimage = GetComponent<Image>();
        maskimage.alphaHitTestMinimumThreshold = 0.1f;
        recttrans = transform.parent.parent.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        if (PieceArea == null)
        {
            Debug.LogError("PieceArea 变量未赋值！");
            return;
        }

        RectTransform pieceAreaRect = PieceArea.GetComponent<RectTransform>();
        if (pieceAreaRect == null)
        {
            Debug.LogError("PieceArea GameObject 没有 RectTransform 组件！");
        }
        else
        {
            PieceArea = pieceAreaRect;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 将鼠标位置转换为世界坐标，并获取RectTransform组件
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(recttrans, Input.mousePosition, eventData.enterEventCamera, out outpos))
        {
            // 记录初始锚点位置
            originpos = recttrans.anchoredPosition;
            // 将RectTransform设置为最后一个子对象，确保在拖动时始终在最上层
            recttrans.SetAsLastSibling();
            // 计算偏移量，即点击位置与RectTransform锚点位置的差值
            offset = (Vector3)recttrans.anchoredPosition - outpos;

            // 使用射线检测是否有碰撞
            RaycastHit2D[] hits2d = Physics2D.RaycastAll(eventData.position, Vector2.zero);

            // 如果检测到碰撞
            if (hits2d.Length > 0)
            {
                // 遍历所有碰撞对象
                foreach (var h in hits2d)
                {
                    // 如果碰撞对象的层级为8（即Grid层）
                    if (h.collider.transform.gameObject.layer == 8)
                    {
                        // 解析碰撞对象的名称获取网格ID
                        int gridID = int.Parse(h.collider.gameObject.name);
                        // 这里可以根据gridID进行一些逻辑处理，例如高亮显示可移动的格子等
                    }
                }
            }
        }
    }
    private void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // 检测碎片和格子的射线碰撞
        RaycastHit2D pieceHit = Physics2D.Raycast(eventData.position, Vector2.zero, Mathf.Infinity, 1 << 8); // 第8层碎片碰撞层
        RaycastHit2D gridHit = Physics2D.Raycast(eventData.position, Vector2.right, Mathf.Infinity, 1 << 9); // 第9层格子碰撞层
        if (pieceHit.collider != null && gridHit.collider != null)
        {
            int gridID = int.Parse(gridHit.collider.gameObject.name);
            int pieceID = int.Parse(pieceHit.collider.gameObject.name);
            // 获取碎片和格子的 RectTransform 组件
            RectTransform pieceRectTrans = pieceHit.collider.gameObject.GetComponent<RectTransform>();
            RectTransform gridRectTrans = gridHit.collider.gameObject.GetComponent<RectTransform>();
            // 检查碎片的碰撞体位置和格子的位置是否一致
            if (gridID == pieceID)
            {
                // 检查放置位置是否为空
                if (!GameManager.Instance.CheckHavePiece(gridID))
                {
                    // 将碎片放置在格子上
                    pieceRectTrans.anchoredPosition = gridRectTrans.anchoredPosition;
                    GameManager.Instance.SetPiece(gridID, pieceID);
                    // 播放拼图开始的音效
                    PlaySound(startSound);
                    pieceRectTrans.SetAsFirstSibling();

                    // 重置碎片的位置为 PieceArea 的位置
                    pieceRectTrans.anchoredPosition = PieceArea.GetComponent<RectTransform>().anchoredPosition;
                    // 禁用碎片的 RectTransform 组件的移动功能
                    Collider2D pieceCollider2D = pieceHit.collider;

                    // 禁用碎片的移动
                    pieceCollider2D.enabled = false; // 禁用Collider2D组件
                    // 检查拼图是否完成
                    if (GameManager.Instance.IsFinish())
                    {
                        Debug.Log("恭喜！拼图完成！");
                        // 在此处添加拼图完成后的处理逻辑

                        // 播放拼图结束的音效
                        PlaySound(finishSound);
                    }
                }
            }
            else
            {
                PlaySound(fSound);
                // 如果未找到碎片或格子的碰撞，则将碎片位置重置为初始位置
                recttrans.anchoredPosition = originpos;
                Debug.Log("放置失败：放置到的正确格子");
            }
        }
        else
        {
            PlaySound(fSound);
            // 如果未找到碎片或格子的碰撞，则将碎片位置重置为初始位置
            recttrans.anchoredPosition = originpos;
            Debug.Log("放置失败：未找到正确的放置位置");
        }
    }





    public void OnDrag(PointerEventData eventData)
    {         
        recttrans.anchoredPosition = offset + Input.mousePosition;
    }
}
