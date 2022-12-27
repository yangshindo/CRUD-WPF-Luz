using System.Windows;

namespace Aula_Fernando
{
    /// <summary>
    /// Lógica interna para CadastroUsuario.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        public void ButtonSave(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
