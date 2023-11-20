using Game.Models;
using Game.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public IPasswordContainer PasswordContainer { get; }
        public LoginView()
        {
            InitializeComponent();

            PasswordContainer = new PasswordContainer();
        }

        private void Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordContainer.Password = Pass.SecurePassword;
        }
    }
}
