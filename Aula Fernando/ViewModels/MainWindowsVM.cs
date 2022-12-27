using Aula_Fernando.DB;
using Npgsql;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Aula_Fernando
{
    public class MainWindowsVM
    {
        public ObservableCollection<User> UsersList { get; set; } //variável para ser linkada ao objeto

        public ICommand Add { get; set; } //comando adicionar (ICommand interface, RelayCommand class)

        public ICommand Remove { get; set; }

        public ICommand Edit { get; set; }

        public User SelectedUser { get; set; }

        private IDatabase connection { get; set; }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=WPFDB");
        }



        // Construtor
        public MainWindowsVM()
        {
            connection = new PostgreSQL();
            UsersList = connection.LoadDBList();
            IniciaComandos();
        }


        public void IniciaComandos()
        {

            Add = new RelayCommand((object _) =>
            {
                User newUser = new User(); 

                UserRegistration screen = new UserRegistration(); 

                screen.DataContext = newUser; 


                bool? verify = screen.ShowDialog();
                if (verify.HasValue && verify.Value)
                {
                    UsersList.Add(newUser);

                    connection.DB_AddUser(newUser);
                }

            });


            Edit = new RelayCommand((object _) =>
            {
                if (SelectedUser != null)
                {
                    User usuario = SelectedUser.Clone();

                    UserRegistration telaAtualizar = new UserRegistration();

                    telaAtualizar.DataContext = usuario;


                    bool? verifica = telaAtualizar.ShowDialog();


                    if (verifica.HasValue && verifica.Value)
                    {
                        SelectedUser.Name = usuario.Name;
                        SelectedUser.Email = usuario.Email;
                        SelectedUser.Password = usuario.Password;

                        connection.DB_EditUser(SelectedUser);
                    }
                }

            }
            );

            Remove = new RelayCommand((object _) =>
            {

                User TempUser = SelectedUser;
                if (TempUser != null)
                {
                    UsersList.Remove(SelectedUser);

                    connection.DB_RemoveUser(TempUser);


                }

            });


        }


    }
}
