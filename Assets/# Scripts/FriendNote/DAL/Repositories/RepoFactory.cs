using System;
using FriendNote.Core.DTO;
using FriendNote.Data.Tables;
using FriendNote.Infrastructure;

namespace FriendNote.Data.Repositories
{
    public class RepoFactory : IRepositoryFactory
    {
        public IPersonRepository GetPersonRepo()
        {
            try
            {
                return new SQLitePersonRepo(Services.Config.ConnectionString);
            }
            catch (Exception ex)
            {
                Logger.LogError("Произошла ошибка при создании репозитория\n" + ex);
                return null;
            }
        }

        public IPersonRelatedInfoRepository<T> GetRelatedRepo<T>() where T : EntityBase, IPersonRelatedInfo, new()
        {
            try
            {
                var connectionString = Services.Config.ConnectionString;

                if (typeof(T) == typeof(Residence))
                {
                    return new SQLitePersonRelatedInfoRepo<Residence, ResidenceORM, AddressORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }
                if (typeof(T) == typeof(Education))
                {
                    return new SQLitePersonRelatedInfoRepo<Education, EducationORM, AddressORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }
                if (typeof(T) == typeof(WorkPosition))
                {
                    return new SQLitePersonRelatedInfoRepo<WorkPosition, WorkPositionORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }
                if (typeof(T) == typeof(Skill))
                {
                    return new SQLitePersonRelatedInfoRepo<Skill, SkillORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }
                if (typeof(T) == typeof(Interest))
                {
                    return new SQLitePersonRelatedInfoRepo<Interest, InterestORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }
                if (typeof(T) == typeof(Goal))
                {
                    return new SQLitePersonRelatedInfoRepo<Goal, GoalORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }
                if (typeof(T) == typeof(ContactInfo))
                {
                    return new SQLitePersonRelatedInfoRepo<ContactInfo, ContactInfoORM>(connectionString) as IPersonRelatedInfoRepository<T>;
                }

                throw new ArgumentException("Передан неизвестный тип данных");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }

        public IRepository<T> GetRepo<T>() where T : EntityBase, new()
        {
            try
            {
                var connectionString = Services.Config.ConnectionString;

                if (typeof(T) == typeof(PersonBasicInfo))
                {
                    return new SQLitePersonRepo(Services.Config.ConnectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(Residence))
                {
                    return new SQLitePersonRelatedInfoRepo<Residence, ResidenceORM, AddressORM>(connectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(Education))
                {
                    return new SQLitePersonRelatedInfoRepo<Education, EducationORM, AddressORM>(connectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(WorkPosition))
                {
                    return new SQLitePersonRelatedInfoRepo<WorkPosition, WorkPositionORM>(connectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(Skill))
                {
                    return new SQLitePersonRelatedInfoRepo<Skill, SkillORM>(connectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(Interest))
                {
                    return new SQLitePersonRelatedInfoRepo<Interest, InterestORM>(connectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(Goal))
                {
                    return new SQLitePersonRelatedInfoRepo<Goal, GoalORM>(connectionString) as IRepository<T>;
                }
                if (typeof(T) == typeof(ContactInfo))
                {
                    return new SQLitePersonRelatedInfoRepo<ContactInfo, ContactInfoORM>(connectionString) as IRepository<T>;
                }

                throw new ArgumentException("Передан неизвестный тип данных");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
    }
}
