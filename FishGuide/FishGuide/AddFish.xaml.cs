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
using System.Windows.Shapes;

namespace FishGuide
{
    /// <summary>
    /// Логика взаимодействия для AddFish.xaml
    /// </summary>
    public partial class AddFish : Window
    {
        public AddFish()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                string tackle = TackleTextBox.Text;
                string habitat = HabitatTextBox.Text;
                string season = SeasonTextBox.Text;
                string fishInfo = FishInfoTextBox.Text;

                if (string.IsNullOrEmpty(name) || tackle == null || string.IsNullOrEmpty(habitat) || string.IsNullOrEmpty(season) || string.IsNullOrEmpty(fishInfo))
                {
                    MessageBox.Show("Заполните все поля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создание нового объекта Fish
                Recipe newFish = new Recipe
                {
                    Name = name,
                    Ingredients = tackle,
                    Difficulty = habitat,
                    Speed = season,
                    About = fishInfo
                };

                // Сохранение в базе данных
                using (var dbContext = new RecipeBookEntities()) // Замени на свой контекст базы данных
                {
                    dbContext.Recipe.Add(newFish);
                    dbContext.SaveChanges();
                }

                // Закрытие диалогового окна
                DialogResult = true;
            }
            catch (Exception ex) { MessageBox.Show("Ошибка не введена снасть."); }
            }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
