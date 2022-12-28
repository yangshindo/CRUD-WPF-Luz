using Aula_Fernando.DB;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Aula_Fernando.Models;

namespace Aula_Fernando
{
    public class MainWindowsVM
    {
        public ObservableCollection<User> UsersList { get; set; }

        public ICommand Add { get; set; }

        public ICommand Remove { get; set; }

        public ICommand Edit { get; set; }

        public User SelectedUser { get; set; }

        private IDatabase connection { get; set; }

        public UserRules Rules { get; set; }

        // Construtor
        public MainWindowsVM()
        {
            connection = new PostgreSQL();
            Rules = new UserRules();
            ObservableCollection<User> DBList = new ObservableCollection<User>(connection.LoadDBList());
            UsersList = DBList;
            InitCommands();
        }

  


        public void InitCommands()
        {

            Add = new RelayCommand((object _) =>
            {
                User newUser = new User();

                UserRegistration screen = new UserRegistration();

                screen.DataContext = newUser;


                bool? verify = screen.ShowDialog();
                if (verify.HasValue && verify.Value)
                {
                    try
                    {
                        if (Rules.CPFRules(newUser.CPF) && Rules.NameRules(newUser.Name) && Rules.EmailRules(newUser.Email) && Rules.PasswordRules(newUser.Password))
                        {
                            UsersList.Add(newUser);

                            connection.DB_AddUser(newUser);
                        }
                        
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Erro ao incluir um usuário. --> {e.Message}");
                    }
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
                        try
                        {
                            SelectedUser.Name = usuario.Name;
                            SelectedUser.Email = usuario.Email;
                            SelectedUser.Password = usuario.Password;

                            connection.DB_EditUser(SelectedUser);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show($"Erro ao editar um usuário. --> {e.Message}");
                        }
                    }
                }

            }
            );

            Remove = new RelayCommand((object _) =>
            {

                User TempUser = SelectedUser;
                if (TempUser != null)
                {
                    try
                    {
                        UsersList.Remove(SelectedUser);

                        connection.DB_RemoveUser(TempUser);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Erro ao excluir um usuário. --> {e.Message}");
                    }

                }

            });


        }


    }
}
