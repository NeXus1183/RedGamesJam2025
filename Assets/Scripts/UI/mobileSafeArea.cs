using UnityEngine;

public class mobileSafeArea : MonoBehaviour
{
    RectTransform rectTrans;
    Rect safeArea;
    Vector2 safeMin;
    Vector2 safeMax;
    void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        safeMin = safeArea.position;
        safeMax = safeArea.position + safeArea.size;

        safeMin.x /= Screen.width;
        safeMin.y /= Screen.height;
        safeMax.x /= Screen.width;
        safeMax.y /= Screen.height;

        rectTrans.anchorMin = safeMin;
        rectTrans.anchorMax = safeMax;
    }
}
