using System.Collections.Generic;
using FriendNote.Domain.DTO;
using UnityEngine;

namespace FriendNote.UI
{
    public class UI_FriendList : MonoBehaviour
    {
        [SerializeField] private GameObject list;
        List<Person> _persons;

        private void Awake()
        {

        }

        private void OnDestroy()
        {

        }


        public void UpdateList()
        {
            //_persons = Services.EntityData.LoadPersonList();

            UnityHelper.DestroyAllChildren(list);
            foreach (var person in _persons)
            {
                Object prefubUI = Resources.Load("PersonNote_ListElement");
                var cardObj = Instantiate(prefubUI, list.transform) as GameObject;
                cardObj.GetComponent<UI_ListElement_PersonNote>().UpdateData(person);
            }
        }
    }
}
