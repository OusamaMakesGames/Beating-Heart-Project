using UnityEngine;

public class EnableDebugs : MonoBehaviour
{
    public GameObject Debug;
    void Start()
    {
        if (PlayerPrefs.GetInt("Won") == 1)
        {
            Debug.SetActive(true);
        }
    }
}
