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
using Ticketsystem_WPF.Model;

namespace Ticketsystem_WPF.UI
{
    /// <summary>
    /// Interaktionslogik für KundeWindow.xaml
    /// </summary>
    
    public partial class KundeWindow : Window
    {
        TicketSystemLogik ticketSystemLogik;
        public KundeWindow(TicketSystemLogik tsl)
        {
            InitializeComponent();
            ticketSystemLogik = tsl;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string vorname = VornameTextBox.Text;
            string nachname = NachnameTextBox.Text;
            if(vorname=="" || nachname == "")
            {
                MessageBox.Show("Bitte geben Sie sowohl den Vor- als auch den Nachnamen ein.");
            }
            else
            {
                
                Kunde newKunde = ticketSystemLogik.KundeAnlegen(vorname, nachname);
                MessageBox.Show($"Kunde mit ID {newKunde.KundenNummer} wurde erfolgreich angelegt.");
            }
            
           
        }

        private void AbbrechButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
