using System;
using System.Collections.Generic;
using FriendNote.Core.Enums;

namespace FriendNote.Core.Converters
{
    public static class EnumConverterStrategy<T> where T : Enum
    {
        public static Dictionary<T, string> Data
        {
            get => GetConverter().Data;
        }

        public static T ToEnum(string elementName) => GetConverter().ToEnum(elementName);

        public static string ToString(T element) => GetConverter().ToString(element);


        private static EnumConverter<T> GetConverter()
        {
            if (typeof(T) == typeof(CityPartEnum)) return new CityPartConverter() as EnumConverter<T>;
            if (typeof(T) == typeof(ApartmentsEnum)) return new ApartmentsConverter() as EnumConverter<T>;
            if (typeof(T) == typeof(SkillLevelEnum)) return new SkillLevelConverter() as EnumConverter<T>;
            if (typeof(T) == typeof(InterestLevelEnum)) return new InterestLevelConverter() as EnumConverter<T>;
            if (typeof(T) == typeof(EducationDegreeEnum)) return new EducationDegreeConverter() as EnumConverter<T>;
            if (typeof(T) == typeof(InstitutionTypeEnum)) return new InstitutionTypeConverter() as EnumConverter<T>;
            if (typeof(T) == typeof(RelationshipTypeEnum)) return new RelationshipTypeConverter() as EnumConverter<T>;

            return null;
        }
    }
}