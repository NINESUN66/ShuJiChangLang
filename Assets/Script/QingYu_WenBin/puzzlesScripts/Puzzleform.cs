using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Puzzleform : MonoBehaviour
{
    public static Puzzleform Instance;
    // �� Puzzleform �������һ�� public �� Texture2D �������ڴ洢ƴͼ����
    public Texture2D puzzleTexture;
    [SerializeField] private GameObject grid, BL, BR, TL, TR;
    [SerializeField] private Transform puzzlePanel; // ָ�� puzzlePanel ������Ϊ Transform

    private List<GameObject> puzzlePieces = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // ����ƴͼ��Ƭ
        //GeneratePuzzlePieces();
    }
    public void SetTextureSize(int width, int height)
    {
        // ���������ʹ�ô���Ŀ�Ⱥ͸߶���һЩ����
      //  Debug.Log("���ص�����ֱ���Ϊ��" + width + " x " + height);
    }

    // ����ƴͼ��Ƭ
    public void GeneratePuzzlePieces()
    {
        // ��ȡ���ص�ͼƬ�Ŀ�Ⱥ͸߶�
        int imageWidth = puzzleTexture.width;
        int imageHeight = puzzleTexture.height;

        // ����ƴͼ��Ƭ�Ŀ�Ⱥ͸߶ȣ�ʹ����Ӧ���ص�ͼƬ�ֱ���
        int pieceWidth = imageWidth / 2; // ������Ƭ�Ŀ��ΪͼƬ��ȵ�һ��
        int pieceHeight = imageHeight / 2; // ������Ƭ�ĸ߶�ΪͼƬ�߶ȵ�һ��

        // ���ɲ��洢�ĸ��ǵ�ƴͼ��Ƭ
        for (int i = 0; i < 4; i++)
        {
            // ���㵱ǰƴͼ��Ƭ�������е�λ��
            int startX = (i % 2) * pieceWidth;
            int startY = (i / 2) * pieceHeight;

            // ����ƴͼ��Ƭ
            GameObject piece = Instantiate(GetPiecePrefab(i), puzzlePanel);
           RawImage rawImage = piece.transform.Find("Mask/texture").GetComponent<RawImage>();

            // ����ƴͼ��Ƭ��UV���꣬ʵ�ֲü���ͬ��������
            rawImage.texture = puzzleTexture;
            // ����ƴͼ��Ƭ��λ�ú�����
            piece.transform.localPosition = GetPiecePosition(i);
            piece.transform.localScale = Vector3.one;
            piece.name = (i+1).ToString();

            puzzlePieces.Add(piece);
            // ��ӵ��б���

        }
    }


    // ��ȡ��Ӧƴͼ��Ƭ��Ԥ����
    private GameObject GetPiecePrefab(int index)
    {
        switch (index)
        {
            case 0:
                return TL; // ���Ͻ�
            case 1:
                return TR; // ���Ͻ�
            case 2:
                return BL; // ���½�
            case 3:
                return BR; // ���½�
            default:
                return null;
        }
    }


    // ��ȡ��Ӧƴͼ��Ƭ��λ��

    private Vector3 GetPiecePosition(int index)
    {
        float minX = -450f;
        float maxX = -400f;
        float minY = 70f;
        float maxY = 210f;

        float minX1 = 200f;
        float maxX1 = 260f;
        float minY1 = 70f;
        float maxY1 = 210f;

        // �������ƫ����
        float offsetX = Random.Range(minX, maxX);
        float offsetY = Random.Range(minY, maxY);
        float offsetX1 = Random.Range(minX1, maxX1);
        float offsetY1 = Random.Range(minY1, maxY1);

        switch (index)
        {
            case 0:
                return new Vector3(offsetX1, offsetY1, 0f); // ���Ͻ�λ��
            case 1:
                return new Vector3(offsetX, offsetY, 0f); // ���Ͻ�λ��
            case 2:
                return new Vector3(offsetX1, offsetY1, 0f); // ���½�λ��
            case 3:
                return new Vector3(offsetX, offsetY, 0f); // ���½�λ��
            default:
                return Vector3.zero;
        }
    }


}
