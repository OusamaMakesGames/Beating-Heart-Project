using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour
{
    public Text timerText;
    private float timer;
    public bool isTiming;
    public GameObject Text;

    void Start()
    {
        // Initialize the timer text
        UpdateTimerText(0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) && PlayerPrefs.GetInt("Won") == 1)
        {
            Text.SetActive(!Text.activeSelf);

        }
        if (isTiming)
        {
            timer += Time.deltaTime;
            UpdateTimerText(timer);
        }
    }

    public void StartTimer()
    {
        isTiming = true;
        timer = 0f;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    void UpdateTimerText(float timeInSeconds)
    {
        // Format the time as minutes:seconds:milliseconds
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000f) % 1000f);

        // Update the timer text display
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
