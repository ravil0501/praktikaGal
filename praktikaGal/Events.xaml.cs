using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Логика взаимодействия для Events.xaml
    /// </summary>
    public partial class Events : Page
    {
        List<EventsClass> eventList;
        public Events()
        {
            InitializeComponent();
            var dbContext = new Model();
            eventList = (from a in dbContext.Events
                             join b in dbContext.Event_EventType_Direction on a.EventID equals b.IDEvent
                             join c in dbContext.Directions on b.IDDirection equals c.DirectionID
                             select new EventsClass{ Name = a.Name, EventImage = a.eventmage, Date = a.BeginningDate, Direction = c.Name }).ToList();
            eventsListView.ItemsSource = eventList;
            var directions = (from a in dbContext.Directions select a.Name).ToList();
            directions.Add("Сброс фильтров");
            directionFilterComboBox.ItemsSource = directions;
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("AuthorizatonPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(eventsListView.ItemsSource);

            if (directionFilterComboBox.SelectedItem != null || dateFilterDatePicker.SelectedDate != null)
            {
                if (string.IsNullOrEmpty((string)directionFilterComboBox.SelectedValue))
                {
                    directionFilterComboBox.SelectedValue = "";
                }
                if (!string.Equals((string)directionFilterComboBox.SelectedValue,  "Сброс фильтров"))
                {
                    view.Filter = item =>
                    {
                        EventsClass eventsItem = (EventsClass)item;
                        bool directionPass = true;
                        bool datePass = true;

                        if (directionFilterComboBox.SelectedItem != null)
                        {
                            string selectedDirectionId = (string)directionFilterComboBox.SelectedValue;
                            directionPass = eventsItem.Direction == selectedDirectionId;
                        }

                        if (dateFilterDatePicker.SelectedDate != null)
                        {
                            DateTime selectedDate = dateFilterDatePicker.SelectedDate.Value;
                            datePass = eventsItem.Date == selectedDate.Date;
                        }

                        return directionPass && datePass;
                    };
                }
                else
                {
                    if (view != null)
                    {
                        view.Filter = null;
                        directionFilterComboBox.SelectedIndex = -1;
                        dateFilterDatePicker.SelectedDate = null;
                    }
                }
                
            }
            else
            {
                view.Filter = null;
            }
        }

        private void disableButton_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
