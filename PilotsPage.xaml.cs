using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AviationDb.Database;
using Microsoft.Data.SqlClient;

namespace AviationDb
{
    public partial class PilotsPage : Page
    {
        private readonly DatabaseProxy _database;

        public ObservableCollection<Pilot> Pilots { get; set; } = new();

        public PilotsPage(DatabaseProxy database)
        {
            _database = database;

            InitializeComponent();

            RefreshPilots();
        }

        private void RefreshPilots()
        {
            Pilots.Clear();
            Pilots.AddRange(_database.QueryAllPilots());
        }

        private void CreateNewPilot()
        {
            var dialog = new PilotDialog {Owner = Window.GetWindow(this)};
            if (dialog.ShowDialog() != true) return;

            var newPilot = dialog.Pilot;

            try
            {
                _database.CreateNewPilot(newPilot);
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

            RefreshPilots();
        }

        private void ModifyPilot(int idx)
        {
            var pilot = Pilots[idx];
            var dialog = new PilotDialog(pilot) {Owner = Window.GetWindow(this)};
            if (dialog.ShowDialog() != true) return;

            var newPilot = dialog.Pilot;

            try
            {
                _database.ModifyPilot(newPilot);
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

            RefreshPilots();
        }

        private void RemovePilot(int idx)
        {
            var pilot = Pilots[idx];
            if (MessageBox.Show(
                "Are you sure?",
                "Remove pilot",
                MessageBoxButton.YesNo, MessageBoxImage.Exclamation
            ) != MessageBoxResult.Yes) return;

            try
            {
                _database.RemovePilot(pilot);
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

            RefreshPilots();
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            CreateNewPilot();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ModifyPilot(PilotsTable.SelectedIndex);
        }

        private void PilotsTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            ModifyPilot(PilotsTable.SelectedIndex);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RemovePilot(PilotsTable.SelectedIndex);
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            RefreshPilots();
        }
    }
}