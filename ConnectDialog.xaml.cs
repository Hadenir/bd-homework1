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

namespace AviationDb
{
    public partial class ConnectDialog
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Address { get; private set; }
        public int Port { get; private set; }

        public ConnectDialog()
        {
            InitializeComponent();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            Username = UsernameTextBox.Text;
            Password = PasswordTextBox.Password;
            Address = AddressTextBox.Text;

            if (Username.Length == 0 || Address.Length == 0) return;

            try
            {
                Port = int.Parse(PortTextBox.Text);
            }
            catch (Exception)
            {
                return;
            }
            
            if(Port is < 0 or > 65535) return;

            DialogResult = true;
        }
    }
}