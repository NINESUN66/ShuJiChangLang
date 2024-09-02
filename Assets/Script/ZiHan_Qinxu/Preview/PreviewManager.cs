using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class PreviewManager : MonoBehaviour
{

    [SerializeField] private ImageManager imageManager; // ͼ������������ڱ���ͼ��
    [SerializeField] private Camera renderCamera; // ��Ⱦ���
    [SerializeField] private ToggleGroup toggleGroup; // �����飬���ڹ���������

    [SerializeField] private RawImage renderImg; // ��Ⱦͼ��� RawImage
    [SerializeField] private RawImage croppedImg; // �ü����ͼ��� RawImage
    [SerializeField] private RawImage scaledImg; // ���ź��ͼ��� RawImage

    private Texture2D texture2D_256; // ���ڴ洢 256x256 ��С��ͼ��
    private RenderTexture renderTexture; // ��Ⱦ����
    private Texture2D croppedTexture; // �ü����ͼ��
    private Texture2D scaledTexture; // ���ź��ͼ��

    // ��ȡ���ź��ͼ��
    public Texture2D ScaledTexture => scaledTexture;

    private void Start()
    {
        renderTexture = new RenderTexture(256, 256, 16); // ���� 256x256 ��С����Ⱦ����
        texture2D_256 = new Texture2D(256, 256, TextureFormat.RGB24, false); // ���� 256x256 ��С������2D����

        renderCamera.targetTexture = renderTexture; // ����Ⱦ��������Ϊ��Ⱦ�����Ŀ������
        if (renderImg) renderImg.texture = renderTexture; // ��ʾ��Ⱦ�����ͼ��
    }

    // Ԥ��ͼ��
    public void PreviewImage()
    {
        renderCamera.Render();

        ShowRenderImage(renderTexture);
        ShowCroppedImage(texture2D_256);
        ShowScaledImage(croppedTexture);
    }

    // ����ͼ��
    public void SaveImage()
    {
        renderCamera.Render(); // ��Ⱦ���

        ShowCroppedImage(texture2D_256); // ��ʾ�ü����ͼ��
        //imageManager.SaveImage(texture2D_256, 0); // ����ü����ͼ��
    }

    // ��ʾ��Ⱦ���ͼ��
    private void ShowRenderImage(RenderTexture rTexture)
    {
        RenderTexture.active = rTexture; // ������Ⱦ����
        Rect rectReadPixels = new Rect(0, 0, rTexture.width, rTexture.height);
        texture2D_256.ReadPixels(rectReadPixels, 0, 0); // ����Ⱦ�����ж�ȡ����
        texture2D_256.Apply(); // Ӧ�����ر仯
        if (renderImg) renderImg.texture = texture2D_256; // ��ʾ��Ⱦ���ͼ��
        RenderTexture.active = null; // ȡ��������Ⱦ����
    }

    // ��ʾ�ü����ͼ��
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

        if (croppedImg) croppedImg.texture = croppedTexture; // ��ʾ�ü����ͼ��
    }

    // ��ʾ���ź��ͼ��
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

        if (scaledImg) scaledImg.texture = scaledTexture; // ��ʾ���ź��ͼ��
    }


}
