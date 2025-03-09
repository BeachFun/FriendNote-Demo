using UnityEngine;
using UnityEngine.UI;

namespace RUI
{
    [RequireComponent(typeof(Toggle))]

    [AddComponentMenu("RUI/Toggle - ColorChanger")]
    public class ToggleColorChanger : MonoBehaviour
    {
        [Header("TargetGraphic colors")]
        [SerializeField] private Color selectedColor = Color.green;
        [SerializeField] private Color normalColor = Color.white;

        [Header("Debug EditMode")]
        [SerializeField] private bool isOn;

        private Toggle _toggle;


#if UNITY_EDITOR
        private void OnValidate()
        {
            var toggle = GetComponent<Toggle>();
            if (toggle != null)
            {
                toggle.isOn = isOn;
                toggle.targetGraphic.color = isOn ? selectedColor : normalColor;
            }
        }
#endif


        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            _toggle.onValueChanged.AddListener(OnToggleStateChanged);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleStateChanged);
        }


        public void OnToggleStateChanged(bool isOn)
        {
            if (_toggle.targetGraphic != null)
            {
                // Меняем цвет в зависимости от состояния togglе
                _toggle.targetGraphic.color = isOn ? selectedColor : normalColor;
            }
        }
    }
}
