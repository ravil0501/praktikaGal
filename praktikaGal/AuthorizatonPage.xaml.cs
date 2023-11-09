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
    /// Логика взаимодействия для AuthorizatonPage.xaml
    /// </summary>
    public partial class AuthorizatonPage : Page
    {
        public AuthorizatonPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dbContext = new Model();
                var polzovatel = (from a in dbContext.Users
                                  where a.Email == LoginTextBox.Text
                                  select a).Single();
                if (polzovatel != null && polzovatel.Password == PasswordTextBox.Password)
                {
                    switch (polzovatel.Role)
                    {
                        case 1:
                            MessageBox.Show("Окно участника");
                            break;
                        case 2:
                            MessageBox.Show("Окно модератора");
                            break;
                        case 3:
                            NavigationService.Navigate(new Organizerpage(polzovatel));
                            break;
                        case 4:
                            MessageBox.Show("Окно жюри");
                            break;
                    }
                }
                else MessageBox.Show("Нету пользователя либо пароль неверный");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Events());
        }
    }
}
