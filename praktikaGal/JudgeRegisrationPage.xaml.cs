using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;
using praktikaGal.Models;


namespace praktikaGal
{
    /// <summary>
    /// Логика взаимодействия для JudgeRegisrationPage.xaml
    /// Форма для регистрации пользователей
    /// </summary>
    public partial class JudgeRegisrationPage : Page
    {
        private Model dbContext = new Model();
        User user1 = new User();
        string imagePath;
        public JudgeRegisrationPage(User user)
        {
            InitializeComponent();
            var events = from a in dbContext.Events select a.Name;
            eventsComboBox.ItemsSource = events.ToList();
            var genders = from b in dbContext.Genders select b.Name;
            genderComboBox.ItemsSource = genders.ToList();
            var roles = (from c in dbContext.Roles select c.Name).ToList();
            roles.Remove("Участник");
            roles.Remove("Организатор");
            roleComboBox.ItemsSource = roles;
            Random rnd = new Random();
            while (true)
            {
                int uniqueId = rnd.Next(0, 100000);
                if (!(from k in dbContext.Users where k.UserID == uniqueId select k).Any())
                {
                    IdNumberTextBox.Text = Convert.ToString(uniqueId);
                    break;
                }
            }

            user1 = user;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            eventsComboBox.IsEnabled = true;
        }

        private void eventsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            eventsComboBox.IsEnabled = false;
        }

        private void passwordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Text = passwordBox.Password;
            repeatTextBox.Text = repeatPasswordBox.Password;
            passwordBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Visibility = Visibility.Visible;
            repeatPasswordBox.Visibility = Visibility.Collapsed;
            repeatTextBox.Visibility = Visibility.Visible;
        }

        private void passwordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = passwordTextBox.Text;
            repeatPasswordBox.Password = repeatTextBox.Text;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordBox.Visibility = Visibility.Visible;
            repeatTextBox.Visibility = Visibility.Collapsed;
            repeatPasswordBox.Visibility = Visibility.Visible;
        }

        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "image files (*.jpeg;*.jpg;*.png)|*.jpeg; *.jpg; *.png";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == true)
            {
                profileImage.Source = new BitmapImage(new Uri(openFileDialog1.FileName));
                imagePath = System.IO.Path.GetFileName(openFileDialog1.FileName);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Organizerpage(user1));
        }

        private void createUserButton_Click(object sender, RoutedEventArgs e)
        {

            errorLabel.Content = null;
            if (string.IsNullOrWhiteSpace(passwordBox.Password) && string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                errorLabel.Content = "Введите пароль";
            }

            if (passwordCheckBox.IsChecked == true && passwordTextBox.Text != repeatTextBox.Text ||
                passwordCheckBox.IsChecked == false && passwordBox.Password != repeatPasswordBox.Password)
            {
                errorLabel.Content = "Пароли не совпадают";
            }
            if(passwordCheckBox.IsChecked == true)
            {
                if (passwordTextBox.Text.Length < 6)
                {
                    errorLabel.Content = "Пароль должен содержать минимум 6 символов";
                }
                if (!Regex.IsMatch(passwordTextBox.Text, "[A-Z]") || !Regex.IsMatch(passwordTextBox.Text, "[a-z]"))
                    errorLabel.Content = "Пароль должен содержать заглавные и строчные символы";
                if (!Regex.IsMatch(passwordTextBox.Text, "[0-9]"))
                    errorLabel.Content = "Пароль должен содержать цифру";
                if (!Regex.IsMatch(passwordTextBox.Text, @"[!@#$%^&*()_+\\={}\[\]:;<>|./?]"))
                    errorLabel.Content = "Пароль должен содержать спецсимвол";
            }
            else
            {
                if (passwordTextBox.Text.Length < 6)
                {
                    errorLabel.Content = "Пароль должен содержать минимум 6 символов";
                }
                if (!Regex.IsMatch(passwordBox.Password, "[A-Z]") || !Regex.IsMatch(passwordBox.Password, "[a-z]"))
                    errorLabel.Content = "Пароль должен содержать заглавные и строчные символы";
                if (!Regex.IsMatch(passwordBox.Password, "[0-9]"))
                    errorLabel.Content = "Пароль должен содержать цифру";
                if (!Regex.IsMatch(passwordBox.Password, @"[!@#$%^&*()_+\\={}\[\]:;<>|./?]"))
                    errorLabel.Content = "Пароль должен содержать спецсимвол";
            }
            string[] family = new string[4];
            if (!string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                family = nameTextBox.Text.Split(' ');
                if (family.Count() < 2)
                {
                    errorLabel.Content = "Введите имя и фамилию через пробел";
                }
                else if (family.Count() > 3)
                {
                    errorLabel.Content = "Введите имя, фамилию и отчество через пробел";
                }
            }
            else errorLabel.Content = "Введите имя и фамилию";

            if (genderComboBox.SelectedIndex == -1)
            {
                errorLabel.Content = "Выберите пол";
            }

            if (roleComboBox.SelectedIndex == -1)
            {
                errorLabel.Content = "Выберите poль";
            }
            if (!string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                if (!emailTextBox.Text.Contains("@"))
                {
                    errorLabel.Content = "Введите корректный email";
                }
            }
            else errorLabel.Content = "Введите email";
            if (_maskedTextBox.Text.Trim('_').Length < 17)
            {
                errorLabel.Content = "Введите номер";
            }
            if (string.IsNullOrWhiteSpace(directionTextBox.Text))
            {
                errorLabel.Content = "Введите направление";
            }
            if (eventsCheckBox.IsChecked == true && eventsComboBox.SelectedIndex == -1)
            {
                errorLabel.Content = "Введите мероприятие";
            }
            try
            {
                if (errorLabel.Content == null)
                {
                    string[] name = new string[3];
                    if (family.Length < 3)
                    {
                        name[0] = family[0];
                        name[1] = family[1];
                        name[2] = "";
                    }
                    else name = family;
                    int userRole = 0;
                    if (roleComboBox.SelectedIndex == 0)
                    {
                        userRole = 2;
                    }
                    else userRole = 4;
                    string password = "";
                    if (passwordCheckBox.IsChecked == true)
                    {
                        password = passwordTextBox.Text;
                    }
                    else password = passwordBox.Password;
                    try
                    {

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    var newUser = new User
                    {
                        Name = name[1],
                        Surname = name[0],
                        MiddleName = name[2],
                        UserID = int.Parse(IdNumberTextBox.Text),
                        Gender = genderComboBox.SelectedIndex + 1,
                        Role = userRole,
                        Email = emailTextBox.Text,
                        PhoneNumber = _maskedTextBox.Text,
                        Password = password,

                    };
                    if (imagePath != "")
                    {
                        newUser.Photo = imagePath;
                    }

                    var directions = (from a in dbContext.Directions
                                      where a.Name == directionTextBox.Text
                                      select a).ToList();
                    if (!directions.Any())
                    {
                        var newDirection = new Direction { Name = directionTextBox.Text, };
                        dbContext.Directions.Add(newDirection);
                    }
                    dbContext.Users.Add(newUser);

                    var currentDirection = (from a in dbContext.Directions where a.Name == directionTextBox.Text select a.DirectionID).FirstOrDefault();
                    var userDirection = new Users_Direction
                    {
                        IDUser = newUser.UserID,
                        IDDirection = currentDirection,
                    };
                    dbContext.Users_Direction.Add(userDirection);
                    dbContext.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен");
                    NavigationService.Navigate(new Organizerpage(user1));
                }
                else MessageBox.Show((string)errorLabel.Content);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
