using UnityEngine;

public class GameOverBugFix : MonoBehaviour
{
    public GameObject GOCanvas;
    public Transform Sakura;
    public Vector3 NewPosition;

    void Update()
    {
        if (GOCanvas.activeSelf)
        {
            Sakura.position = NewPosition;
        }
    }
}
