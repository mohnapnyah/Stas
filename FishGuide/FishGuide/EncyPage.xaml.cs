using System;
using System.Collections.Generic;
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
            LoadTackleList();
            if(currentUser.User_id == 4)
            {
                AddFish.Visibility= Visibility.Visible;
            }
        }
        private void LoadFishList()
        {
            using (var context = new FishGuideEntities())
            {
                // Загрузка списка рыб из базы данных
                var fishList = context.Fish.ToList();

                // Привязка списка рыб к ListView
                FishListView.ItemsSource = fishList;
            }
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            // Сначало необходимо уставновить NuGet: QRCoder
            // Ссылка на XL таблицу
            string soucer_xl = "prikormka.com"; //внутри кавычек надо вставить ссылку
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
                var selectedFish = (Fish)FishListView.SelectedItem;
                Tackle tackle = GetTackleById((int)selectedFish.Tackle); // Метод, который выполняет запрос к базе данных

                FishName.Text = selectedFish.Name;
                ShowFishInfo(selectedFish, tackle);
            }
        }
        private void ShowFishInfo(Fish fish, Tackle tackle)
        {
            // Отображение информации о рыбе в TextBlock
            FishInfoTextBlock.Text = $"Эффективная снасть: {tackle.Name}\nГде ловить: {fish.Area}\nСезон ловли: {fish.Season}\nОбщая информация: {fish.About}";
        }

        private Tackle GetTackleById(int tackleId)
        {
            using (var context = new FishGuideEntities())
            {
                Tackle tackle = context.Tackle.FirstOrDefault(t => t.Tackle_id == tackleId);
                // Здесь просто возвращаю заглушку, замени на реальную реализацию
                return tackle;
            }
        }
        private void LoadTackleList()
        {
            using (var context = new FishGuideEntities())
            {
                
                var tackleList = context.Tackle.ToList();
                TackleListView.ItemsSource = tackleList;
            }
        }

        private void TackleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TackleListView.SelectedItem != null)
            {
                Tackle selectedTackle = (Tackle)TackleListView.SelectedItem;

                TackleInfoTextBlock.Text = $"Название снасти: {selectedTackle.Name}\nМетод рыбалки: {selectedTackle.FishingType}";
            }
            else
            {
                TackleInfoTextBlock.Text = string.Empty;
            }
        }

        private void AddFish_Click(object sender, RoutedEventArgs e)
        {
            AddFish addfish = new AddFish();
            addfish.ShowDialog();
        }
    }

}
