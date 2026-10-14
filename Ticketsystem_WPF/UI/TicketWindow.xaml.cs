using System.Windows;
using System.Windows.Controls;
using Ticketsystem_WPF.Model;
using Ticketsystem_WPF.Persistenz;
using Ticketsystem_WPF.UI;

namespace Ticketsystem_WPF
{
    /// <summary>
    /// Interaktionslogik für TicketWindow.xaml
    /// </summary>


    public partial class TicketWindow : Window
    {
        private TicketSystemLogik ticketSystemLogik;



        public TicketWindow(TicketSystemLogik tsl)
        {
            InitializeComponent();
            ticketSystemLogik =tsl;
            KundenListeLaden();
            loadDate();
            loadStatus();

        }

       

        public void loadDate()
        {
            Ticket t = new Ticket();
            DatumLabel.Content = t.ErstellungsDatum.ToShortDateString();
        }


        private void KundeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void KundenListeLaden()
        {
            List<Kunde> kunden = ticketSystemLogik.GetAlleKunden();
            foreach (Kunde k in kunden)
            {
                KundeComboBox.Items.Add(k);
            }
        }

        public void loadStatus()
        {
            foreach (TicketStatus status in Enum.GetValues(typeof(TicketStatus)))
            {
                StatusComboBox.Items.Add(status);
            }
            StatusComboBox.SelectedIndex = 0;
        }

        private void UeberschriftTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UeberschriftTextBox.Text == "" || BeschreibungTextBox.Text == "")
                {
                    MessageBox.Show("Bitte alle Felder ausfüllen.");

                }
                else
                {
                    string ueberschrift = UeberschriftTextBox.Text;
                    string beschreibung = BeschreibungTextBox.Text;
                    Kunde ausgewaehlterKunde = (Kunde)KundeComboBox.SelectedItem;

                    if (ausgewaehlterKunde == null)
                    {
                        MessageBox.Show("Bitte einen Kunden auswählen.");
                        return;
                    }

                    Ticket newTicket = ticketSystemLogik.TicketAnlegen(ueberschrift, beschreibung, ausgewaehlterKunde.KundenNummer);
                    MessageBox.Show($"Ticket mit ID {newTicket.Id} wurde erfolgreich angelegt.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Anlegen des Tickets: {ex.Message}");
            }
            this.DialogResult = true;
        }

        private void AbbrechButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            KundeWindow window = new KundeWindow(ticketSystemLogik);
            window.ShowDialog();
            KundeComboBox.Items.Clear();
            KundenListeLaden();
        }
    }
}
