using SQLite;

namespace FriendNote.Data.Repositories
{
    public abstract class SQLiteRepoBase
    {
        protected const int EMPTY_ID = -1;
        protected const int DEFAULT_ID = 0;
        protected readonly SQLiteConnectionString _connectionString;
        protected SQLiteConnection _db;

        protected SQLiteRepoBase(SQLiteConnectionString connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
