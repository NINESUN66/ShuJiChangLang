using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShangPreview : MonoBehaviour
{
    public Dictionary<string, Texture2D> imagesDict;
    public MnistTest mnistTest;
    public RawImage shangImg;
    private string number;

    void Start()
    {
        imagesDict = new Dictionary<string, Texture2D>();
        number = mnistTest.result;
        LoadImages();
    }

    private void Update()
    {
        if (number != mnistTest.result)
        {
            number = mnistTest.result;

            if (imagesDict.ContainsKey(number))
            {
                shangImg.texture = imagesDict[number];
            }
            else
            {
                Debug.LogError("没有这个数字" + number);
            }
        }
    }

    void LoadImages()
    {
        Object[] textures = Resources.LoadAll("ShangImg", typeof(Texture2D));
        foreach (Object obj in textures)
        {
            Texture2D texture = (Texture2D)obj;
            imagesDict.Add(obj.name, texture);
        }
    }
}
