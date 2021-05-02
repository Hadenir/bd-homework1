using System;
using System.Collections.Generic;
using System.Data;
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
using AviationDb.Database;
using Microsoft.Data.SqlClient;

namespace AviationDb
{
    public partial class MainWindow : IDisposable
    {
        private DatabaseProxy _database;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            _database?.Dispose();
            _database = null;
            GC.SuppressFinalize(this);
        }

        private void EnableMenus(bool enable)
        {
            FlightsMenu.IsEnabled = enable;
            AircraftMenu.IsEnabled = enable;
            PilotsMenu.IsEnabled = enable;
            // AirportsMenu.IsEnabled = enable;
            SummaryMenu.IsEnabled = enable;
            DisconnectMenu.IsEnabled = enable;
        }

        private void ConnectDatabase()
        {
            var connectDialog = new ConnectDialog { Owner = this };
            if (connectDialog.ShowDialog() != true) return;

            try
            {
                _database?.Dispose();
                _database = DatabaseProxy.ConnectTcp(
                    connectDialog.Username,
                    connectDialog.Password,
                    connectDialog.Address,
                    connectDialog.Port
                );
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "Couldn't connect to database!",
                    MessageBoxButton.OK, MessageBoxImage.Error
                );
                return;
            }

            EnableMenus(true);
            ContentFrame.Content = new FlightsPage(_database);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ConnectDatabase();
        }

        private void ConnectMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ConnectDatabase();
        }

        private void DisconnectMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            EnableMenus(false);
            ContentFrame.Content = null;

            _database?.Dispose();
            _database = null;
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Close();
        }

        private void ViewFlightsMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (ContentFrame.Content is not FlightsPage)
                ContentFrame.Content = new FlightsPage(_database);
        }

        private void ViewAircraftMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (ContentFrame.Content is not AircraftPage)
                ContentFrame.Content = new AircraftPage(_database);
        }

        private void ViewPilotsMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (ContentFrame.Content is not PilotsPage)
                ContentFrame.Content = new PilotsPage(_database);
        }

        private void PilotsSummaryMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            var window = new SummaryWindow(_database.PilotsSummary()) {Owner = this};
            window.Show();
        }

        private void AircraftSummaryMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            var window = new SummaryWindow(_database.AircraftSummary()) {Owner = this};
            window.Show();
        }
    }
}