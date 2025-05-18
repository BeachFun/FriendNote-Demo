using System.Collections.Generic;
using System.Linq;
using FriendNote.Domain.DTO;
using RUI;
using TMPro;
using UnityEngine;

namespace FriendNote.UI
{
    public class UI_EditSection_Address : MonoBehaviour, IDataUpdatable<Address>, ICollectable<Address>, IFieldsReseting
    {
        [Header("Settings")]
        [SerializeField] private bool isNameplace = true;
        [SerializeField] private bool isDescription = true;
        [SerializeField] private bool isCountry = true;
        [SerializeField] private bool isState = true;
        [SerializeField] private bool isCity = true;
        [SerializeField] private bool isStreet = true;
        [SerializeField] private bool isHouse = true;
        [SerializeField] private bool isApartment = true;
        [SerializeField] private bool isPostalCode = true;

        [Space(10), Header("Events callbacks")]
        [Space(5), SerializeField] private TMP_InputField.SelectionEvent onSelect = new();
        [Space(5), SerializeField] private TMP_InputField.SelectionEvent onDeselect = new();
        [Space(5), SerializeField] private TMP_InputField.OnChangeEvent onValueChanged = new();
        [Space(5), SerializeField] private TMP_InputField.SubmitEvent onEndEdit = new();

        [Header("Requiered Page Fields References")]
        [SerializeField] private TMP_InputField inputFieldNameplace;
        [SerializeField] private TMP_InputField inputFieldDescription;
        [SerializeField] private TMP_InputField inputFieldCountry;
        [SerializeField] private TMP_InputField inputFieldState;
        [SerializeField] private TMP_InputField inputFieldCity;
        [SerializeField] private TMP_InputField inputFieldStreet;
        [SerializeField] private TMP_InputField inputFieldHouse;
        [SerializeField] private TMP_InputField inputFieldApartment;
        [SerializeField] private TMP_InputField inputFieldPostalCode;

        private readonly List<TMP_InputField> inputFields = new();
        private Address _data;


        public bool IsContainValidData
        {
            get => inputFields.All(x => x.GetComponent<TMP_InputField_Validator>().IsContainValidData);
        }
        public TMP_InputField.SelectionEvent OnSelect { get; private set; } = new();
        public TMP_InputField.SelectionEvent OnDeselect { get; private set; } = new();
        public TMP_InputField.OnChangeEvent OnValueChanged { get; private set; } = new();
        public TMP_InputField.SubmitEvent OnEndEdit { get; private set; } = new();


        private void OnValidate()
        {
            inputFieldNameplace?.gameObject.SetActive(isNameplace);
            inputFieldDescription?.gameObject.SetActive(isDescription);
            inputFieldCountry?.gameObject.SetActive(isCountry);
            inputFieldState?.gameObject.SetActive(isState);
            inputFieldCity?.gameObject.SetActive(isCity);
            inputFieldStreet?.gameObject.SetActive(isStreet);
            inputFieldHouse?.gameObject.SetActive(isHouse);
            inputFieldApartment?.gameObject.SetActive(isApartment);
            inputFieldPostalCode?.gameObject.SetActive(isPostalCode);
        }


        private void Awake()
        {
            inputFields.Add(inputFieldNameplace);
            inputFields.Add(inputFieldDescription);
            inputFields.Add(inputFieldCountry);
            inputFields.Add(inputFieldState);
            inputFields.Add(inputFieldCity);
            inputFields.Add(inputFieldStreet);
            inputFields.Add(inputFieldHouse);
            inputFields.Add(inputFieldApartment);
            inputFields.Add(inputFieldPostalCode);

            // �������� �� ������� ���� TMP_InputField
            inputFields.ForEach(x =>
            {
                x?.onSelect.AddListener(OnSelectChildTrigger);
                x?.onValueChanged.AddListener(OnValueChangedChildTrigger);
                x?.onEndEdit.AddListener(OnEndEditChildTrigger);
                x?.onDeselect.AddListener(OnDeselectChildTrigger);
            });
        }

        private void OnDestroy()
        {
            // �������� �� ������� ���� TMP_InputField
            inputFields.ForEach(x =>
            {
                x?.onSelect.RemoveListener(OnSelectChildTrigger);
                x?.onValueChanged.RemoveListener(OnValueChangedChildTrigger);
                x?.onEndEdit.RemoveListener(OnEndEditChildTrigger);
                x?.onDeselect.RemoveListener(OnDeselectChildTrigger);
            });
        }

        public void UpdateData(Address Address)
        {
            if (Address == null) return;

            _data = Address;

            inputFieldCountry.text = _data.Country;
            inputFieldState.text = _data.State;
            inputFieldCity.text = _data.City;
            inputFieldStreet.text = _data.Street;
            inputFieldHouse.text = _data.House;
            inputFieldApartment.text = _data.Apartment;
            inputFieldPostalCode.text = _data.PostalCode;
        }

        public void ResetFields()
        {
            inputFields.ForEach(x =>
            {
                x.text = string.Empty;
            });
        }

        public Address CollectData()
        {
            if (_data is null) _data = new();

            _data.Id = _data.Id;
            _data.Country = inputFieldCountry.text;
            _data.State = inputFieldState.text;
            _data.City = inputFieldCity.text;
            _data.Street = inputFieldStreet.text;
            _data.House = inputFieldHouse.text;
            _data.Apartment = inputFieldApartment.text;
            _data.PostalCode = inputFieldPostalCode.text;

            return _data;
        }


        protected void OnSelectChildTrigger(string text) => onSelect?.Invoke(text);
        protected void OnValueChangedChildTrigger(string text) => onValueChanged?.Invoke(text);
        protected void OnEndEditChildTrigger(string text) => onEndEdit?.Invoke(text);
        protected void OnDeselectChildTrigger(string text) => onDeselect?.Invoke(text);
    }
}
