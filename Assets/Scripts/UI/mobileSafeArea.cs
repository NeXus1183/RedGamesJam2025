using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class mobileSafeArea : MonoBehaviour
{
    private RectTransform rectTrans;
    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;
        Vector2 safeMin = safeArea.position;
        Vector2 safeMax = safeArea.position + safeArea.size;

        safeMin.x /= Screen.width;
        safeMin.y /= Screen.height;
        safeMax.x /= Screen.width;
        safeMax.y /= Screen.height;

        rectTrans.anchorMin = safeMin;
        rectTrans.anchorMax = safeMax;
    }
}
