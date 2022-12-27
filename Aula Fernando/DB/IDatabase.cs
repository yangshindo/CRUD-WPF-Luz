using System.Collections.ObjectModel;

namespace Aula_Fernando.DB
{
    public interface IDatabase
    {
        ObservableCollection<User> LoadDBList();
        void DB_AddUser(User newUser);
        void DB_EditUser(User user);
        void DB_RemoveUser(User user);
    };
}
