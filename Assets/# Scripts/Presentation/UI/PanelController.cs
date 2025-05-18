using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FriendNote.UI
{
    public class PanelController : MonoBehaviour/*, IDragHandler, IEndDragHandler*/
    {
        [SerializeField] private RectTransform mainPanel; // ������ �� �������� ������
        [SerializeField] private RectTransform sidePanel; // ������ �� ������ ����������
        [SerializeField] private float slideDistance = -100f; // ����������, �� ������� ���������� ������

        [Header("Animation settings")]
        [SerializeField] private bool isSliding;
        [SerializeField] private DirectionSliding directionSliding;
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private float elapsedTime = 0f;


        private Vector3 originalPosition;
        private VerticalLayoutGroup _layoutGroup;
        private Coroutine _slideCoroutine;


        private void Awake()
        {
            isSliding = false;
            directionSliding = DirectionSliding.None;
            _layoutGroup = GetComponent<VerticalLayoutGroup>();
        }

        private void Start()
        {
            originalPosition = mainPanel.anchoredPosition; // ���������� �������� �������
        }

        public void OnDrag(PointerEventData eventData)
        {
            // TODO: ������� ��������� ��������� ������� � �������� ������� ����, ��� ��� ��� �������� ��������� ��������� ��������

            if (isSliding) return;

            if (_layoutGroup is null)
            {
                // �������� �������� ������ � ����������� �� ����������� ������
                Vector3 newPosition = mainPanel.anchoredPosition3D + new Vector3(eventData.delta.x, 0f, 0f);
            }
            // TODO: ������� �������� � ���������� ����������
            else
            {
                _layoutGroup.padding.left = (int)((float)_layoutGroup.padding.left + eventData.delta.x);
                if (_layoutGroup.padding.left > 0) _layoutGroup.padding.left = 0;
                if (_layoutGroup.padding.left < slideDistance) _layoutGroup.padding.left = (int)slideDistance;
                _layoutGroup.SetLayoutHorizontal();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isSliding) return;

            if (_layoutGroup is null)
            {
                // ���������, ����� �� �������� ������ ���������� ��� ������� �������� ������ �������
                if (mainPanel.anchoredPosition.x < originalPosition.x + slideDistance / 2f)
                {
                    mainPanel.anchoredPosition = new Vector3(slideDistance, originalPosition.y, 0f);
                }
                else
                {
                    mainPanel.anchoredPosition = originalPosition; // ���������� �������� ������ �������
                }
            }
            else
            {
                // ���������, ����� �� �������� ������ ���������� ��� ������� �������� ������ �������
                if (_layoutGroup.padding.left < (slideDistance) / 2f)
                {
                    _layoutGroup.padding.left = (int)slideDistance;
                }
                else
                {
                    _layoutGroup.padding.left = 0; // ���������� �������� ������ �������
                }
                _layoutGroup.SetLayoutHorizontal();
            }
        }

        private IEnumerator SlidePanel(float startPos, float endPos)
        {
            isSliding = true;

            if (_layoutGroup is null)
            {
                while (elapsedTime < duration)
                {
                    float newPosX = Mathf.Lerp(startPos, endPos, (elapsedTime / duration));
                    mainPanel.anchoredPosition = new Vector2(newPosX, mainPanel.anchoredPosition.y);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                mainPanel.anchoredPosition = new Vector2(endPos, mainPanel.anchoredPosition.y);
            }
            else
            {
                while (elapsedTime < duration)
                {
                    float newPosX = Mathf.Lerp(startPos, endPos, (elapsedTime / duration));
                    _layoutGroup.padding.left = (int)newPosX;
                    _layoutGroup.SetLayoutHorizontal();
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                _layoutGroup.padding.left = (int)endPos;
                _layoutGroup.SetLayoutHorizontal();
            }

            elapsedTime = 0f;
            isSliding = false;
            directionSliding = DirectionSliding.None;
        }

        /// <summary>
        /// ���������� ���� �������� ControlPanel
        /// </summary>
        public void TogglePanel()
        {
            try
            {
                if (isSliding)
                {
                    StopCoroutine(_slideCoroutine);

                    float targetPos;
                    if (directionSliding == DirectionSliding.Left)
                    {
                        targetPos = 0f;
                        directionSliding = DirectionSliding.Right;
                    }
                    else
                    {
                        targetPos = slideDistance;
                        directionSliding = DirectionSliding.Left;
                    }

                    elapsedTime = duration - elapsedTime;


                    if (_layoutGroup is null)
                    {
                        _slideCoroutine = StartCoroutine(SlidePanel(mainPanel.anchoredPosition.x, targetPos));
                    }
                    else
                    {
                        _slideCoroutine = StartCoroutine(SlidePanel(_layoutGroup.padding.left, targetPos));

                    }
                }
                else
                {
                    if (_layoutGroup is null)
                    {
                        float targetPos;
                        if (mainPanel.anchoredPosition.x < slideDistance / 2f)
                        {
                            targetPos = 0f;
                            directionSliding = DirectionSliding.Right;
                        }
                        else
                        {
                            targetPos = slideDistance;
                            directionSliding = DirectionSliding.Left;
                        }

                        _slideCoroutine = StartCoroutine(SlidePanel(mainPanel.anchoredPosition.x, targetPos));
                    }
                    else
                    {
                        float targetPos;
                        if (_layoutGroup.padding.left < slideDistance / 2f)
                        {
                            targetPos = 0f;
                            directionSliding = DirectionSliding.Right;
                        }
                        else
                        {
                            targetPos = slideDistance;
                            directionSliding = DirectionSliding.Left;
                        }

                        _slideCoroutine = StartCoroutine(SlidePanel(_layoutGroup.padding.left, targetPos));
                    }
                }
            }
            catch
            {

            }
        }

        private enum DirectionSliding
        {
            Left,
            None,
            Right
        }
    }
}
