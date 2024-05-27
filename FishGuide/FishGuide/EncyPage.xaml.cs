using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace FishGuide
{
    /// <summary>
    /// Логика взаимодействия для EncyPage.xaml
    /// </summary>
    public partial class EncyPage : Window
    {
        public User user;
        public EncyPage(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
            LoadFishList();
            if(currentUser.User_Id == 1)
            {
                AddFish.Visibility = Visibility.Visible;
                DeleteRecipe.Visibility = Visibility.Visible;
            }
        }
        private void LoadFishList()
        {
            using (var context = new RecipeBookEntities())
            {
                // Загрузка списка рыб из базы данных
                var fishList = context.Word.ToList();
                // Привязка списка рыб к ListView
                FishListView.ItemsSource = fishList;
            }
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            // Сначало необходимо уставновить NuGet: QRCoder
            // Ссылка на XL таблицу
            string soucer_xl = "russianfood.com"; //внутри кавычек надо вставить ссылку
            // Создание переменной библиотеки QRCoder
            QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
            // Присваеваем значиения
            QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
            // переводим в Qr
            QRCoder.QRCode code = new QRCoder.QRCode(data);
            Bitmap bitmap = code.GetGraphic(100);
            /// Создание картинки
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                imageQr.Source = bitmapimage;
            }
        }

        private void FishListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FishListView.SelectedItem != null)
            {
                var selectedFish = (Word)FishListView.SelectedItem;
                FishName.Text = selectedFish.Name;
                ShowFishInfo(selectedFish);
            }
        }
        private void ShowFishInfo(Word fish)
        {
            // Отображение информации о рыбе в TextBlock
            FishInfoTextBlock.Text = $"\nСложность слова: {fish.Difficulty} \nСкорость освоения: {fish.Speed} \nПеревод: {fish.About}";
        }

     

        private void AddFish_Click(object sender, RoutedEventArgs e)
        {
            AddFish addfish = new AddFish();
            addfish.ShowDialog();
        }

        private void TackleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FishListView.SelectedItem != null)
            {
                var selectedFish = (Word)FishListView.SelectedItem;
                FishName.Text = selectedFish.Name;
                ShowFishInfo(selectedFish);
            }
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Получение выбранного элемента
            Word selectedFish = (Word)FishListView.SelectedItem;

            if (selectedFish != null)
            {
                // Удаление из базы данных
                using (var dbContext = new RecipeBookEntities()) // Замени на свой контекст базы данных
                {
                    var existingFish = dbContext.Word.Find(selectedFish.Word_id);

                    if (existingFish != null)
                    {
                        dbContext.Word.Remove(existingFish);
                        dbContext.SaveChanges();
                    }
                }
                FishListView.SelectedItems.Remove(selectedFish);
                UpdateLayout();
            }
            else
            {
                MessageBox.Show("Выберите рецепт для удаления.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(FishListView.ItemsSource);
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
                view.Refresh();
            }
        }
    }
}
