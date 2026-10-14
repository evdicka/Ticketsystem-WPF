using System.Windows;
using System.Windows.Controls;
using Ticketsystem_WPF.Model;
using Ticketsystem_WPF.Persistenz;

namespace Ticketsystem_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TicketSystemLogik ticketSystemLogik;
        
        public MainWindow()
        {
            InitializeComponent();
            ticketSystemLogik = new TicketSystemLogik(new DBTicket());
            TicketsLaden();


        }
        private void TicketsLaden()
        {
            TicketListe.Items.Clear();
            List<Ticket> tickets = ticketSystemLogik.GetAlleTickets();

            foreach (Ticket ticket in tickets)
            {
                TicketListe.Items.Add(ticket);
            }
        }


        private void NeuButton_Click(object sender, RoutedEventArgs e)
        {
            TicketWindow window = new TicketWindow(ticketSystemLogik);
            window.ShowDialog();
            if (window.DialogResult == true)
                TicketsLaden();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Ticket ausgewaehlterTicket = (Ticket)TicketListe.SelectedItem;
            EditWindow window = new EditWindow(ausgewaehlterTicket, ticketSystemLogik);

            window.ShowDialog();
            TicketsLaden();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Ticket ausgewaehltesTicket = (Ticket)TicketListe.SelectedItem;

            MessageBoxResult result = MessageBox.Show("Ticket " + ausgewaehltesTicket.Id + " wirklich löschen?", "Löschen bestätigen",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                ticketSystemLogik.TicketLoeschen(ausgewaehltesTicket.Id);
                TicketsLaden();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int suche = Convert.ToInt32(SearchTextBox.Text);
            Ticket searchticket = ticketSystemLogik.GetTicket(suche);

            if (searchticket != null)
            {
                TicketListe.Items.Clear();
                TicketListe.Items.Add(searchticket);
            }
            else
            {
                MessageBox.Show("Ticket nicht gefunden.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            TicketsLaden();
        }

        private void Aktualisier_Click(object sender, RoutedEventArgs e)
        {
            TicketsLaden();
        }

        private void TicketListe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TicketListe.SelectedItem != null)
            {
                DeleteButton.IsEnabled = true;
                EditButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
                EditButton.IsEnabled = false;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ticketSystemLogik = new TicketSystemLogik(new DBTicket());
            TicketsLaden();
        }

        private void CSVSpeichern_Click(object sender, RoutedEventArgs e)
        {
            ticketSystemLogik = new TicketSystemLogik(new CSVTicket());
            TicketsLaden();
        }
    }
}