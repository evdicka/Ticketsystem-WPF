using MySql.Data.MySqlClient;
using Ticketsystem_WPF.Persistenz;

namespace Ticketsystem_WPF.Model
{
    public class TicketSystemLogik
    {
        DBKunde dbKunde = new DBKunde();
        IPersistenz<Ticket> persistTicket;
        public TicketSystemLogik(IPersistenz<Ticket> tp)
        {
            persistTicket = tp;
        }

        public Kunde KundeAnlegen(string vorname, string nachname)
        {
            Kunde neuerKunde = new Kunde(vorname, nachname);
            dbKunde.Speichern(neuerKunde);
            return neuerKunde;
        }

        public Kunde GetKunde(int kundenId)
        {
            return dbKunde.Laden(kundenId);
        }

        public List<Kunde> GetAlleKunden()
        {
            return dbKunde.AlleLaden();
        }

        public Ticket TicketAnlegen(string ueberschrift, string beschreibung, int kundenId)
        {
            Kunde k = dbKunde.Laden(kundenId);

            if(k == null)
            {
                throw new Exception("Kein Kunde mit dieser ID gefunden.");
            }
            Ticket t = new Ticket(ueberschrift,beschreibung,kundenId);
            persistTicket.Speichern(t);
            return t;
        }

        public List<Ticket> GetAlleTickets()
        {
            return persistTicket.AlleLaden();
        }

        public Ticket GetTicket(int ticketId)
        {
           return persistTicket.Laden(ticketId);
        }

        public void TicketLoeschen(int ticketId)
        {
            persistTicket.Loeschen(ticketId);
        }

        public void KundeLoeschen(int kundenId)
        {
            dbKunde.Loeschen(kundenId);

        }

        public void TicketBearbeiten(Ticket t)
        {
            persistTicket.Aendern(t);
        }

        public void TicketUeberschriftBearbeiten(int ticketId, string neueUeberschrift)
        {
            Ticket readTicket = persistTicket.Laden(ticketId);
            readTicket.Ueberschrift = neueUeberschrift;
            persistTicket.Aendern(readTicket);
        }

        public void TicketBeschreibungBearbeiten(int ticketId, string neueBeschreibung)
        {
            Ticket readTicket = persistTicket.Laden(ticketId);
            readTicket.Beschreibung = neueBeschreibung;
            persistTicket.Aendern(readTicket);
        }

        public void TicketStatusBearbeiten(int ticketId, TicketStatus neuerStatus)
        {
            Ticket readTicket = persistTicket.Laden(ticketId);
            readTicket.Status = neuerStatus;
            persistTicket.Aendern(readTicket);
        }
    }
}