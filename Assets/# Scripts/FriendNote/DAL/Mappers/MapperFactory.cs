using System;
using System.Collections.Generic;
using FriendNote.Core.DTO;
using FriendNote.Data.Tables;

namespace FriendNote.Data.Mappers
{
    public static class MapperFactory
    {
        private static readonly Dictionary<string, object> _mappers = new Dictionary<string, object>
        {
            { $"{typeof(WorkPositionORM).FullName}_{typeof(WorkPosition).FullName}", new WorkPositionMapper() },
            { $"{typeof(ContactInfoORM).FullName}_{typeof(ContactInfo).FullName}", new ContactInfoMapper() },
            { $"{typeof(EducationORM).FullName}_{typeof(AddressORM).FullName}_{typeof(Education).FullName}", new EducationMapper() },
            { $"{typeof(ResidenceORM).FullName}_{typeof(AddressORM).FullName}_{typeof(Residence).FullName}", new ResidenceMapper() },
            { $"{typeof(InterestORM).FullName}_{typeof(Interest).FullName}", new InterestMapper() },
            { $"{typeof(AddressORM).FullName}_{typeof(Address).FullName}", new AddressMapper() },
            { $"{typeof(PersonORM).FullName}_{typeof(AddressORM).FullName}_{typeof(PersonBasicInfo).FullName}", new PersonBasicInfoMapper() },
            { $"{typeof(SkillORM).FullName}_{typeof(Skill).FullName}", new SkillMapper() },
            { $"{typeof(GoalORM).FullName}_{typeof(Goal).FullName}", new GoalMapper() }
        };


        public static IMapper<TSource, TDestination> GetMapper<TSource, TDestination>()
        {
            var key = $"{typeof(TSource).FullName}_{typeof(TDestination).FullName}";
            if (_mappers.ContainsKey(key))
            {
                return (IMapper<TSource, TDestination>)_mappers[key];
            }

            throw new ArgumentException($"Mapper for {key} not found.");
        }

        public static IMapper<TSource1, TSource2, TDestination> GetMapper<TSource1, TSource2, TDestination>()
        {
            var key = $"{typeof(TSource1).FullName}_{typeof(TSource2).FullName}_{typeof(TDestination).FullName}";
            if (_mappers.ContainsKey(key))
            {
                return (IMapper<TSource1, TSource2, TDestination>)_mappers[key];
            }

            throw new ArgumentException($"Mapper for {key} not found.");
        }
    }
}
