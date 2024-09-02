using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generate : MonoBehaviour
{
    [SerializeField] private GameObject IMGParent;
    private Dictionary<string, Texture2D> imagesDict;
    private List<Transform> childList;
    private List<bool> isGeneral;

    void Start()
    {
        childList = new List<Transform>();
        imagesDict = new Dictionary<string, Texture2D>();
        LoadResources();
        childList = LoadObject(IMGParent.transform);
        isGeneral = LoadGenerate();

        Add_To_Image();
        Check_Generate(); // ����Ƿ��п�ͼƬ
    }

    private void LoadResources() // �������������ͼƬ
    {
        Object[] textures = Resources.LoadAll("ShangImg", typeof(Texture2D));
        foreach (Object obj in textures)
        {
            Texture2D texture = (Texture2D)obj;
            imagesDict.Add(obj.name, texture);
        }
    }

    private void Check_Generate() // ����Ƿ�ȫ�����õ�button�϶�������ͼƬ�����û�����������button
    {
        foreach (Transform child in IMGParent.transform)
        {
            RawImage rawImage = child.GetComponent<RawImage>();
            if (rawImage.texture == null)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    List<bool> LoadGenerate() // ����һ��bool���飬��ȡ10��true��λ��
    {
        List<bool> res = new List<bool>(new bool[childList.Count]);
        int trueCount = Mathf.Min(imagesDict.Count, res.Count);
        List<int> indices = new List<int>(res.Count);

        for (int i = 0; i < res.Count; i++)
        {
            indices.Add(i);
        }

        Shuffle(indices);

        for (int i = 0; i < trueCount; i++)
        {
            res[indices[i]] = true;
        }

        for (int i = trueCount; i < res.Count; i++)
        {
            res[indices[i]] = false;
        }

        return res;
    }

    public void Add_To_Image() // ����bool������true��λ�ý���ͼƬ���
    {
        List<Transform> Randchild = new List<Transform>();
        Shuffle(Randchild);

        List<string> imageKeys = new List<string>(imagesDict.Keys); // ������˳��������
        Shuffle(imageKeys);

        int childCount = childList.Count;
        int imageCount = imagesDict.Count;

        if (childCount < imageCount)
        {
            Debug.LogWarning("��������������");
            return;
        }

        int img_num = 0;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = childList[i];
            string imageName;
            Texture2D texture;
            if (img_num < imageCount)
            {
                imageName = imageKeys[img_num];
                texture = imagesDict[imageName];
            }
            else
            {
                // ͼƬ�Ѿ���������
                break;
            }

            RawImage rawImage = child.GetComponent<RawImage>();

            if (isGeneral[i] == true)
            {
                rawImage.texture = texture;
                rawImage.GetComponent<IMGAttribute>().ImgName = imageName;
                img_num++;
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    List<Transform> LoadObject(Transform Parent) // ��ȡ���е�������
    {
        List<Transform> temp = new List<Transform>();
        foreach (Transform child in IMGParent.transform)
        {
            temp.Add(child);
        }
        return temp;
    }
}
