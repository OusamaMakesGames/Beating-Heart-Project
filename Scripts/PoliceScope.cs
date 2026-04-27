using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PoliceScope : MonoBehaviour
{
    public float moveDistance;

    public Color Highlighted, UnHighlighted;
    public RectTransform canvasRect;
    private RectTransform imageRect;

    void Start()
    {
        imageRect = GetComponent<RectTransform>();
        Application.targetFrameRate = 60;
    }

    private void MoveRight()
    {
        imageRect.anchoredPosition += Vector2.right * moveDistance * Time.deltaTime;
    }

    private void MoveLeft()
    {
        imageRect.anchoredPosition -= Vector2.right * moveDistance * Time.deltaTime;
    }
    private void MoveUp()
    {
        imageRect.anchoredPosition += Vector2.up * moveDistance * Time.deltaTime;
    }

    private void MoveDown()
    {
        imageRect.anchoredPosition -= Vector2.up * moveDistance * Time.deltaTime;
    }
    void Update()
    {
        Vector3 pos = imageRect.localPosition;

        float halfWidth = canvasRect.rect.width / 2f - imageRect.rect.width / 2f;
        float halfHeight = canvasRect.rect.height / 2f - imageRect.rect.height / 2f;

        pos.x = Mathf.Clamp(pos.x, -halfWidth, halfWidth);
        pos.y = Mathf.Clamp(pos.y, -halfHeight, halfHeight);

        imageRect.localPosition = pos;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
        Button[] buttons = FindObjectsOfType<Button>();

        Button buttonToHighlight = null;
        foreach (Button button in buttons)
        {
            if (IsGameObjectOnTopOfButton(button.gameObject))
            {
                buttonToHighlight = button;
                break;
            }
        }

        foreach (Button button in buttons)
        {
            if (button == buttonToHighlight)
                button.targetGraphic.color = Highlighted;
            else
                button.targetGraphic.color = UnHighlighted;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Button button in buttons)
            {
                if (IsGameObjectOnTopOfButton(button.gameObject))
                {
                    if (button == buttonToHighlight)
                    button.onClick.Invoke();
                    break;
                }
            }
        }
    }
    private bool IsGameObjectOnTopOfButton(GameObject buttonObject)
    {
        RectTransform gameObjectRect = GetComponent<RectTransform>();
        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();

        return RectTransformUtility.RectangleContainsScreenPoint(buttonRect, gameObjectRect.position);
    }
}
