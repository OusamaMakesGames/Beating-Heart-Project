using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartDay : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Resolutions");
        }
    }
}
