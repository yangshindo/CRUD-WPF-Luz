using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Aula_Fernando.Models
{
    public class UserRules
    {

        public UserRules() { }

        //FUNÇÕES AUXILIARES:

        private static bool HasSpecialChars(string stringData)
        {
            return stringData.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private static bool containsInt(string stringData)
        {

            return stringData.Any(char.IsDigit);
        }

        private static bool isIntString(string stringData) {
            return stringData.All(char.IsDigit);
                }

        private static bool IsValidEmail(string stringData)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(stringData);
        }

        public static string RemoveWhitespace(string stringData)
        {
            return new string(stringData.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }


        //Regras para CPF
        public bool CPFRules(string stringData)
        {
            if (stringData.Length == 11 && isIntString(stringData))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Favor inserir um CPF válido. 11 dígitos, apenas números.");
                return false;
            }
        }


        //Regras para nome
        public bool NameRules(string stringData)
        {
            stringData = RemoveWhitespace(stringData);
            if (stringData.Length < 5 || HasSpecialChars(stringData) || containsInt(stringData))
            {
                MessageBox.Show("Favor inserir um nome válido.");
                return false;
            } else
            {
                return true;
            }
        }

        //Regras para e-mail
        public bool EmailRules(string stringData)
        {
            if (IsValidEmail(stringData) && stringData.Length < 50) {
                return true;
            } else
            {
                MessageBox.Show("Favor inserir um e-mail válido.");
                return false;
            }
        }
        
        //Regras para senha
        public bool PasswordRules(string stringData)
        {
            if (stringData.Length < 8 || !HasSpecialChars(stringData) || !containsInt(stringData))
            {
                MessageBox.Show("A senha precisa conter no mínimo 8 caracteres, com ao menos um caracter especial e um número.");
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
