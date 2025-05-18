using UnityEngine;
using UnityEngine.UI;

namespace FriendNote
{
    [RequireComponent(typeof(Image))]

    [AddComponentMenu("UI/Binding/ImageColorBinding")]

    [ExecuteAlways]
    public class ImageColorBinding : MonoBehaviour
    {
        [SerializeField] private Image sourceImage;

        private Image targetImage;

        [ExecuteAlways]
        public void Awake()
        {
            targetImage = GetComponent<Image>();

            if (sourceImage is not null)
            {
                targetImage.color = sourceImage.color;
            }
        }

#if UNITY_EDITOR
        [ExecuteAlways]
        public void Update()
        {
            if (targetImage is null || sourceImage is null) return;

            targetImage.color = sourceImage.color;
        }
#endif

    }
}
