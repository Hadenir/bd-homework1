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
    public partial class PilotDialog : Window
    {
        public Pilot Pilot { get; set; }

        public PilotDialog(Pilot pilot = null)
        {
            Pilot = pilot ?? new Pilot();

            InitializeComponent();

            if (pilot is not null)
            {
                Title = "Edit pilot";
                CreateButton.Content = "Edit pilot";
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;

            if (Pilot.FirstName.Length < 1 || Pilot.LastName.Length < 1 || Pilot.LicenseNumber is < 1 or > 99999999)
                return;

            DialogResult = true;
        }
    }
}
