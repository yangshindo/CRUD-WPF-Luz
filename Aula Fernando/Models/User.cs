using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula_Fernando
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string cpf;
        private string name;
        private string email;
        private string password;
        

        public User(){ }

        public User(string cpf, string name, string email, string password, int id)
        {
            this.cpf = cpf;
            this.name = name;
            this.email = email;
            this.password = password;
            this.id = id;
        }


        // Definindo os métodos getters e setters de cada propriedade

        public string CPF
        {
            get { return cpf; }
            set
            {
                if (cpf != value)
                {
                    cpf = value;
                    RaisePropertyChanged("CPF");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }


        public int Id
        { get { return id; }}


        public User Clone()
        {
            return (User)this.MemberwiseClone();
        }

        //-- implementação da interface INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
