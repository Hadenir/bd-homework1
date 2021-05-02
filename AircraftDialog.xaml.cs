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
    public partial class AircraftDialog : Window
    {
        public Aircraft Aircraft { get; set; }

        public AircraftDialog(DatabaseProxy database, Aircraft aircraft = null)
        {
            Aircraft = aircraft ?? new Aircraft();

            InitializeComponent();

            TypeComboBox.ItemsSource = database.QueryAllAircraftTypes();
            OwnerComboBox.ItemsSource = database.QueryAllPilots();

            if (aircraft is not null)
            {
                Title = @"Edit aircraft";
                CreateButton.Content = @"Edit aircraft";
                RegistrationTextBox.IsReadOnly = true;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (Aircraft.Registration.Length < 1 || Aircraft.Type == null || Aircraft.Owner == null) return;

            DialogResult = true;
        }
    }
}
