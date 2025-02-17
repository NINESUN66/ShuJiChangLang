﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ImageManager : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, Texture2D>> LoadImages()
    {
        string dataPath = Application.persistentDataPath;
        Debug.Log("Data Path: " + dataPath);

        string[] labels = Directory.GetDirectories(Application.persistentDataPath)
            .Select(folderPath => Path.GetFileName(folderPath)).ToArray();

        Dictionary<string, Dictionary<string, Texture2D>> loadedDict = new Dictionary<string, Dictionary<string, Texture2D>>();
        if (labels.Length>0)
        {
            foreach (string label in labels)
            {
                string labelFolderPath =  Path.Combine(Application.persistentDataPath, label);
                string[] fileNames = Directory.GetFiles(labelFolderPath).Select(name => Path.GetFileName(name))
                    .ToArray();

                if (fileNames.Length>0)
                {
                    Dictionary<string, Texture2D> imageDict = new Dictionary<string, Texture2D>();
                    foreach (string fileName in fileNames)
                    {
                        if (fileName.Contains("meta"))
                            continue;
                        string imagePath = Path.Combine(labelFolderPath, fileName);
                        byte[] fileData = File.ReadAllBytes(imagePath);
                        Texture2D tex = new Texture2D(28, 28, TextureFormat.RGB24, false);
                        tex.LoadImage(fileData);
                        imageDict.Add(fileName, tex);
                    }

                    loadedDict.Add(label, imageDict);
                }
            }
        }
        
        return loadedDict;
    }

    public void DeleteImage(string folderName, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, folderName, fileName);
        if (File.Exists(filePath))
            File.Delete(filePath);
    }
}
