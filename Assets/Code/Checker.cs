using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Checker : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    private RectTransform current;
    private RectTransform m_DraggingPlane;
    private Image m_imageComponent;
    private Color32 m_baseColor;
    private Color32 m_colorUsingChecker = new Color32(48, 169, 25, 255);
    private Vector2 m_previousPosition;
    private bool m_isSelected = false;

    void Awake()
    {
        current = GetComponent<RectTransform>();
        m_imageComponent = GetComponent<Image>();
        m_baseColor = m_imageComponent.color;
    }
    public void SetNewPosition(Vector2 newPosition) => current.anchoredPosition = newPosition;
    private void ChangeCheckerColor(bool isPicked) => m_imageComponent.color = isPicked ? m_colorUsingChecker : m_baseColor;
    IEnumerator WaitForMouseClick()
    {
        int clickCount = 0;

        while (clickCount < 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;

                if (clickCount == 2)
                {
                    m_isSelected = false;
                    if (GlobalClickDetector.GetBoardCell() != null && GlobalClickDetector.GetBoardCell() != current)
                        current.localPosition = GlobalClickDetector.GetBoardCell().transform.localPosition;
                    ChangeCheckerColor(false);
                    yield break;
                }
            }
            yield return null;
        }
    }

    #region Event Handlers

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (DropDownOption.GetOption() == 1)
        {
            m_previousPosition = current.anchoredPosition;
            gameObject.tag = "Player";
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        switch (DropDownOption.GetOption())
        {
            case 0:
                ChangeCheckerColor(true);
                AnchoredDragPsotion();
                break;
            case 1:
                ChangeCheckerColor(true);
                FreeDragPosition(eventData);
                break;
            default:
                break;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (DropDownOption.GetOption() == 1)
        {
            if (GlobalClickDetector.GetBoardCell() != null && GlobalClickDetector.GetBoardCell() != current)
                current.localPosition = GlobalClickDetector.GetBoardCell().transform.localPosition;
            else
                current.anchoredPosition = m_previousPosition;
            gameObject.tag = "Checker";
        }
        ChangeCheckerColor(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (DropDownOption.GetOption() == 2)
            SetPositionOnClick();
    }


    #endregion

    #region Mode Methods 


    /// <summary>
    /// Режим 0
    /// Режим перемещение по сетке с привязкой к ячейке
    /// </summary>
    private void AnchoredDragPsotion()
    {
        if (GlobalClickDetector.GetBoardCell() != null && GlobalClickDetector.GetBoardCell() != current)
            current.localPosition = GlobalClickDetector.GetBoardCell().transform.localPosition;
    }


    /// <summary>
    /// Режим 1
    /// Режим свободного перемещения
    /// </summary>
    private void FreeDragPosition(PointerEventData data)
    {
        if (data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_DraggingPlane = data.pointerEnter.transform as RectTransform;

        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            transform.position = globalMousePos;
        }
    }


    /// <summary>
    /// Режим 2
    /// Перемещение по клику
    /// Первый клик - выбор шашки
    /// Второй клик - установка позиции для шашки
    /// </summary>
    private void SetPositionOnClick()
    {
        if (!m_isSelected)
        {
            m_isSelected = true;
            ChangeCheckerColor(true);
            StartCoroutine(WaitForMouseClick());
        }
    }


#endregion
}