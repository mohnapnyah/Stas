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

namespace FishGuide
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User currentUser;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationWindow = new Registration();
            registrationWindow.ShowDialog();
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != string.Empty && Password.Password != string.Empty)
            {
                User foundUser = FishDB.FindUserByLogin(Login.Text);
                if(foundUser.Password == Password.Password)
                {
                    currentUser = foundUser;
                    MessageBox.Show("Успешная авторизация");
                    Login.Text = string.Empty;
                    Password.Password = string.Empty;
                    EncyPage encyPage = new EncyPage(currentUser);
                    encyPage.Show();
                    this.Close();
                }
                else
                { 
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                MessageBox.Show("Неправильно введен логин или пользователь не существует");
            }
        }
    }
}
