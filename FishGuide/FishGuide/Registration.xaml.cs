using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace FishGuide
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new FishGuideEntities())
            {
                var newUser = new User
                {
                    Login = LoginBox.Text,
                    Password = PassworBox.Password,
                    Nickname = NicnknameBox.Text
                };
                if (LoginBox.Text == String.Empty || PassworBox.Password == String.Empty || NicnknameBox.Text == String.Empty)
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                }
                else
                {
                    context.User.Add(newUser);
                    context.SaveChanges();
                }
            }
            this.Close();
        }
    }
}
