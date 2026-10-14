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
using Ticketsystem_WPF.Persistenz;
using Ticketsystem_WPF.UI;

namespace Ticketsystem_WPF
{
    /// <summary>
    /// Interaktionslogik für EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private DBTicket dbTicket = new DBTicket();
        TicketSystemLogik ticketSystemLogik;
        private Ticket aktuellesTicket;
        public EditWindow(Ticket ticket, TicketSystemLogik tsl)
        {
            InitializeComponent();
            aktuellesTicket = ticket;
            ticketSystemLogik = tsl;
            LoadOldTicketInGUI();
        }

      
        public void LoadOldTicketInGUI()
        {
          

            
                UeberschriftTextBox.Text = aktuellesTicket.Ueberschrift;
                BeschreibungTextBox.Text = aktuellesTicket.Beschreibung;

                StatusComboBox.Items.Clear();
                foreach (TicketStatus stat in Enum.GetValues(typeof(TicketStatus)))
                {
                    StatusComboBox.Items.Add(stat);
                }
                StatusComboBox.SelectedItem = aktuellesTicket.Status;

                DatumLabel.Content = aktuellesTicket.ErstellungsDatum.ToShortDateString();

                KundeComboBox.Items.Clear();
                KundeComboBox.Items.Add(ticketSystemLogik.GetKunde(aktuellesTicket.KundenId));
                KundeComboBox.SelectedIndex = 0;
            
            
        }

        private void KundeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(UeberschriftTextBox.Text =="" || BeschreibungTextBox.Text == "")
                {
                    MessageBox.Show("Fehler: Überschrift und Beschreibung dürfen nicht leer sein.");
                }
                else
                {
                    Ticket freshReloadedTicket = ticketSystemLogik.GetTicket(aktuellesTicket.Id);

                    if (freshReloadedTicket.LastModified != aktuellesTicket.LastModified)
                    {
                        MessageBox.Show("Das Ticket wurde bereits von einem anderen Benutzer geändert. Bitte laden Sie das Ticket neu und versuchen Sie es erneut.");
                        this.Close();
                        return;
                    }
                    
                    
                    aktuellesTicket.Ueberschrift = UeberschriftTextBox.Text;
                    aktuellesTicket.Beschreibung = BeschreibungTextBox.Text;
                    aktuellesTicket.Status = (TicketStatus)StatusComboBox.SelectedItem;
                    
                   ticketSystemLogik.TicketBearbeiten(aktuellesTicket);

                    MessageBox.Show("Ticket erfolgreich bearbeitet!");
                    this.Close();
                    
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Fehler beim Bearbeiten des Tickets: " + ex.Message);
            }
          
        }

        private void UeberschriftTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void BeschreibungTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AbbrechButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
