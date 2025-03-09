using UnityEngine;

namespace FriendNote.UI
{
    [ExecuteAlways]
    public class SafeArea : MonoBehaviour
    {
        private void Awake()
        {
            UpdateSafeArea();
        }

#if UNITY_EDITOR
        [ExecuteAlways]
        private void Update()
        {
            UpdateSafeArea();
        }
#endif

        private void UpdateSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            var rectTransform = GetComponent<RectTransform>();

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}
