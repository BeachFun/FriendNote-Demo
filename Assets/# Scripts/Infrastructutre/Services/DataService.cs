using System;
using System.Collections.Generic;
using System.Linq;
using FriendNote.Configuration;
using FriendNote.Core;
using FriendNote.Core.Enums;
using FriendNote.Data.Repositories;
using FriendNote.Domain;
using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using UniRx;
using Zenject;

namespace FriendNote.Data
{
    public class DataService : ServiceBase, IDataService, IDisposable
    {
        private IPersonRepository _repoPerson;
        private IPersonRelatedInfoRepository<Residence> _repoResidences;
        private IPersonRelatedInfoRepository<Education> _repoEducation;
        private IPersonRelatedInfoRepository<WorkPosition> _repoCareer;
        private IPersonRelatedInfoRepository<Skill> _repoSkills;
        private IPersonRelatedInfoRepository<Interest> _repoInterests;
        private IPersonRelatedInfoRepository<Goal> _repoGoals;
        private IPersonRelatedInfoRepository<ContactInfo> _repoContacts;

        [Inject] private IRepositoryFactory repoFactory;
        [Inject] private ConfigurationService configService;


        #region [Методы] Запуск и инициализация сервиса

        public override void Startup()
        {
            Status.Value = ServiceStatus.Initializing;

            if (configService.Status.Value != ServiceStatus.Started)
            {
                configService.Status
                    .Where(x => x == ServiceStatus.Started)
                    .Take(1)
                    .Subscribe(status => OnConfigServiceStarted());
            }
            else
            {
                OnConfigServiceStarted();
            }
        }

        private void OnConfigServiceStarted()
        {
            _repoPerson = repoFactory.GetPersonRepo();
            _repoResidences = repoFactory.GetRelatedRepo<Residence>();
            _repoEducation = repoFactory.GetRelatedRepo<Education>();
            _repoCareer = repoFactory.GetRelatedRepo<WorkPosition>();
            _repoSkills = repoFactory.GetRelatedRepo<Skill>();
            _repoInterests = repoFactory.GetRelatedRepo<Interest>();
            _repoGoals = repoFactory.GetRelatedRepo<Goal>();
            _repoContacts = repoFactory.GetRelatedRepo<ContactInfo>();

            Status.Value = ServiceStatus.Started;
        }

        public void Dispose()
        {
            if (Status.Value != ServiceStatus.Started) return;

            Status.Value = ServiceStatus.Shutdown;
        }

        #endregion


        /// <summary>
        /// Получение полной информации, связанной с человеком
        /// </summary>
        public PersonProfile LoadPersonProfile(int personId)
        {
            if (personId < 0) return null;

            var res = new PersonProfile();
            {
                res.BasicInfo = _repoPerson.LoadById(personId);
                res.Residences = _repoResidences.LoadAllByPersonId(personId)?.ToList();
                res.Education = _repoEducation.LoadAllByPersonId(personId)?.ToList();
                res.Career = _repoCareer.LoadAllByPersonId(personId)?.ToList();
                res.Skills = _repoSkills.LoadAllByPersonId(personId)?.ToList();
                res.Interests = _repoInterests.LoadAllByPersonId(personId)?.ToList();
                res.Goals = _repoGoals.LoadAllByPersonId(personId)?.ToList();
                res.ContactInfo = LoadPersonContacts(personId);
            }

            return res;
        }

        /// <summary>
        /// Сохранение полной информации, связанной с человеком
        /// </summary>
        public bool SavePersonProfile(PersonProfile personProfile)
        {
            if (personProfile is null) return false;

            // Сохранение основной информации
            int personId = _repoPerson.Save(personProfile.BasicInfo);

            // Указание ссылки на запись о человеке, с которым связаны данные, если добавлялся новый человек
            personProfile.Residences.ForEach(x => x.PersonId = personId);
            personProfile.Education.ForEach(x => x.PersonId = personId);
            personProfile.Career.ForEach(x => x.PersonId = personId);
            personProfile.Skills.ForEach(x => x.PersonId = personId);
            personProfile.Interests.ForEach(x => x.PersonId = personId);
            personProfile.Goals.ForEach(x => x.PersonId = personId);
            List<ContactInfo> contactList = personProfile.ContactInfo.ToList();
            contactList.ForEach(x => x.PersonId = personId);

            // Сохранение дополнительной информации
            foreach (var item in personProfile.Residences) _repoResidences.Save(item); // места проживания
            foreach (var item in personProfile.Education) _repoEducation.Save(item); // образование
            foreach (var item in personProfile.Career) _repoCareer.Save(item); // карьера
            foreach (var item in personProfile.Skills) _repoSkills.Save(item); // навыки
            foreach (var item in personProfile.Interests) _repoInterests.Save(item); // интересы
            foreach (var item in personProfile.Goals) _repoGoals.Save(item); // цели
            foreach (var item in contactList) _repoContacts.Save(item); // контактная информация

            // Записи не сохраняются с ID = 0, даже первые
            return personId > 0;
        }

        /// <summary>
        /// Удаление всей информации, связанной с человеком
        /// </summary>
        /// <returns>true/false в зависимости от успешности операции удаления</returns>
        public bool RemovePersonProfile(PersonProfile personProfile)
        {
            if (personProfile is null) return false;

            // Проверка на существование человека
            int? personId = personProfile.BasicInfo.Id;
            if (!personProfile.BasicInfo.Id.HasValue || _repoPerson.LoadById(personId.Value) is null)
            {
                Logger.LogWarning($"No Person record found for ID {personProfile.BasicInfo.Id} to removing");
                return false;
            }

            // Удаление дополнительной информации
            foreach (var item in personProfile.Residences) _repoResidences.Remove(item); // места проживания
            foreach (var item in personProfile.Education) _repoEducation.Remove(item); // образование
            foreach (var item in personProfile.Career) _repoCareer.Remove(item); // карьера
            foreach (var item in personProfile.Skills) _repoSkills.Remove(item); // навыки
            foreach (var item in personProfile.Interests) _repoInterests.Remove(item); // интересы
            foreach (var item in personProfile.Goals) _repoGoals.Remove(item); // цели
            foreach (var item in personProfile.ContactInfo.ToList()) _repoContacts.Remove(item); // контактная информация
            // Удаление основной информации
            return _repoPerson.Remove(personProfile.BasicInfo.Id.Value);
        }

        /// <summary>
        /// Получение контактов связанных с человеком
        /// </summary>
        public PersonContacts LoadPersonContacts(int personId)
        {
            IEnumerable<ContactInfo> contacts = _repoContacts.LoadAllByPersonId(personId);

            var personContacts = new PersonContacts(personId);
            foreach (ContactInfo contact in contacts)
            {
                switch (contact.ContactType)
                {
                    case ContactTypeEnum.Phone:
                        personContacts.PhoneNumbers.Add(contact);
                        break;
                    case ContactTypeEnum.SocialNetwork:
                        personContacts.SocialMedia.Add(contact);
                        break;
                    case ContactTypeEnum.Email:
                        personContacts.Emails.Add(contact);
                        break;

                    default:
                        personContacts.Others.Add(contact);
                        break;
                }
            }

            return personContacts;
        }
    }
}
