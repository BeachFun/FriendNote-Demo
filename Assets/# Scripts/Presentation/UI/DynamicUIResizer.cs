using UnityEngine;
using UnityEngine.UI;

namespace FriendNote.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DynamicUIResizer : MonoBehaviour // �� ��������� Pre alpha
    {
        [Header("Settings")]
        [SerializeField] private bool _isChangeWidth;
        [SerializeField] private bool _isChangeHeight;

        [Header("References")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
        [SerializeField] private ContentSizeFitter _contentSizeFitter;

        //[Space, SerializeField] private bool _needsResize = false;


        // ��� �����������, ����� �� ������������� ������ ����
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
                    // ��������� ������ ������� ��������� ��������
                    totalWidth += child.rect.width;
                }

                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, totalWidth);
            }

            if (_isChangeHeight)
            {
                float totalHeight = 0f;

                foreach (RectTransform child in _rectTransform)
                {
                    // ��������� ������ ������� ��������� ��������
                    totalHeight += child.rect.height;
                }

                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);
            }

            //_needsResize = false;
        }

        private void UpdateSizeDemo()
        {
            // �������� ����� ����� ���� �������� ���������
            float totalHeight = 0f;

            foreach (RectTransform child in _rectTransform)
            {
                // ��������� ������ ������� ��������� ��������
                totalHeight += child.rect.height;

                // ���� ���� ���������� ����� ����������, ��������� ��
                totalHeight += _verticalLayoutGroup.spacing;
            }

            // ��������� �������� ����������
            totalHeight += _verticalLayoutGroup.padding.top + _verticalLayoutGroup.padding.bottom;

            // ���� ������ ����������, ��������� ������
            if (Mathf.Abs(_lastContentHeight - totalHeight) > Mathf.Epsilon)
            {
                _lastContentHeight = totalHeight;

                // ������������� ����� ������ ����������
                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, totalHeight);

                // ��������� ContentSizeFitter (���� ����)
                if (_contentSizeFitter != null)
                {
                    _contentSizeFitter.SetLayoutVertical();
                }
            }
        }
    }
}
