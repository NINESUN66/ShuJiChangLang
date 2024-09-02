using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Puzzleform : MonoBehaviour
{
    public static Puzzleform Instance;
    // 在 Puzzleform 类中添加一个 public 的 Texture2D 变量用于存储拼图纹理
    public Texture2D puzzleTexture;
    [SerializeField] private GameObject grid, BL, BR, TL, TR;
    [SerializeField] private Transform puzzlePanel; // 指定 puzzlePanel 的类型为 Transform

    private List<GameObject> puzzlePieces = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 生成拼图碎片
        //GeneratePuzzlePieces();
    }
    public void SetTextureSize(int width, int height)
    {
        // 在这里可以使用传入的宽度和高度做一些处理
      //  Debug.Log("加载的纹理分辨率为：" + width + " x " + height);
    }

    // 生成拼图碎片
    public void GeneratePuzzlePieces()
    {
        // 获取加载的图片的宽度和高度
        int imageWidth = puzzleTexture.width;
        int imageHeight = puzzleTexture.height;

        // 计算拼图碎片的宽度和高度，使其适应加载的图片分辨率
        int pieceWidth = imageWidth / 2; // 单个碎片的宽度为图片宽度的一半
        int pieceHeight = imageHeight / 2; // 单个碎片的高度为图片高度的一半

        // 生成并存储四个角的拼图碎片
        for (int i = 0; i < 4; i++)
        {
            // 计算当前拼图碎片在纹理中的位置
            int startX = (i % 2) * pieceWidth;
            int startY = (i / 2) * pieceHeight;

            // 创建拼图碎片
            GameObject piece = Instantiate(GetPiecePrefab(i), puzzlePanel);
           RawImage rawImage = piece.transform.Find("Mask/texture").GetComponent<RawImage>();

            // 设置拼图碎片的UV坐标，实现裁剪不同的纹理部分
            rawImage.texture = puzzleTexture;
            // 设置拼图碎片的位置和缩放
            piece.transform.localPosition = GetPiecePosition(i);
            piece.transform.localScale = Vector3.one;
            piece.name = (i+1).ToString();

            puzzlePieces.Add(piece);
            // 添加到列表中

        }
    }


    // 获取对应拼图碎片的预制体
    private GameObject GetPiecePrefab(int index)
    {
        switch (index)
        {
            case 0:
                return TL; // 左上角
            case 1:
                return TR; // 右上角
            case 2:
                return BL; // 左下角
            case 3:
                return BR; // 右下角
            default:
                return null;
        }
    }


    // 获取对应拼图碎片的位置

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

        // 生成随机偏移量
        float offsetX = Random.Range(minX, maxX);
        float offsetY = Random.Range(minY, maxY);
        float offsetX1 = Random.Range(minX1, maxX1);
        float offsetY1 = Random.Range(minY1, maxY1);

        switch (index)
        {
            case 0:
                return new Vector3(offsetX1, offsetY1, 0f); // 左上角位置
            case 1:
                return new Vector3(offsetX, offsetY, 0f); // 右上角位置
            case 2:
                return new Vector3(offsetX1, offsetY1, 0f); // 左下角位置
            case 3:
                return new Vector3(offsetX, offsetY, 0f); // 右下角位置
            default:
                return Vector3.zero;
        }
    }


}
