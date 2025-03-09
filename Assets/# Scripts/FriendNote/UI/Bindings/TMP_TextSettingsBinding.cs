using TMPro;
using UnityEngine;

namespace FriendNote
{
    [RequireComponent(typeof(TMP_Text))]

    [AddComponentMenu("UI/Binding/TMP_TextSettingsBinding")]

    [ExecuteAlways]
    public class TMP_TextSettingsBinding : MonoBehaviour
    {
        [SerializeField] private TMP_Text sourceText;

        private TMP_Text targetText;

        [ExecuteAlways]
        public void Awake()
        {
            targetText = GetComponent<TMP_Text>();

            CopyValues();
        }

#if UNITY_EDITOR
        [ExecuteAlways]
        public void Update()
        {
            CopyValues();
        }
#endif


        public void CopyValues()
        {
            if (sourceText is null) return;

            // Копирование основных свойств текста
            //targetText.text = sourceText.text;
            targetText.font = sourceText.font;
            targetText.fontSize = sourceText.fontSize;
            targetText.fontStyle = sourceText.fontStyle;
            targetText.fontWeight = sourceText.fontWeight;
            targetText.color = sourceText.color;
            targetText.outlineWidth = sourceText.outlineWidth;
            targetText.outlineColor = sourceText.outlineColor;
            targetText.richText = sourceText.richText;
            targetText.alignment = sourceText.alignment;
            targetText.enableAutoSizing = sourceText.enableAutoSizing;
            targetText.autoSizeTextContainer = sourceText.autoSizeTextContainer;
            targetText.fontSharedMaterial = sourceText.fontSharedMaterial;
            targetText.material = sourceText.material;
            targetText.isOverlay = sourceText.isOverlay;
            targetText.faceColor = sourceText.faceColor;
            targetText.outlineColor = sourceText.outlineColor;
            targetText.margin = sourceText.margin;
            targetText.characterSpacing = sourceText.characterSpacing;
            targetText.lineSpacing = sourceText.lineSpacing;
            targetText.paragraphSpacing = sourceText.paragraphSpacing;
            targetText.extraPadding = sourceText.extraPadding;
            targetText.overflowMode = sourceText.overflowMode;
            targetText.pageToDisplay = sourceText.pageToDisplay;
            targetText.maxVisibleCharacters = sourceText.maxVisibleCharacters;
            targetText.havePropertiesChanged = sourceText.havePropertiesChanged;
            targetText.horizontalMapping = sourceText.horizontalMapping;
            targetText.verticalMapping = sourceText.verticalMapping;
            targetText.renderMode = sourceText.renderMode;
        }
    }
}
