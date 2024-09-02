using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class PreviewManager : MonoBehaviour
{

    [SerializeField] private ImageManager imageManager; // 图像管理器，用于保存图像
    [SerializeField] private Camera renderCamera; // 渲染相机
    [SerializeField] private ToggleGroup toggleGroup; // 开关组，用于管理多个开关

    [SerializeField] private RawImage renderImg; // 渲染图像的 RawImage
    [SerializeField] private RawImage croppedImg; // 裁剪后的图像的 RawImage
    [SerializeField] private RawImage scaledImg; // 缩放后的图像的 RawImage

    private Texture2D texture2D_256; // 用于存储 256x256 大小的图像
    private RenderTexture renderTexture; // 渲染纹理
    private Texture2D croppedTexture; // 裁剪后的图像
    private Texture2D scaledTexture; // 缩放后的图像

    // 获取缩放后的图像
    public Texture2D ScaledTexture => scaledTexture;

    private void Start()
    {
        renderTexture = new RenderTexture(256, 256, 16); // 创建 256x256 大小的渲染纹理
        texture2D_256 = new Texture2D(256, 256, TextureFormat.RGB24, false); // 创建 256x256 大小的纹理2D对象

        renderCamera.targetTexture = renderTexture; // 将渲染纹理设置为渲染相机的目标纹理
        if (renderImg) renderImg.texture = renderTexture; // 显示渲染纹理的图像
    }

    // 预览图像
    public void PreviewImage()
    {
        renderCamera.Render();

        ShowRenderImage(renderTexture);
        ShowCroppedImage(texture2D_256);
        ShowScaledImage(croppedTexture);
    }

    // 保存图像
    public void SaveImage()
    {
        renderCamera.Render(); // 渲染相机

        ShowCroppedImage(texture2D_256); // 显示裁剪后的图像
        //imageManager.SaveImage(texture2D_256, 0); // 保存裁剪后的图像
    }

    // 显示渲染后的图像
    private void ShowRenderImage(RenderTexture rTexture)
    {
        RenderTexture.active = rTexture; // 激活渲染纹理
        Rect rectReadPixels = new Rect(0, 0, rTexture.width, rTexture.height);
        texture2D_256.ReadPixels(rectReadPixels, 0, 0); // 从渲染纹理中读取像素
        texture2D_256.Apply(); // 应用像素变化
        if (renderImg) renderImg.texture = texture2D_256; // 显示渲染后的图像
        RenderTexture.active = null; // 取消激活渲染纹理
    }

    // 显示裁剪后的图像
    private void ShowCroppedImage(Texture2D texture)
    {
        int width = texture.width;
        int height = texture.height;

        int left = width;
        int right = 0;
        int bottom = height;
        int top = 0;
        Color[] pixels = texture.GetPixels();

        bool hasColorPixel = false;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color32 pixel = pixels[x + y * width];
                if (pixel.r > 0)
                {
                    hasColorPixel = true;
                    left = Mathf.Min(left, x);
                    right = Mathf.Max(right, x);
                    bottom = Mathf.Min(bottom, y);
                    top = Mathf.Max(top, y);
                }
            }
        }

        if (!hasColorPixel) return;

        int newWidth = right - left + 1;
        int newHeight = top - bottom + 1;

        croppedTexture = new Texture2D(newWidth, newHeight);
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

        if (croppedImg) croppedImg.texture = croppedTexture; // 显示裁剪后的图像
    }

    // 显示缩放后的图像
    private void ShowScaledImage(Texture2D croppedTexture)
    {
        if (!croppedTexture) return;

        Texture2D originalTexture = croppedTexture;

        int targetSize = 22;
        int paddedSize = targetSize + 2 * 3;

        float scale = Mathf.Min((float)targetSize / originalTexture.width, (float)targetSize / originalTexture.height);

        int newWidth = Mathf.RoundToInt(originalTexture.width * scale);
        int newHeight = Mathf.RoundToInt(originalTexture.height * scale);

        scaledTexture = new Texture2D(paddedSize, paddedSize);

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

        if (scaledImg) scaledImg.texture = scaledTexture; // 显示缩放后的图像
    }


}
