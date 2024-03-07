using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class GlobalClickDetector 
{
    public static GameObject GetBoardCell()
    {
        List<RaycastResult> results = new List<RaycastResult>();

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointerEventData, results);
        GameObject cell = null;

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Checker"))
                return cell = null;
            if(result.gameObject.CompareTag("Board"))
                cell = result.gameObject;
        }
        return cell;
    }
}