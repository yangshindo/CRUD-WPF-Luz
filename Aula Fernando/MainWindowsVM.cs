using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Npgsql;

namespace Aula_Fernando
{
    public class MainWindowsVM
    {
        public ObservableCollection<User> UsersList { get; set; } //variável para ser linkada ao objeto

        public ICommand Add { get; set; } //comando adicionar (ICommand interface, RelayCommand class)

        public ICommand Remove { get; set; }

        public ICommand Edit { get; set; }

        public User SelectedUser { get; set; }

        private static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=WPFDB");
        }

        private static void InsertRecord()
        {
            using(NpgsqlConnection con=GetConnection())
            {
                string query = @"insert into public.Users";
            }
        }

        // Construtor
        public MainWindowsVM()
        {
            UsersList = new ObservableCollection<User>()
            {

            };
            IniciaComandos();
        }


        public void IniciaComandos()
        {

            Add = new RelayCommand((object _) =>
            {
                User userCadastro = new User(); //novo usuário em branco

                CadastroUsuario tela = new CadastroUsuario();  //CadastroUsuario é uma tela mas também é uma classe

                tela.DataContext = userCadastro; //define o context da tela para salvar os dados digitados no usuário

       
                bool? verifica = tela.ShowDialog();
                if (verifica.HasValue && verifica.Value)
                {
                    UsersList.Add(userCadastro);
                }

                
                using (NpgsqlConnection con = GetConnection())
                {
                    string query = @"insert into public.Users(Name,Email,Password)values("+userCadastro.Name+","+userCadastro.Email+","+userCadastro.Password+")";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n == 1)
                    {
                        Console.WriteLine("Record inserted");
                    }

                }
                
                

            });


            Edit = new RelayCommand((object _) =>
            {
                if (SelectedUser != null) {
                    User usuario = SelectedUser.Clone();

                    CadastroUsuario telaAtualizar = new CadastroUsuario();

                    telaAtualizar.DataContext = usuario;

                    bool? verifica = telaAtualizar.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        SelectedUser.Name = usuario.Name;
                        SelectedUser.Email = usuario.Email;
                        SelectedUser.Password = usuario.Password;
                    }
                }
                
            }
            );

            Remove = new RelayCommand((object _) => { UsersList.Remove(SelectedUser); }); // utiliza função remove no usuário selecionado (ItemSelected no ListView)


        }


    }
}
