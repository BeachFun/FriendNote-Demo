using UnityEngine;

namespace Bitsplash.DatePicker
{
    partial class DatePickerSettings
    {
        [SerializeField]
        private TextTypeEnum textType;

        public TextTypeEnum TextType
        {
            get { return textType; }
            set
            {
                textType = value;
                if (TextTypeChanged != null)
                    TextTypeChanged();
            }
        }

        [SerializeField]
        private TMPro.TMP_FontAsset fontAsset;

        public TMPro.TMP_FontAsset FontAsset
        {
            get { return fontAsset; }
            set
            {
                fontAsset = value;
                if (TextTypeChanged != null)
                    TextTypeChanged();
            }
        }
    }

}
