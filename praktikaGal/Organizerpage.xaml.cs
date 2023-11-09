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
using praktikaGal.Models;

namespace praktikaGal
{
    /// <summary>
    /// Логика взаимодействия для Organizerpage.xaml
    /// </summary>
    public partial class Organizerpage : Page
    {
        User user1;
        public Organizerpage(User user)
        {
            InitializeComponent();
            profileImage.Source = new BitmapImage(new Uri("/Resources/"+user.Photo, UriKind.Relative));
            DateTime currentTime = DateTime.Now;
            if (currentTime.TimeOfDay >= TimeSpan.Parse("09:00") && currentTime.TimeOfDay <= TimeSpan.Parse("11:00"))
            {
                helloLabel.Content = "Доброе утро!";
            }
            else if (currentTime.TimeOfDay > TimeSpan.Parse("11:00") && currentTime.TimeOfDay <= TimeSpan.Parse("18:00"))
            {
                helloLabel.Content = "Добрый день!";
            }
            else
            {
                helloLabel.Content = "Добрый вечер!";
            }
            if (user.Role == 1)
            {
                helloLabel.Content += " Mr " + user.Name;
            }
            else helloLabel.Content += " Mrs " + user.Name;
            user1 = user;
        }

        private void judgesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new JudgeRegisrationPage(user1));
        }

        private void parcipantsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Участники");
        }

        private void eventsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Мероприятия");
        }

        private void profileButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Профиль");
        }
    }
}
