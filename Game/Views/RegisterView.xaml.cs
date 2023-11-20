using Game.Models;
using Game.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public IPasswordContainer PasswordContainer { get; } 
        public IPasswordContainer PasswordRepeateContainer { get; } 
        public (IPasswordContainer password, IPasswordContainer passwordRepeate) B 
        { 
            get
            {
                return (PasswordContainer, PasswordRepeateContainer);
            } 
        }
        public RegisterView()
        {
            InitializeComponent();

            PasswordContainer = new PasswordContainer();
            PasswordRepeateContainer = new PasswordContainer();
        }

        private void Pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordContainer.Password = Pass.SecurePassword;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordRepeateContainer.Password = RepPass.SecurePassword;
        }
    }
}
