using System.Windows;

namespace Aula_Fernando
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowsVM(); //ligando com o view model adequado
        }
    }
}
