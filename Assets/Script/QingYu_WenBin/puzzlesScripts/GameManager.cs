using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public RawImage rawImage; // ����RawImage�����������ʾ���ص�ͼƬ
    public string imagePath; // Ԥ��ͼƬ��·��
    public Puzzleform puzzleform; // ����Puzzleform���ʵ��

    public GameObject StartPanel;
    public GameObject EndPanel;

    //����
    public AudioClip typeSound; // ��Ч�ļ�������
    private AudioSource audioSource; // AudioSource���������

    // ��̬�� GameManager ʵ��
    public static GameManager Instance { get; private set; }

    // ƴͼ����
    private int[] puzzle;

    void Start()
    {
        StartPanel.SetActive(true);
        // ����Ϸ����ʱ����Ԥ��ͼƬ
        //LoadPredefinedTexture(imagePath);

        //��������
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>(); // ���û��ͨ��Inspector���ã������Զ���ȡ������������ֶ����AudioSource���  
            if (!audioSource)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // ���GameObject��û��AudioSource����������һ��  
            }
            audioSource.clip = typeSound; // ������Ч�ļ�  
        }

        audioSource.loop = true;
        audioSource.Play(); // ������Ч 
    }

    void Awake()
    {
        // ���� GameManager ��ʵ��
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("��� GameManager ʵ��������������ܻᵼ��������Ϊ��");
            Destroy(gameObject); // ���ٶ����ʵ��
        }
    }

    public void LoadPredefinedTexture(string imagePath)
    {
        // ����Ԥ���ͼƬ·������ͼƬ��Դ
        Texture2D tex2d = LoadTextureFromFile(imagePath);
        // ��ȡRawImage��ʾ������
        Texture2D tex2d1 = (Texture2D)rawImage.texture;
        // ����RawImage
        rawImage.gameObject.SetActive(true);
            // �������ص�ͼƬ��СΪ340x340
            Texture2D resizedTex = ResizeTexture(tex2d1, 680, 680);

            // ���rawImage�Ƿ�Ϊ��
            if (rawImage != null)
            {
                // ��ʾ���ص�ͼƬ
                rawImage.texture = resizedTex;
                // ��ͼ�����ɫ����Ϊ��͸��
                Color transparentColor = new Color(1f, 1f, 1f, 0.5f); // ����alphaͨ��Ϊ0.5��ʾ��͸��
                rawImage.color = transparentColor;

                // �����ص������ݸ�Puzzleform��
                puzzleform.puzzleTexture = resizedTex;

                // ��ȡ���ص�����Ŀ�Ⱥ͸߶ȣ������ݸ�Puzzleform��
              //  puzzleform.SetTextureSize(resizedTex.width, resizedTex.height);
            }
            else
            {
                Debug.LogError("rawImage����δ��ֵ������Unity�༭���н�rawImage�ֶ����ӵ���Ӧ��RawImage����ϡ�");
            }
        
        InitPuzzleGrid();

        StartPanel.SetActive(false);

    }

    // ���������С
    private Texture2D ResizeTexture(Texture2D sourceTex, int newWidth, int newHeight)
    {
        Texture2D resizedTex = new Texture2D(newWidth, newHeight);
        Graphics.ConvertTexture(sourceTex, resizedTex);
        return resizedTex;
    }

    // ���ļ�����ͼƬ
    private Texture2D LoadTextureFromFile(string path)
    {
        // ���·���Ƿ����
        if (!System.IO.File.Exists(path))
        {
            //Debug.LogError("ָ����ͼƬ·�������ڣ�" + path);
            return null;
        }

        // ��ȡͼƬ����
        byte[] texByte = System.IO.File.ReadAllBytes(path);

        // �����µ�Texture2D������ͼƬ����
        Texture2D tex2d = new Texture2D(1, 1);
        tex2d.LoadImage(texByte);

      //  Debug.Log("���ص�ͼƬ�ֱ���Ϊ��" + tex2d.width + " x " + tex2d.height);

        return tex2d;
    }

    /// <summary>
    /// ��ʼ������
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
    /// ���ø������Ƿ�����Ƭ
    /// </summary>
    /// <param name="gridID"></param>
    public bool CheckHavePiece(int gridID)
    {
        if (gridID < 0 || gridID > 4)
        {
            Debug.LogError("��������Ƭʱ����������Χ");
            return false;
        }
        if (puzzle[gridID] != 99999)
        {
            return true;
        }
        return false;
    }


    /// <summary>
    /// ���������Ƭ
    /// </summary>
    /// <param name="gridID">����ID</param>
    /// <param name="pieceID">��ƬID</param>
    public void SetPiece(int gridID, int pieceID)
    {   
        if (gridID < 0 || gridID > 4)
        {
            Debug.LogError("������Ƭʱ����ID������Χ");
            return;
        }

        puzzle[gridID] = pieceID;
    }


    /// <summary>
    /// ȡ����Ƭ
    /// </summary>
    /// <param name="gridID">����ID</param>
    public void OutPiece(int gridID)
    {
        if (puzzle.Length <= gridID)
        {
            Debug.LogError("ȡ����Ƭʱ��������");
            return;
        }

        puzzle[gridID] = 99999;
    }

    /// <summary>
    /// ����Ƿ����ƴͼ
    /// </summary>
    /// <returns></returns>
    public bool IsFinish()
    {
        for (int i = 1; i <= 4; i++)
        {
            // �����ǰ����û�з�����Ƭ���ߵ�ǰ���ӵ���Ƭ������һ�����ӵ���Ƭ����ƴͼδ���
             if (puzzle[i] == 99999 || puzzle[i] != i)
             {

                 //Debug.Log("ƴͼδ��ɣ�");
                return false;
             }
        }

        EndPanel.SetActive(true);

        // �ڴ˴����ƴͼ��ɺ�Ĵ����߼�
        return true;
    }


}

