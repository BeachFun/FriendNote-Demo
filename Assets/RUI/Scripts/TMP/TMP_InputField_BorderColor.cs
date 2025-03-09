using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RUI
{
    [RequireComponent(typeof(TMP_InputField))]
    [RequireComponent(typeof(TMP_InputField_Validator))]

    [AddComponentMenu("RUI/TMP_InputField - BorderColor")]
    public class TMP_InputField_BorderColor : MonoBehaviour
    {
        [Header("TargetGraphic colors")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color validColor = Color.green;
        [SerializeField] private Color invalidColor = Color.red;

        private TMP_InputField targetInputField;
        private TMP_InputField_Validator validator;


        private void Awake()
        {
            targetInputField = GetComponent<TMP_InputField>();
            validator = GetComponent<TMP_InputField_Validator>();

            targetInputField.onSelect.AddListener(OnSelect);
            targetInputField.onDeselect.AddListener(OnDeselect);
            validator.onStateChanged.AddListener(ChooseBorderColor);
        }

        private void Start()
        {
            validator.Validate();
            ChooseBorderColor(validator.State);
        }

        private void OnSelect(string str)
        {
            SwitchColors();
            ChooseBorderColor(validator.State);
        }

        private void OnDeselect(string str)
        {
            SwitchColors();
            ChooseBorderColor(validator.State);
        }

        private void ChooseBorderColor(ValidatorStatesEnum state)
        {
            switch (state)
            {
                case ValidatorStatesEnum.Valid:
                    targetInputField.targetGraphic.color = validColor;
                    break;

                case ValidatorStatesEnum.Invalid:
                    targetInputField.targetGraphic.color = invalidColor;
                    break;

                default:
                    targetInputField.targetGraphic.color = normalColor;
                    break;
            }
        }

        private void SwitchColors()
        {
            // Нужно для корректного отображения цвета во время редактирования и после

            Color bufferColor = targetInputField.colors.selectedColor;

            ColorBlock colorBlock = targetInputField.colors;
            colorBlock.selectedColor = normalColor;
            targetInputField.colors = colorBlock;

            normalColor = bufferColor;
        }
    }
}
