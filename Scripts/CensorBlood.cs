using UnityEngine;

public class CensorBlood : MonoBehaviour
{
    public Material BloodPool;
    public Color RedColor, PinkColor;

    void Update()
    {
        if (PlayerPrefs.GetInt("BloodCensored") == 1)
        {
            BloodPool.color = PinkColor;
        }
        else
        {
            BloodPool.color = RedColor;
        }
    }
}
