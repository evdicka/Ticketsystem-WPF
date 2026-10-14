using Google.Protobuf.WellKnownTypes;

namespace Ticketsystem_WPF.Model
{
    public enum TicketStatus { Neu, InBearbeitung, Testing, Fertig }
    public class Ticket
    {
        private static int _fortlaufendeNummer = 0;

        private int _id;
        private int _kundenId;
        private DateTime _erstellungsDatum = DateTime.Now;
        private TicketStatus _status;
        private string _beschreibung, _ueberschrift;
        private DateTime _lastModified;

        public int Id { get => _id; set => _id = value; }
        public int KundenId { get => _kundenId; }
        public DateTime ErstellungsDatum { get => _erstellungsDatum; }
        public TicketStatus Status { get => _status; set => _status = value; }
        public string Beschreibung { get => _beschreibung; set => _beschreibung = value; }
        public string Ueberschrift { get => _ueberschrift; set => _ueberschrift = value; }
        public DateTime LastModified { get => _lastModified; set => _lastModified = value; }
       

        // Mehrere Konstruktoren ermöglichen es, Tickets flexibel zu erzeugen – je nach Anwendungsfall:
        // - Der parameterlose Konstruktor kann z. B. für leere Platzhalter oder zur späteren Initialisierung genutzt werden.
        // - Der Konstruktor mit Überschrift, Beschreibung und Kundennummer eignet sich für die normale Ticketerstellung durch den Nutzer.
        // - Der ausführliche Konstruktor mit allen Feldern ist besonders nützlich beim Laden von Tickets aus externen Quellen (z. B. aus einer Datei oder Datenbank), da hier alle Eigenschaften gezielt gesetzt werden können.

        public Ticket()
        {
            // Standardwerte für ein neues Ticket setzen.
        }

        public Ticket(string ueberschrift, string beschreibung, int kundenNummer)
        {
            _ueberschrift = ueberschrift;
            _beschreibung = beschreibung;
            _kundenId = kundenNummer;
            _id = ++_fortlaufendeNummer;
        }

        public Ticket(string ueberschrift, string beschreibung,
            int kundenNummer, int id,
            DateTime erstellDatum, TicketStatus ticketStatus)
        {
            _ueberschrift = ueberschrift;
            _beschreibung = beschreibung;
            _kundenId = kundenNummer;
            _id = id;
            _erstellungsDatum = erstellDatum;
            _status = ticketStatus;
        }

        public Ticket(
            string ueberschrift, string beschreibung,
            int kundenNummer, int id,
            DateTime erstellDatum, TicketStatus ticketStatus, DateTime lastModified)
        {
            _ueberschrift = ueberschrift;
            _beschreibung = beschreibung;
            _kundenId = kundenNummer;
            _id = id;
            _erstellungsDatum = erstellDatum;
            _status = ticketStatus;
            _lastModified = lastModified;

            if (id > _fortlaufendeNummer)
            {
                _fortlaufendeNummer = id;
            }
        }

        public override string ToString()
        {
            return $"{_id,-15} " +
                   $"{_erstellungsDatum.ToShortDateString(),-20} " +
                   $"{_kundenId,-15} " +
                   $"{_ueberschrift,-30} ";
                   
        }

    }
}
