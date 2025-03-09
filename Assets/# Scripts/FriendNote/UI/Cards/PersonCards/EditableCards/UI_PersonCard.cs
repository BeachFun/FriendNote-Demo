using FriendNote.Core.DTO;

namespace FriendNote.UI.Cards
{
    public abstract class UI_PersonCard<T> : UI_Card<T>
        where T : EntityBase, IPersonRelatedInfo, new()
    {
        protected string _emptyField = "???";


        /// <summary>
        /// Открывает страницу редактирования карточки
        /// </summary>
        public virtual void OpenEditPage()
        {
            Controllers.Pages.OpenRelatedInfoEditPage<T>(_data);
        }

        /// <summary>
        /// Удаляет карточку
        /// </summary>
        public virtual void Remove()
        {
            if (Services.EntityData.RemoveEntity<T>(_data))
            {
                DestroyImmediate(this.gameObject); // TODO: сделать удаление плавным и с анимацией
            }
        }
    }
}
