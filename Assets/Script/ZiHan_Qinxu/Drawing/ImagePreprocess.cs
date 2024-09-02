using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class ImagePreprocess : MonoBehaviour
{
    private string[] folderNames;
    private string[] imageNames;
    private string persistenDataPath;

    public void StartProcess()
    {
        persistenDataPath = Application.persistentDataPath;
        
        SetUpFolders();
        if (folderNames.Length <= 0) return;
        
        foreach (string folderName in folderNames)
        {
            imageNames = LoadImagesInFolder(folderName);
            StartCoroutine(ProcessImagesCoroutine(imageNames, folderName));
        }
        Debug.Log("处理完成");
    }

    private void SetUpFolders() 
    {
        folderNames = Directory.GetDirectories(persistenDataPath)
            .Select(folderPath => Path.GetFileName(folderPath))
            .ToArray();

        if (folderNames.Length > 0)
        {
            foreach (string label in folderNames)
            {
                if (label.Contains("copy"))
                {
                    string copyfolderPath = Path.Combine(persistenDataPath, label);
                    if (!IsDirectoryEmpty(copyfolderPath))
                    {
                        throw new Exception("copy folders are not empty.");
                    }
                    continue;
                }
                string folderPath = Path.Combine(persistenDataPath, label+"_copy");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
            }
        }

        folderNames = folderNames.Where(filename => !filename.Contains("copy")).ToArray();
    }
    
    private string[] LoadImagesInFolder(string folderName)  //加载文件夹中的所有图片
    {
        string labelFolderPath =  Path.Combine(persistenDataPath, folderName);
        return Directory.GetFiles(labelFolderPath).Select(name => Path.GetFileName(name))
            .ToArray();
    }
    
    private IEnumerator ProcessImagesCoroutine(string[] imageNames, string label) // 处理图片并且保存
    {
        if (imageNames.Length <= 0) yield break;
            
        for (int i = 0; i < imageNames.Length; i++)
        {
            if (imageNames[i].Contains("meta"))continue;
            
            yield return new WaitUntil(() =>
            {
                string labelFolderPath =  Path.Combine(persistenDataPath, label);

                string imagePath = Path.Combine(labelFolderPath, imageNames[i]);
                byte[] fileData = File.ReadAllBytes(imagePath);
                Texture2D tex = new Texture2D(28, 28, TextureFormat.RGB24, false);
                tex.LoadImage(fileData);
                SaveImage(ProcessImage(tex), label);
                return true;
            });
        }
    }


    private void SaveImage(Texture2D newImage, string folderName)// 保存图片
    {
        string folderPath = Path.Combine(persistenDataPath, folderName+"_copy");
        
        byte[] jpgData = newImage.EncodeToJPG();
        string filePath = Path.Combine(folderPath , folderName + "_" + DateTimeOffset.Now.ToUnixTimeMilliseconds() + ".jpg");
        
        if (!File.Exists(filePath))
            File.WriteAllBytes(filePath, jpgData);
    } 
    
    public bool IsDirectoryEmpty(string path) // 判断文件夹是否为空
    {
        return !Directory.EnumerateFileSystemEntries(path).Any();
    }

    private Texture2D ProcessImage(Texture2D texture2D) // 返回裁剪并且缩放后的图片
    {
        return ScaledImage(CroppedImage(texture2D));
    }
    
    private Texture2D CroppedImage(Texture2D texture)// 裁剪图片
    {
        int width = texture.width;
        int height = texture.height;

        int left = width;
        int right = 0;
        int bottom = height;
        int top = 0;
        Color[] pixels = texture.GetPixels();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[x + y * width];
                if (pixel.r > 50)
                {
                    left = Mathf.Min(left, x);
                    right = Mathf.Max(right, x);
                    bottom = Mathf.Min(bottom, y);
                    top = Mathf.Max(top, y);
                }
            }
        }

        int newWidth = right - left+1 ;
        int newHeight = top - bottom+1 ;

        Texture2D croppedTexture = new Texture2D(newWidth, newHeight);
        Color[] croppedPixels = new Color[newWidth * newHeight];
        for (int y = bottom; y <= top; y++)
        {
            for (int x = left; x <= right; x++)
            {
                int sourceIndex = x + y * width;
                int targetIndex = (x - left) + (y - bottom) * newWidth;
                croppedPixels[targetIndex] = pixels[sourceIndex];
            }
        }
        croppedTexture.SetPixels(croppedPixels);
        croppedTexture.Apply();

        return croppedTexture;
    } 

    private Texture2D ScaledImage(Texture2D croppedTexture)// 缩放图片
    {
        Texture2D originalTexture = croppedTexture;

        int targetSize = 22;
        int paddedSize = targetSize + 2 * 3;

        float scale = Mathf.Min((float)targetSize  / originalTexture.width, (float)targetSize  / originalTexture.height);

        int newWidth = Mathf.RoundToInt(originalTexture.width * scale);
        int newHeight = Mathf.RoundToInt(originalTexture.height * scale);

        Texture2D scaledTexture = new Texture2D(paddedSize, paddedSize);

        int offsetX = (paddedSize - newWidth) / 2;
        int offsetY = (paddedSize - newHeight) / 2;

        for (int y = 0; y < paddedSize; y++)
        {
            for (int x = 0; x < paddedSize; x++)
            {
                if (x < offsetX || x >= offsetX + newWidth || y < offsetY || y >= offsetY + newHeight)
                {
                    scaledTexture.SetPixel(x, y, Color.black);
                }
                else
                {
                    float u = (float)(x - offsetX) / newWidth;
                    float v = (float)(y - offsetY) / newHeight;
                    scaledTexture.SetPixel(x, y, originalTexture.GetPixelBilinear(u, v));
                }
            }
        }

        scaledTexture.Apply();

        return scaledTexture;
    } 
}
