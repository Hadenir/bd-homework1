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
using AviationDb.Database;

namespace AviationDb
{
    public partial class FlightDialog : Window
    {
        public Flight Flight { get; set; }
        public DateTime? DepartureTime { get; set; } = DateTime.Now;
        public DateTime? ArrivalTime { get; set; } = DateTime.Now;

        public FlightDialog(DatabaseProxy database, Flight flight = null)
        {
            Flight = flight ?? new Flight();

            InitializeComponent();

            AircraftComboBox.ItemsSource = database.QueryAllAircraft();
            PilotComboBox.ItemsSource = database.QueryAllPilots();
            var airports = database.QueryAllAirports().ToArray();
            OriginComboBox.ItemsSource = airports;
            DestinationComboBox.ItemsSource = airports;

            if (flight is not null)
            {
                Title = @"Edit flight";
                CreateButton.Content = @"Edit flight";
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (Flight.Aircraft == null || Flight.Pilot == null || Flight.Origin == null || Flight.Destination == null
                || DepartureTime == null || ArrivalTime == null) return;

            if (DepartureTime >= ArrivalTime) return;

            Flight.DepartureTime = DepartureTime.Value;
            Flight.ArrivalTime = ArrivalTime.Value;

            DialogResult = true;
        }
    }
}