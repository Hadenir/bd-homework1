using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
using AviationDb.Database;
using Microsoft.Data.SqlClient;

namespace AviationDb
{
    public partial class FlightsPage
    {
        private readonly DatabaseProxy _database;

        public ObservableCollection<Flight> Flights { get; set; } = new();
        
        public FlightsPage(DatabaseProxy database)
        {
            _database = database;
            
            InitializeComponent();

            RefreshFlights();
        }

        private void RefreshFlights()
        {
            Flights.Clear();
            Flights.AddRange(_database.QueryAllFlights());
        }

        private void CreateNewFlight()
        {
            var dialog = new FlightDialog(_database) { Owner = Window.GetWindow(this) };
            if (dialog.ShowDialog() != true) return;

            var newFlight = dialog.Flight;

            try
            {
                _database.CreateNewFlight(newFlight);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Couldn't modify the database!",
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
                return;
            }

            RefreshFlights();
        }

        private void RemoveFlight(int idx)
        {
            var flight = Flights[idx];
            if (MessageBox.Show(
                "Are you sure?",
                "Remove pilot",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation
            ) != MessageBoxResult.Yes) return;

            try
            {
                _database.RemoveFlight(flight);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Couldn't modify the database!",
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
                return;
            }

            RefreshFlights();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            CreateNewFlight();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RemoveFlight(FlightsTable.SelectedIndex);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshFlights();
        }
    }
}
