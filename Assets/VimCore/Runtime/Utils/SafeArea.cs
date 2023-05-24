using UnityEngine;

namespace VimCore.Runtime.Utils
{
    public class SafeArea : MonoBehaviour
    {
        private void Awake()
        {
            var rectTransform = GetComponent<RectTransform>();
            var safe = Screen.safeArea;
            rectTransform.anchorMin = new Vector2(
                Mathf.Max(rectTransform.anchorMin.x, safe.x / Screen.width),
                Mathf.Max(rectTransform.anchorMin.y, safe.y / Screen.height)
            );
            rectTransform.anchorMax = new Vector2(
                Mathf.Min(rectTransform.anchorMax.x,safe.x + safe.width / Screen.width), 
                Mathf.Min(rectTransform.anchorMax.y, safe.y + safe.height / Screen.height)
            );
        }
    }
}