using FriendNote.Domain;
using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using FriendNote.UI.Cards;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace FriendNote.UI.Pages
{
    public class InfoPage_PersonProfile : PersonInfoPage, IFieldsReseting
    {
        [Header("Required Page Fields references")]
        [SerializeField] private TMP_Text textNickname;
        [SerializeField] private UI_Card_PersonBasicInfo cardPersonBaseInfo;
        [SerializeField] private UI_Section_ContactInfo sectionContactInfo;
        [SerializeField] private TMP_Text textNotation;

        private PersonProfile _personProfile;
        [Inject] private IPagesController pagesController;
        [Inject] private IDataService dataService;


        #region [Методы] Управление жизненным циклом

        protected override void Start()
        {
            base.Start();

            pagesController.OpenedPages
                .ObserveRemove()
                .Subscribe(x => Refresh())
                .AddTo(this);
        }

        #endregion


        public override void UpdateData(int personId)
        {
            base.UpdateData(personId);

            ResetFields();
        }

        public override void Refresh()
        {
            if (dataService is null) return;

            _personProfile = dataService.LoadPersonProfile(_pageData.Value);
            if (_personProfile is null) return;

            Person basicInfo = _personProfile.BasicInfo;

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


        #region [Unity API] Методы для обратных вызовов

        public void OpenPagePersonResidences() => pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<Residence>(), _pageData.Value);
        public void OpenPagePersonEducation() => pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<Education>(), _pageData.Value);
        public void OpenPagePersonCareer() => pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<WorkPosition>(), _pageData.Value);
        public void OpenPagePersonSkills() => pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<Skill>(), _pageData.Value);
        public void OpenPagePersonInterests() => pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<Interest>(), _pageData.Value);
        public void OpenPagePersonGoals() => pagesController.OpenInfoPage(PageRegistry.GetInfoPageId<Goal>(), _pageData.Value);
        public void OpenPersonEditPage() => pagesController.OpenEditPage(PageRegistry.GetEditPageId<PersonProfile>(), _personProfile);

        public void DeletePersonInfo() =>
            dataService.RemovePersonProfile(_personProfile);

        #endregion

    }
}
