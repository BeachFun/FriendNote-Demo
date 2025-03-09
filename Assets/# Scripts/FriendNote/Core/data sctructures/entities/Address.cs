namespace FriendNote.Core.DTO
{

    public class Address : EntityBase
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public string PostalCode { get; set; }


        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Country) &&
                string.IsNullOrEmpty(State) &&
                string.IsNullOrEmpty(City) &&
                string.IsNullOrEmpty(Street) &&
                string.IsNullOrEmpty(House) &&
                string.IsNullOrEmpty(Apartment) &&
                string.IsNullOrEmpty(PostalCode);
        }

        public string GetAddressString()
        {
            string res = "";

            if (!string.IsNullOrEmpty(Country)) res += $"{Country}";
            if (!string.IsNullOrEmpty(State)) res += $", {State}";
            if (!string.IsNullOrEmpty(City)) res += $", {City}";
            if (!string.IsNullOrEmpty(Street)) res += $", {Street}";
            if (!string.IsNullOrEmpty(House)) res += $", {House}";
            if (!string.IsNullOrEmpty(Apartment)) res += $", {Apartment}";

            return res;
        }
    }
}
