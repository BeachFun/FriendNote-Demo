using FriendNote.Core.DTO;
using FriendNote.UI.Cards;
using Observer;
using TMPro;
using UnityEngine;

namespace FriendNote.UI.Pages
{
    public class InfoPage_PersonProfile : InfoPage, IFieldsReseting
    {
        [Header("Required Page Fields references")]
        [SerializeField] private TMP_Text textNickname;
        [SerializeField] private UI_Card_PersonBasicInfo cardPersonBaseInfo;
        [SerializeField] private UI_Section_ContactInfo sectionContactInfo;
        [SerializeField] private TMP_Text textNotation;

        private PersonProfile _personProfile;


        #region [Методы] Управление жизненным циклом

        private void Awake()
        {
            ResetFields();
            Messenger.AddListener(Notices.UINotice.EDIT_PAGE_PERSON_PROFILE_CLOSED, OnPersonUpdate);
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(Notices.UINotice.EDIT_PAGE_PERSON_PROFILE_CLOSED, OnPersonUpdate);
        }

        #endregion


        public override void DataUpdate(int id)
        {
            ResetFields();

            _personProfile = Services.Data.LoadPersonProfile(id);

            PageUpdate();
        }

        public override void PageUpdate()
        {
            if (_personProfile is null) return;

            PersonBasicInfo basicInfo = _personProfile.BasicInfo;

            textNickname.text = string.IsNullOrEmpty(basicInfo.Nickname)
                ? $"{basicInfo.Surname} {basicInfo.Name}"
                : basicInfo.Nickname;

            cardPersonBaseInfo.UpdateData(basicInfo);

            textNotation.text = string.IsNullOrEmpty(basicInfo.Notation)
                ? _emptyField
                : basicInfo.Notation;

            sectionContactInfo.UpdateData(_personProfile.ContactInfo);
        }

        public void ResetFields()
        {
            textNickname.text = _emptyField;
            cardPersonBaseInfo.UpdateData(new());
            textNotation.text = _emptyField;
            sectionContactInfo.UpdateData(new());
        }

        public override void Close()
        {
            base.Close();
            Messenger.Broadcast(Notices.UINotice.INFO_PAGE_PERSON_PROFILE_CLOSED);
        }



        /// <summary>
        /// Открывает страницу мест проживания данного знакомого
        /// </summary>
        public void OpenPagePersonResidences() => Controllers.Pages.OpenInfoPage<Residence>(_personProfile.BasicInfo.Id.Value);

        /// <summary>
        /// Открывает страницу с информацией об образовании данного знакомого
        /// </summary>
        public void OpenPagePersonEducation() => Controllers.Pages.OpenInfoPage<Education>(_personProfile.BasicInfo.Id.Value);

        /// <summary>
        /// Открывает страницу с информацией о карьере данного знакомого
        /// </summary>
        public void OpenPagePersonCareer() => Controllers.Pages.OpenInfoPage<WorkPosition>(_personProfile.BasicInfo.Id.Value);

        /// <summary>
        /// Открывает страницу скилов данного знакомого
        /// </summary>
        public void OpenPagePersonSkills() => Controllers.Pages.OpenInfoPage<Skill>(_personProfile.BasicInfo.Id.Value);

        /// <summary>
        /// Открывает страницу интересов данного знакомого
        /// </summary>
        public void OpenPagePersonInterests() => Controllers.Pages.OpenInfoPage<Interest>(_personProfile.BasicInfo.Id.Value);

        /// <summary>
        /// Открывает страницу целей данного знакомого
        /// </summary>
        public void OpenPagePersonGoals() => Controllers.Pages.OpenInfoPage<Goal>(_personProfile.BasicInfo.Id.Value);

        /// <summary>
        /// Открывает страницу редактирования знакомого
        /// </summary>
        public void OpenPersonEditPage() => Controllers.Pages.OpenEditPage<PersonProfile>(_personProfile);

        public void DeletePersonInfo()
        {
            Services.Data.RemovePersonProfile(_personProfile);
        }


        private void OnPersonUpdate()
        {
            DataUpdate(_personProfile.BasicInfo.Id.Value);
        }
    }
}
