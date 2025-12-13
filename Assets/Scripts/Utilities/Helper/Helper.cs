using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public static class Helper
{
    private static PointerEventData eventDataCurrentPosition;
    private static List<RaycastResult> results;
    public static bool isOverUI()
    {
        eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Mouse.current.position.ReadValue() };
        results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
