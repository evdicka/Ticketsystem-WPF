using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.IO;
using Ticketsystem_WPF.Model;

namespace Ticketsystem_WPF.Persistenz
{
    public class CSVTicket : IPersistenz<Ticket>
    {
        private string path = "Tickets.csv";

        public CSVTicket()
        {
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }
        }

        public void AlleSpeichern(List<Ticket> alleTickets)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var t in alleTickets)
                {
                    string zeile = string.Join(";", t.Id, t.Ueberschrift, t.Beschreibung, t.ErstellungsDatum.ToString("o"), t.Status.ToString(), t.KundenId);
                    writer.WriteLine(zeile);
                }
            }
        }

        public void Speichern(Ticket ticket)
        {
            List<Ticket>alletickets = AlleLaden();

            foreach (Ticket t in alletickets)
            {
                if(t.Id == ticket.Id)
                {
                    throw new Exception("Ticket mit dieser ID existiert bereits.");
                }
            }

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                string zeile = string.Join(";", ticket.Id, ticket.Ueberschrift, ticket.Beschreibung, ticket.ErstellungsDatum.ToString("o"), ticket.Status.ToString(), ticket.KundenId);
                writer.WriteLine(zeile);
            }
        }

        public List<Ticket> AlleLaden()
        {
            List<Ticket> ticketList = new List<Ticket>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    string[] teile = line.Split(';');
                    if (teile.Length == 6)
                    {
                        Ticket t = new Ticket(
                            teile[1],
                            teile[2],
                            Convert.ToInt32(teile[5]),
                            Convert.ToInt32(teile[0]),
                            DateTime.Parse(teile[3]),
                            (TicketStatus)Enum.Parse(typeof(TicketStatus), teile[4])
                        );
                        ticketList.Add(t);
                    }
                    line = reader.ReadLine();
                }
                return ticketList;
            }
        }

        public Ticket Laden(int id)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] teile = line.Split(';');

                    if (teile.Length == 6 && Convert.ToInt32(teile[0]) == id)
                    {
                        return new Ticket(
                            teile[1],
                            teile[2],
                            Convert.ToInt32(teile[5]),
                            Convert.ToInt32(teile[0]),
                            DateTime.Parse(teile[3]),
                            (TicketStatus)Enum.Parse(typeof(TicketStatus), teile[4])
                        );
                    }
                }
            }

            return null;
        }



        public void Aendern(Ticket ticket)
        {
            List<Ticket> alleTickets = AlleLaden();

            foreach (var t in alleTickets)
            {
                if (t.Id == ticket.Id)
                {
                    t.Beschreibung = ticket.Beschreibung;
                    t.Ueberschrift = ticket.Ueberschrift;
                    t.Status = ticket.Status;
                    break;
                }
            }

            AlleSpeichern(alleTickets);
        }


        public void Loeschen(int id)
        {
            List<Ticket> alleTickets = AlleLaden();

            for (int i = 0; i < alleTickets.Count; i++)
            {
                if (alleTickets[i].Id == id)
                {
                    alleTickets.RemoveAt(i);
                    break;
                }
            }
            AlleSpeichern(alleTickets);
        }
    }
}
