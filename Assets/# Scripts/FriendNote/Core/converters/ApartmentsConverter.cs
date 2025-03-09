using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    /// <summary>
    /// Преобразователь данных для ApartmentsEnum
    /// </summary>
    public class ApartmentsConverter : EnumConverter<ApartmentsEnum>
    {
        public ApartmentsConverter()
        {
            Data = new()
            {
                { ApartmentsEnum.House, "Дом" },
                { ApartmentsEnum.Apartment, "Квартира" },
                { ApartmentsEnum.RentedHouse, "Арендованный дом" },
                { ApartmentsEnum.RentedApartment, "Арендованная квартира" },
                { ApartmentsEnum.MortgageHouse, "Дом в ипотеке" },
                { ApartmentsEnum.MortgageApartment, "Квартира в ипотеке" },
                { ApartmentsEnum.Homeless, "Бездомный" }
            };
        }
    }
}
