using FriendNote.Data.Repositories;
using FriendNote.Domain.DTO;
using FriendNote.Infrastructure;
using FriendNote.UI.Pages;
using Zenject;

namespace FriendNote.UI.Cards
{
    public abstract class UI_PersonCard<T> : UI_Card<T>
        where T : EntityBase, IPersonRelatedInfo, new()
    {
        protected string _emptyField = "???";

        protected IPersonRelatedInfoRepository<T> repo;
        [Inject] private IRepositoryFactory repoFactory;
        [Inject] private IPagesController _pagesController;

        public int PersonId { get; set; }


        private void Start()
        {
            repo = repoFactory?.GetRelatedRepo<T>();
        }

        /// <summary>
        /// Открывает страницу редактирования карточки
        /// </summary>
        public virtual void OpenEditPage()
        {
            var page = _pagesController.OpenEditPage<T>(PageRegistry.GetEditPageId<T>(), _data) as EntityEditPage<T>;
            page.PersonId = this.PersonId;
        }

        /// <summary>
        /// Удаляет карточку
        /// </summary>
        public virtual void Remove()
        {
            if (repo.Remove(_data))
            {
                DestroyImmediate(this.gameObject); // TODO: сделать удаление плавным и с анимацией
            }
        }
    }
}
