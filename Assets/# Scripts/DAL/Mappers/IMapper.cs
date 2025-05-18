namespace FriendNote.Data.Mappers
{
    public interface IMapper<TSource, TDestination>
    {
        /// <summary>
        /// Преобразование ORM-объекта в структуру данных (Напирмер: DTO)
        /// </summary>
        TDestination ToDTO(TSource source);

        /// <summary>
        /// Преобразование структуры данных (Например: DTO) в ORM-объект
        /// </summary>
        TSource ToORM(TDestination destination);
    }

    public interface IMapper<TSource1, TSource2, TDestination>
    {
        /// <summary>
        /// Преобразование пары ORM-объектов в структуру данных (Напирмер: DTO)
        /// </summary>
        TDestination ToDTO(TSource1 source1, TSource2 source2);

        /// <summary>
        /// Преобразование структуры данных (Например: DTO) в ORM-объект, определенный как TSource1
        /// </summary>
        (TSource1, TSource2) ToORM(TDestination destination);
    }
}
