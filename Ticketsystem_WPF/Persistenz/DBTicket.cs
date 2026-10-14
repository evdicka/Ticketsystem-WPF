using MySql.Data.MySqlClient;
using Ticketsystem_WPF.Model;

namespace Ticketsystem_WPF.Persistenz
{
    public class DBTicket : IPersistenz<Ticket>
    {
        string constr = "Server=localhost;Database=ticketsystem;Uid=root;Pwd=;";

        public void Aendern(Ticket ticket)
        {
            string sql = "UPDATE Tickets SET Ueberschrift = @Ueberschrift, Beschreibung = @Beschreibung, " +
                "ErstellungsDatum = @ErstellungsDatum, Status = @Status WHERE Id = @Id AND LastModified = @LastModified;";

           using MySqlConnection con = new MySqlConnection(constr);
           using MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            cmd.Parameters.AddWithValue("@Id", ticket.Id);
            cmd.Parameters.AddWithValue("@Ueberschrift", ticket.Ueberschrift);
            cmd.Parameters.AddWithValue("@Beschreibung", ticket.Beschreibung);
            cmd.Parameters.AddWithValue("@ErstellungsDatum", ticket.ErstellungsDatum);
            cmd.Parameters.AddWithValue("@Status", ticket.Status.ToString());
            cmd.Parameters.AddWithValue("@LastModified", ticket.LastModified);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                throw new Exception("Das Ticket wurde bereits von einem anderen Benutzer geändert.");
            }
        }

        public List<Ticket> AlleLaden()
        {
            string sql = "Select * from Tickets";
            List<Ticket> ticketList = new List<Ticket>();
            using MySqlConnection con = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                con.Open();
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Ticket t = new Ticket(
                        reader.GetString("Ueberschrift"),
                        reader.GetString("Beschreibung"),
                        reader.GetInt32("KundenId"),
                        reader.GetInt32("Id"),
                        reader.GetDateTime("ErstellungsDatum"),
                        (TicketStatus)Enum.Parse(typeof(TicketStatus), reader.GetString("Status")),
                        reader.GetDateTime("LastModified")
                        );


                    ticketList.Add(t);
                }
            }
            return ticketList;
        }

        public void AlleSpeichern(List<Ticket> alleTickets)
        {
            string sql = "Insert into Tickets (Ueberschrift, Beschreibung, ErstellungsDatum, Status, KundenId) values (@Ueberschrift, @Beschreibung, @ErstellungsDatum, @Status, @KundenId)";
            using MySqlConnection con = new MySqlConnection(constr);
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();

            foreach(Ticket t in  alleTickets)
            {
                cmd.Parameters.Clear();  //Ohne Clear würdest du beim zweiten Durchlauf einen Fehler bekommen
                cmd.Parameters.AddWithValue("@Ueberschrift", t.Ueberschrift);
                cmd.Parameters.AddWithValue("@Beschreibung", t.Beschreibung);
                cmd.Parameters.AddWithValue("@ErstellungsDatum", t.ErstellungsDatum);
                cmd.Parameters.AddWithValue("@Status", t.Status.ToString());
                cmd.Parameters.AddWithValue("@KundenId", t.KundenId);
                cmd.ExecuteNonQuery();
                t.Id = (int)cmd.LastInsertedId;
            }

        }

        public Ticket Laden(int id)
        {
            string sql = "Select * from Tickets where id = @id";
            using MySqlConnection con = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using MySqlDataReader reader = cmd.ExecuteReader();
               
                if (reader.Read())
                {
                    return new Ticket(
                        reader.GetString("Ueberschrift"),
                        reader.GetString("Beschreibung"),
                        reader.GetInt32("KundenId"),
                        reader.GetInt32("Id"),
                        reader.GetDateTime("ErstellungsDatum"),
                        (TicketStatus)Enum.Parse(typeof(TicketStatus), reader.GetString("Status")),
                        reader.GetDateTime("LastModified")
                        );


                }
            }
            return null;
        }

        public void Loeschen(int id)
        {
            string sql = "Delete from Tickets where id = @id";

            using MySqlConnection con = new MySqlConnection(constr);
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Speichern(Ticket t)
        {
            string sql = "Insert into Tickets (Ueberschrift, Beschreibung, ErstellungsDatum, Status, KundenId) values (@Ueberschrift, @Beschreibung, @ErstellungsDatum, @Status, @KundenId)";
            using MySqlConnection con = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Ueberschrift", t.Ueberschrift);
                cmd.Parameters.AddWithValue("@Beschreibung", t.Beschreibung);
                cmd.Parameters.AddWithValue("@ErstellungsDatum", t.ErstellungsDatum);
                cmd.Parameters.AddWithValue("@Status", t.Status.ToString());
                cmd.Parameters.AddWithValue("@KundenId", t.KundenId);
                cmd.ExecuteNonQuery();
                t.Id = (int)cmd.LastInsertedId;
            }
        }

     
    }
}