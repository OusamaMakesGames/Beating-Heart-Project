using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionManager : MonoBehaviour
{
    public float maxSuspicion = 100f;
    public float suspicionIncreaseRate = 10f;
    public float suspicionDecreaseRate = 5f;

    public Slider suspicionSlider; // Reference to the UI slider for suspicion
    public float currentSuspicion = 0f;

    private void Start()
    {
        currentSuspicion = 0f;
        UpdateSuspicionUI();
    }

    private void Update()
    {
        IncreaseSuspicion();

        // Decrease suspicion over time
        if (currentSuspicion > 0f)
        {
            currentSuspicion -= Time.deltaTime * suspicionDecreaseRate;
            currentSuspicion = Mathf.Clamp(currentSuspicion, 0f, maxSuspicion);
            UpdateSuspicionUI();
        }
    }

    private void IncreaseSuspicion()
    {
        currentSuspicion += suspicionIncreaseRate * Time.deltaTime;
        currentSuspicion = Mathf.Clamp(currentSuspicion, 0f, maxSuspicion);
        UpdateSuspicionUI();

        if (currentSuspicion >= maxSuspicion)
        {
            // Game over or other relevant actions when suspicion is too high
            HandleSuspicionMaxReached();
        }
    }

    private void UpdateSuspicionUI()
    {
        if (suspicionSlider != null)
        {
            suspicionSlider.value = currentSuspicion / maxSuspicion;
        }
    }

    private void HandleSuspicionMaxReached()
    {
        Debug.Log("You were spotted!");
    }
}
