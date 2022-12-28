using System.Collections.Generic;

namespace Aula_Fernando.DB
{
    public interface IDatabase
    {
        List<User> LoadDBList();
        void DB_AddUser(User newUser);
        void DB_EditUser(User user);
        void DB_RemoveUser(User user);
    };
}
