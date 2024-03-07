using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image imageComponent;
    [SerializeField] private RectTransform rectTransformComponent;

    public void SetNewColor(Color newColor) => imageComponent.color = newColor;
    public Vector2 GetPosition() => rectTransformComponent.anchoredPosition;
}
