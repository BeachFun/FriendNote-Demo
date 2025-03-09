using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RUI
{
    /// <summary>
    /// —крипт предназначен дл€ подсвечивани€ общего border дл€ группы RUI/InputField в соответствии из их правилами валидации заполн€емого текста
    /// </summary>
    [AddComponentMenu("RUI/InputFieldGroup - BorderColor")]
    public class UI_InputFieldGroup_BorderColor : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image border;
        [SerializeField] private InputField[] inputFields;

        [Header("Border color settings")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectedColor = Color.cyan;
        [SerializeField] private Color validColor = Color.green;
        [SerializeField] private Color invalidColor = Color.red;

        [Header("For EditMode")]
        [Space, SerializeField] private bool IsSelected;
        [SerializeField] private bool IsCorrect;
        [SerializeField] private bool IsIncorrect;

        private bool isSelected;


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isEditor)
            {
                border.enabled = true;

                if (!IsSelected && !IsCorrect && !IsIncorrect)
                {
                    border.color = normalColor;
                }

                if (IsSelected)
                {
                    border.color = selectedColor;
                }

                if (IsCorrect)
                {
                    border.color = validColor;
                }

                if (IsIncorrect)
                {
                    border.color = invalidColor;
                }
            }
        }
#endif


        private void Awake()
        {
            border.enabled = true;

            foreach (var item in inputFields)
            {
                item.onSelect.AddListener(Select);
                item.onDeselect.AddListener(Deselect);
                item.onValueChanged.AddListener(Validate);
            }
        }

        private void Start()
        {
            // ≈сли пол€ инициализировались значени€ми до Start, то подсветить
            if (inputFields.All(x => string.IsNullOrEmpty(x.Text))) return;
            border.color = inputFields.All(x => x.TextIsValid) ? validColor : invalidColor;
        }

        private void Select(string text)
        {
            isSelected = true;
            border.color = selectedColor;
        }

        private void Validate(string text)
        {
            if (inputFields.All(x => x.TextIsValid))
            {
                border.color = isSelected ? selectedColor : normalColor;
            }
            else
            {
                border.color = invalidColor;
            }
        }

        private void Deselect(string text)
        {
            isSelected = false;
            if (inputFields.All(x => string.IsNullOrEmpty(x.Text) && x.TextIsValid))
            {
                border.color = normalColor;
            }
            else if (inputFields.All(x => x.TextIsValid))
            {
                border.color = validColor;
            }
            else
            {
                border.color = invalidColor;
            }
        }
    }
}
