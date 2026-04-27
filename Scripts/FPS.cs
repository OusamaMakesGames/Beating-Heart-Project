using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    public float updateInterval = 0.5f; // Update interval in seconds

    private float lastInterval;
    private int frames = 0;
    private float fps;
    public TMP_Text text;

    private void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }

    private void Update()
    {
        frames++;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
    }

    private void OnGUI()
    {
        text.text = "FPS: " + fps.ToString("F0");
    }
}