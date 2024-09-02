using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public RawImage rawImage; // 引用RawImage组件，用于显示加载的图片
    public string imagePath; // 预设图片的路径
    public Puzzleform puzzleform; // 引用Puzzleform类的实例

    public GameObject StartPanel;
    public GameObject EndPanel;

    //音乐
    public AudioClip typeSound; // 音效文件的引用
    private AudioSource audioSource; // AudioSource组件的引用

    // 静态的 GameManager 实例
    public static GameManager Instance { get; private set; }

    // 拼图数组
    private int[] puzzle;

    void Start()
    {
        StartPanel.SetActive(true);
        // 在游戏启动时加载预设图片
        //LoadPredefinedTexture(imagePath);

        //播放音乐
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>(); // 如果没有通过Inspector设置，则尝试自动获取，或者你可以手动添加AudioSource组件  
            if (!audioSource)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // 如果GameObject上没有AudioSource组件，则添加一个  
            }
            audioSource.clip = typeSound; // 设置音效文件  
        }

        audioSource.loop = true;
        audioSource.Play(); // 播放音效 
    }

    void Awake()
    {
        // 设置 GameManager 的实例
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("多个 GameManager 实例被创建，这可能会导致意外行为。");
            Destroy(gameObject); // 销毁多余的实例
        }
    }

    public void LoadPredefinedTexture(string imagePath)
    {
        // 根据预设的图片路径加载图片资源
        Texture2D tex2d = LoadTextureFromFile(imagePath);
        // 获取RawImage显示的纹理
        Texture2D tex2d1 = (Texture2D)rawImage.texture;
        // 启用RawImage
        rawImage.gameObject.SetActive(true);
            // 调整加载的图片大小为340x340
            Texture2D resizedTex = ResizeTexture(tex2d1, 680, 680);

            // 检查rawImage是否为空
            if (rawImage != null)
            {
                // 显示加载的图片
                rawImage.texture = resizedTex;
                // 将图像的颜色设置为半透明
                Color transparentColor = new Color(1f, 1f, 1f, 0.5f); // 设置alpha通道为0.5表示半透明
                rawImage.color = transparentColor;

                // 将加载的纹理传递给Puzzleform类
                puzzleform.puzzleTexture = resizedTex;

                // 获取加载的纹理的宽度和高度，并传递给Puzzleform类
              //  puzzleform.SetTextureSize(resizedTex.width, resizedTex.height);
            }
            else
            {
                Debug.LogError("rawImage对象未赋值，请在Unity编辑器中将rawImage字段链接到相应的RawImage组件上。");
            }
        
        InitPuzzleGrid();

        StartPanel.SetActive(false);

    }

    // 调整纹理大小
    private Texture2D ResizeTexture(Texture2D sourceTex, int newWidth, int newHeight)
    {
        Texture2D resizedTex = new Texture2D(newWidth, newHeight);
        Graphics.ConvertTexture(sourceTex, resizedTex);
        return resizedTex;
    }

    // 从文件加载图片
    private Texture2D LoadTextureFromFile(string path)
    {
        // 检查路径是否存在
        if (!System.IO.File.Exists(path))
        {
            //Debug.LogError("指定的图片路径不存在：" + path);
            return null;
        }

        // 读取图片数据
        byte[] texByte = System.IO.File.ReadAllBytes(path);

        // 创建新的Texture2D并加载图片数据
        Texture2D tex2d = new Texture2D(1, 1);
        tex2d.LoadImage(texByte);

      //  Debug.Log("加载的图片分辨率为：" + tex2d.width + " x " + tex2d.height);

        return tex2d;
    }

    /// <summary>
    /// 初始化格子
    /// </summary>
    void InitPuzzleGrid()
    {
        puzzle = null;
        puzzle = new int[8];
        for (int i = 0; i <= 4; i++)
        {
           
            puzzle[i] = 99999;
           
        }
    }

    /// <summary>
    /// 检测该格子下是否有碎片
    /// </summary>
    /// <param name="gridID"></param>
    public bool CheckHavePiece(int gridID)
    {
        if (gridID < 0 || gridID > 4)
        {
            Debug.LogError("检查格子碎片时索引超出范围");
            return false;
        }
        if (puzzle[gridID] != 99999)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// 网格填进碎片
    /// </summary>
    /// <param name="gridID">网格ID</param>
    /// <param name="pieceID">碎片ID</param>
    public void SetPiece(int gridID, int pieceID)
    {   
        if (gridID < 0 || gridID > 4)
        {
            Debug.LogError("设置碎片时网格ID超出范围");
            return;
        }

        puzzle[gridID] = pieceID;
    }


    /// <summary>
    /// 取出碎片
    /// </summary>
    /// <param name="gridID">网格ID</param>
    public void OutPiece(int gridID)
    {
        if (puzzle.Length <= gridID)
        {
            Debug.LogError("取出碎片时超出索引");
            return;
        }

        puzzle[gridID] = 99999;
    }

    /// <summary>
    /// 检测是否完成拼图
    /// </summary>
    /// <returns></returns>
    public bool IsFinish()
    {
        for (int i = 1; i <= 4; i++)
        {
            // 如果当前格子没有放置碎片或者当前格子的碎片大于下一个格子的碎片，则拼图未完成
             if (puzzle[i] == 99999 || puzzle[i] != i)
             {

                 //Debug.Log("拼图未完成！");
                return false;
             }
        }

        EndPanel.SetActive(true);

        // 在此处添加拼图完成后的处理逻辑
        return true;
    }


}

