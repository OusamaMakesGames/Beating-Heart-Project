using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DetectionIcon : MonoBehaviour
{
    public Camera playerCamera;
    public Transform suspiciousStudent, player;
    public Image detectionIcon;

    public float ringRadius = 200f;
    public float smoothSpeed = 5f;
    public float duration = 5;
    public float decreaseDuration = 1f;

    public float initialRotationOffset = 0f;

    public enum State { Idle, Detecting, Holding, Decreasing }
    public State currentState = State.Idle;

    private float elapsedTime = 0f;
    private float startScale;
    private float startAlpha;

    private Coroutine detectCoroutine;
    public bool FullyDetected;

    private Vector2 lastKnownDirection;

    private Color IconColor;

    private void Start()
    {
        detectionIcon.rectTransform.localScale = Vector3.zero;
        IconColor = detectionIcon.color;
        IconColor.a = 0f;
    }

    public void ShowDetection()
    {
        if (currentState == State.Detecting || currentState == State.Holding) return;

        if (detectCoroutine != null) StopCoroutine(detectCoroutine);
        detectCoroutine = StartCoroutine(LerpScaleAndAlpha());
    }

    public void HideDetection()
    {
        if (currentState == State.Decreasing) return;
        if (detectCoroutine != null)
        {
            StopCoroutine(detectCoroutine);
            detectCoroutine = null;
        }

        FullyDetected = false;
        currentState = State.Decreasing;
        elapsedTime = 0f;

        startScale = detectionIcon.rectTransform.localScale.y;
        startAlpha = IconColor.a;
        detectionIcon.color = IconColor;
    }

    private IEnumerator LerpScaleAndAlpha()
    {
        currentState = State.Detecting;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float eased = Mathf.SmoothStep(0f, 1f, t);

            float scale = Mathf.Lerp(detectionIcon.rectTransform.localScale.y, 0.8f, eased * Time.deltaTime);
            float alpha = Mathf.Lerp(IconColor.a, 0.85f, eased * Time.deltaTime);

            detectionIcon.rectTransform.localScale = new Vector3(1f, scale, 1f);
            IconColor.a = alpha;
            detectionIcon.color = IconColor;

            if (Mathf.Abs(0.8f - scale) < 0.01f && Mathf.Abs(0.85f - alpha) < 0.01f)
            {
                break;
            }

            yield return null;
        }

        detectionIcon.rectTransform.localScale = new Vector3(1f, 0.8f, 1f);
        IconColor.a = 0.85f;
        detectionIcon.color = IconColor;
        currentState = State.Holding;
        FullyDetected = true;
    }

    private void Update()
    {
        if (!suspiciousStudent || !playerCamera || !detectionIcon) return;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        Vector3 playerScreenPos = playerCamera.WorldToScreenPoint(player.transform.position);
        Vector3 npcScreenPos = playerCamera.WorldToScreenPoint(suspiciousStudent.position);

        Vector2 screenDir = (npcScreenPos - playerScreenPos);

        if (npcScreenPos.z < 0f)
        {
            screenDir *= -1f;
        }

        Vector2 direction2D = screenDir.normalized;

        Vector3 targetPosition = screenCenter + new Vector3(direction2D.x, direction2D.y, 0f) * ringRadius;

        detectionIcon.rectTransform.position = Vector3.Lerp(
            detectionIcon.rectTransform.position,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );

        float angle = Mathf.Atan2(direction2D.y, direction2D.x) * Mathf.Rad2Deg;
        float iconUpOffset = -90f;
        float finalRotation = angle + iconUpOffset + initialRotationOffset;

        detectionIcon.rectTransform.rotation = Quaternion.Lerp(
            detectionIcon.rectTransform.rotation,
            Quaternion.Euler(0f, 0f, finalRotation),
            Time.deltaTime * smoothSpeed
        );

        if (currentState == State.Decreasing)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / decreaseDuration);
            float scale = Mathf.Lerp(startScale, 0.15f, t);
            float alpha = Mathf.Lerp(startAlpha, 0f, Mathf.SmoothStep(0f, 1f, t));

            detectionIcon.rectTransform.localScale = new Vector3(1f, scale, 1f);
            IconColor.a = alpha;
            detectionIcon.color = IconColor;

            if (t >= 1f)
            {
                currentState = State.Idle;
            }
        }
    }



}
