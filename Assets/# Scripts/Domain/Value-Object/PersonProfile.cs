using System.Collections.Generic;

namespace FriendNote.Domain.DTO
{
    /// <summary>
    /// Информация о человеке
    /// </summary>
    public class PersonProfile
    {
        public Person BasicInfo { get; set; }
        public List<Residence> Residences { get; set; } = new();
        public List<Education> Education { get; set; } = new();
        public List<WorkPosition> Career { get; set; } = new();
        public List<Skill> Skills { get; set; } = new();
        public List<Interest> Interests { get; set; } = new();
        public List<Goal> Goals { get; set; } = new();
        public PersonContacts ContactInfo { get; set; }
    }
}
