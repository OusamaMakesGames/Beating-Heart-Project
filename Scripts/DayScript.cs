using UnityEngine;
using UnityEngine.SceneManagement;

public class DayScript : MonoBehaviour
{
    public bool MenuOpen;

    public GameObject MenuScreen;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma) && PlayerPrefs.GetInt("Won") == 1)
        {
            MenuOpen = !MenuOpen;
        }
        if (MenuOpen)
        {
            MenuScreen.SetActive(true);
        }
        else
        {
            MenuScreen.SetActive(false);
        }
        if (MenuOpen)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerPrefs.SetInt("Day", 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Bedroom");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayerPrefs.SetInt("Day", 2);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Bedroom");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayerPrefs.SetInt("Day", 3);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Bedroom");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PlayerPrefs.SetInt("Day", 4);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Bedroom");
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PlayerPrefs.SetInt("Day", 5);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Bedroom");
            }
        }
    }
}
