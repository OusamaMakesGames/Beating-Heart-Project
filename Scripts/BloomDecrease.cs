using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomDecrease : MonoBehaviour
{
    public PostProcessVolume volume;

    private Bloom bloom;

    public string Scene;

    public void Start()
    {
        volume.profile.TryGetSettings(out bloom);
        if (Scene == "")
        {
            bloom.intensity.value = 80;
        }
        else
        {
            bloom.intensity.value = 200;
        }
            
    }
    void Update()
    {
        if (Scene == "")
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 3, 1f * Time.deltaTime);
        }
        else
        {
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 10, 1f * Time.deltaTime);
        }
        
    }
}
