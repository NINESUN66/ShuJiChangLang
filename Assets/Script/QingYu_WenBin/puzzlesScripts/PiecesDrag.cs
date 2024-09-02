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

    public AudioClip startSound; // ƴͼ��ʼ����Ч
    public AudioClip finishSound; // ƴͼ��������Ч
    public AudioClip fSound; // ƴͼʧ�ܵ���Ч

    private AudioSource audioSource;

    void Start()
    {
        maskimage = GetComponent<Image>();
        maskimage.alphaHitTestMinimumThreshold = 0.1f;
        recttrans = transform.parent.parent.GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        if (PieceArea == null)
        {
            Debug.LogError("PieceArea ����δ��ֵ��");
            return;
        }

        RectTransform pieceAreaRect = PieceArea.GetComponent<RectTransform>();
        if (pieceAreaRect == null)
        {
            Debug.LogError("PieceArea GameObject û�� RectTransform �����");
        }
        else
        {
            PieceArea = pieceAreaRect;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �����λ��ת��Ϊ�������꣬����ȡRectTransform���
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(recttrans, Input.mousePosition, eventData.enterEventCamera, out outpos))
        {
            // ��¼��ʼê��λ��
            originpos = recttrans.anchoredPosition;
            // ��RectTransform����Ϊ���һ���Ӷ���ȷ�����϶�ʱʼ�������ϲ�
            recttrans.SetAsLastSibling();
            // ����ƫ�����������λ����RectTransformê��λ�õĲ�ֵ
            offset = (Vector3)recttrans.anchoredPosition - outpos;

            // ʹ�����߼���Ƿ�����ײ
            RaycastHit2D[] hits2d = Physics2D.RaycastAll(eventData.position, Vector2.zero);

            // �����⵽��ײ
            if (hits2d.Length > 0)
            {
                // ����������ײ����
                foreach (var h in hits2d)
                {
                    // �����ײ����Ĳ㼶Ϊ8����Grid�㣩
                    if (h.collider.transform.gameObject.layer == 8)
                    {
                        // ������ײ��������ƻ�ȡ����ID
                        int gridID = int.Parse(h.collider.gameObject.name);
                        // ������Ը���gridID����һЩ�߼��������������ʾ���ƶ��ĸ��ӵ�
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
        // �����Ƭ�͸��ӵ�������ײ
        RaycastHit2D pieceHit = Physics2D.Raycast(eventData.position, Vector2.zero, Mathf.Infinity, 1 << 8); // ��8����Ƭ��ײ��
        RaycastHit2D gridHit = Physics2D.Raycast(eventData.position, Vector2.right, Mathf.Infinity, 1 << 9); // ��9�������ײ��
        if (pieceHit.collider != null && gridHit.collider != null)
        {
            int gridID = int.Parse(gridHit.collider.gameObject.name);
            int pieceID = int.Parse(pieceHit.collider.gameObject.name);
            // ��ȡ��Ƭ�͸��ӵ� RectTransform ���
            RectTransform pieceRectTrans = pieceHit.collider.gameObject.GetComponent<RectTransform>();
            RectTransform gridRectTrans = gridHit.collider.gameObject.GetComponent<RectTransform>();
            // �����Ƭ����ײ��λ�ú͸��ӵ�λ���Ƿ�һ��
            if (gridID == pieceID)
            {
                // ������λ���Ƿ�Ϊ��
                if (!GameManager.Instance.CheckHavePiece(gridID))
                {
                    // ����Ƭ�����ڸ�����
                    pieceRectTrans.anchoredPosition = gridRectTrans.anchoredPosition;
                    GameManager.Instance.SetPiece(gridID, pieceID);
                    // ����ƴͼ��ʼ����Ч
                    PlaySound(startSound);
                    pieceRectTrans.SetAsFirstSibling();

                    // ������Ƭ��λ��Ϊ PieceArea ��λ��
                    pieceRectTrans.anchoredPosition = PieceArea.GetComponent<RectTransform>().anchoredPosition;
                    // ������Ƭ�� RectTransform ������ƶ�����
                    Collider2D pieceCollider2D = pieceHit.collider;

                    // ������Ƭ���ƶ�
                    pieceCollider2D.enabled = false; // ����Collider2D���
                    // ���ƴͼ�Ƿ����
                    if (GameManager.Instance.IsFinish())
                    {
                        Debug.Log("��ϲ��ƴͼ��ɣ�");
                        // �ڴ˴����ƴͼ��ɺ�Ĵ����߼�

                        // ����ƴͼ��������Ч
                        PlaySound(finishSound);
                    }
                }
            }
            else
            {
                PlaySound(fSound);
                // ���δ�ҵ���Ƭ����ӵ���ײ������Ƭλ������Ϊ��ʼλ��
                recttrans.anchoredPosition = originpos;
                Debug.Log("����ʧ�ܣ����õ�����ȷ����");
            }
        }
        else
        {
            PlaySound(fSound);
            // ���δ�ҵ���Ƭ����ӵ���ײ������Ƭλ������Ϊ��ʼλ��
            recttrans.anchoredPosition = originpos;
            Debug.Log("����ʧ�ܣ�δ�ҵ���ȷ�ķ���λ��");
        }
    }





    public void OnDrag(PointerEventData eventData)
    {         
        recttrans.anchoredPosition = offset + Input.mousePosition;
    }
}
