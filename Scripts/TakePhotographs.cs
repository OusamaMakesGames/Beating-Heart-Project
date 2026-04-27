using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.IO;

public class TakePhotographs : MonoBehaviour
{
    public int count;
    private FileSystemWatcher watcher;

    void Start()
    {
        string path = Application.streamingAssetsPath + "/Photographs";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        count = GetPhotoCount();

        watcher = new FileSystemWatcher(path);
        watcher.Filter = "*.*";
        watcher.IncludeSubdirectories = false;
        watcher.EnableRaisingEvents = true;

        watcher.Created += (s, e) => UpdateCount();
        watcher.Deleted += (s, e) => UpdateCount();
        watcher.Renamed += (s, e) => UpdateCount();
    }

    void UpdateCount()
    {
        count = GetPhotoCount();
    }

    int GetPhotoCount()
    {
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/Photographs", "*.*");

        int photoCount = 0;
        foreach (var file in files)
        {
            string ext = Path.GetExtension(file).ToLower();
            if (ext == ".png" || ext == ".jpg" || ext == ".jpeg")
                photoCount++;
        }
        return photoCount;
    }

    int GetNextPhotoIndex()
    {
        HashSet<int> usedIndices = new HashSet<int>();
        string[] files = Directory.GetFiles(Application.streamingAssetsPath + "/Photographs", "Photo_*.png");

        foreach (string file in files)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            if (name.StartsWith("Photo_"))
            {
                if (int.TryParse(name.Substring(6), out int index))
                    usedIndices.Add(index);
            }
        }

        int candidate = 1;
        while (usedIndices.Contains(candidate))
            candidate++;

        return candidate;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            int nextIndex = GetNextPhotoIndex();
            string path = Path.Combine(Application.streamingAssetsPath + "/Photographs", "Photo_" + nextIndex + ".png");
            ScreenCapture.CaptureScreenshot(path);
        }
    }
}
