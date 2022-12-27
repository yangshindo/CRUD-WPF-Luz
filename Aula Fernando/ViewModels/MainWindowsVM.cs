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



    // Construtor
    public MainWindowsVM()
        {
            UsersList = new ObservableCollection<User>()
            {

            };
            LoadDBList();
            IniciaComandos();
        }


        public void LoadDBList()
        {

            try
            {
                using (NpgsqlConnection con = GetConnection())
                {
                    ObservableCollection<User> list = new ObservableCollection<User>();
                    string query = $@"SELECT * FROM users";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User()
                        {
                            //(0) é a id, (1) é o created_at
                            CPF = reader.GetString(2),
                            Name = reader.GetString(3),
                            Email = reader.GetString(4),
                            Password = reader.GetString(5)
                        };
                        list.Add(user);
                    }
                    UsersList = list;
                    con.Close();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void IniciaComandos()
        {

            Add = new RelayCommand((object _) =>
            {
                User newUser = new User(); //novo usuário em branco

                UserRegistration screen = new UserRegistration();  //CadastroUsuario é uma tela mas também é uma classe

                screen.DataContext = newUser; //define o context da tela para salvar os dados digitados no usuário

       
                bool? verify = screen.ShowDialog();
                if (verify.HasValue && verify.Value)
                {
                    UsersList.Add(newUser);

                    //Integração DB

                    using (NpgsqlConnection con = GetConnection())
                    {
                        string query = $@"INSERT INTO users (cpf, name, email, password) VALUES   ('{newUser.CPF}','{newUser.Name}','{newUser.Email}','{newUser.Password}')";
                        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                
                
                
                

            });


            Edit = new RelayCommand((object _) =>
            {
                if (SelectedUser != null) {
                    User usuario = SelectedUser.Clone();

                    UserRegistration telaAtualizar = new UserRegistration();

                    telaAtualizar.DataContext = usuario;

                    bool? verifica = telaAtualizar.ShowDialog();

                    if (verifica.HasValue && verifica.Value)
                    {
                        SelectedUser.Name = usuario.Name;
                        SelectedUser.Email = usuario.Email;
                        SelectedUser.Password = usuario.Password;
             


                        //Integração DB

                        using (NpgsqlConnection con = GetConnection())
                        {
                  
                            string query = $@"UPDATE users SET name='{usuario.Name}',
                                           email='{usuario.Email}',
                                           password='{usuario.Password}'
                                           WHERE cpf='{usuario.CPF}'";
                            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
                
            }
            );

            Remove = new RelayCommand((object _) => {

                User TempUser = SelectedUser;
                if (TempUser != null) { 
                UsersList.Remove(SelectedUser);


                //Integração DB

                using (NpgsqlConnection con = GetConnection())
                {
                    string query = $@"DELETE FROM users WHERE cpf='{TempUser.CPF}'";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                }

            }); 

            
        }


    }
}
