using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class AircraftPage : Page
    {
        private readonly DatabaseProxy _database;

        public ObservableCollection<Aircraft> Aircraft { get; set; } = new();
        
        public AircraftPage(DatabaseProxy database)
        {
            _database = database;
            
            InitializeComponent();
            
            RefreshAircraft();
        }

        private void RefreshAircraft()
        {
            Aircraft.Clear();
            Aircraft.AddRange(_database.QueryAllAircraft());
        }

        private void CreateNewAircraft()
        {
            var dialog = new AircraftDialog(_database) {Owner = Window.GetWindow(this)};
            if (dialog.ShowDialog() != true) return;

            var newAircraft = dialog.Aircraft;

            try
            {
                _database.CreateNewAircraft(newAircraft);
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

            RefreshAircraft();
        }

        private void ModifyAircraft(int idx)
        {
            var aircraft = Aircraft[idx];
            var dialog = new AircraftDialog(_database, aircraft) { Owner = Window.GetWindow(this) };
            if (dialog.ShowDialog() != true) return;

            var newAircraft = dialog.Aircraft;

            try
            {
                _database.ModifyAircraft(newAircraft);
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

            RefreshAircraft();
        }

        private void RemoveAircraft(int idx)
        {
            var aircraft = Aircraft[idx];
            if (MessageBox.Show(
                "Are you sure?",
                "Remove pilot",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation
            ) != MessageBoxResult.Yes) return;

            try
            {
                _database.RemoveAircraft(aircraft);
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

            RefreshAircraft();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            CreateNewAircraft();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ModifyAircraft(AircraftTable.SelectedIndex);
        }


        private void AircraftTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ModifyAircraft(AircraftTable.SelectedIndex);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RemoveAircraft(AircraftTable.SelectedIndex);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshAircraft();
        }

    }
}
