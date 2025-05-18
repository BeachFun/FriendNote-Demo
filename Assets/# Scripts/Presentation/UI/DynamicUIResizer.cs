using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DynamicUIResizer : MonoBehaviour // Не доработан Pre alpha
    {
        [Header("Settings")]
        [SerializeField] private bool _isChangeWidth;
        [SerializeField] private bool _isChangeHeight;

        [Header("References")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private ContentSizeFitter _contentSizeFitter;

        //[Space, SerializeField] private bool _needsResize = false;


        // Для оптимизации, чтобы не пересчитывать каждый кадр
        private float _lastContentHeight = -1f;


#if UNITY_EDITOR
        void Awake()
        {

        }
#endif

#if UNITY_EDITOR
        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
#endif

#if UNITY_EDITOR
        private void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
            UpdateSize();
        }
#endif


        private void UpdateSize()
        {
            if (_isChangeWidth)
            {
                float totalWidth = 0f;

                foreach (RectTransform child in _rectTransform)
                {
                    // Добавляем высоту каждого дочернего элемента
                    totalWidth += child.rect.width;
                }

                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);
            }

            if (_isChangeHeight)
            {
                float totalHeight = 0f;

                foreach (RectTransform child in _rectTransform)
                {
                    // Добавляем высоту каждого дочернего элемента
                    totalHeight += child.rect.height;
                }

                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
            }

            //_needsResize = false;
        }

        private void UpdateSizeDemo()
        {
            // Получаем сумму высот всех дочерних элементов
            float totalHeight = 0f;

            foreach (RectTransform child in _rectTransform)
            {
                // Добавляем высоту каждого дочернего элемента
                totalHeight += child.rect.height;

                // Если есть промежутки между элементами, добавляем их
                totalHeight += _verticalLayoutGroup.spacing;
            }

            // Добавляем паддинги контейнера
            totalHeight += _verticalLayoutGroup.padding.top + _verticalLayoutGroup.padding.bottom;

            // Если высота изменилась, обновляем размер
            if (Mathf.Abs(_lastContentHeight - totalHeight) > Mathf.Epsilon)
            {
                _lastContentHeight = totalHeight;

                // Устанавливаем новый размер контейнера
                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);

                // Обновляем ContentSizeFitter (если есть)
                if (_contentSizeFitter != null)
                {
                    _contentSizeFitter.SetLayoutVertical();
                }
            }
        }
    }
}
