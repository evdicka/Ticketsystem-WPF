using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Ticketsystem_WPF.Model;

namespace Ticketsystem_WPF.Persistenz
{
    public class DBKunde
    {

        string constr = "Server=localhost;Database=ticketsystem;Uid=root;Pwd=;";

        public List<Kunde> AlleLaden()
        {
            string sql = "Select * from Kunden";
            List<Kunde> kundenList = new List<Kunde>();
            using MySqlConnection con = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                con.Open();
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Kunde k = new Kunde(
                        reader.GetString("Vorname"),
                        reader.GetString("Nachname"),
                        reader.GetInt32("KundenNummer")
                        );
                    kundenList.Add(k);
                }
            }
            return kundenList;
        }

        public void AlleSpeichern(List<Kunde> lstKunden)
        {
            string sql = "Insert into Kunden(Vorname, Nachname) values (@vorname, @nachname)";
            using MySqlConnection con = new MySqlConnection(constr);
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();

            foreach(Kunde k in lstKunden)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@vorname", k.Vorname);
                cmd.Parameters.AddWithValue("@nachname", k.Nachname);
                cmd.ExecuteNonQuery();
                k.KundenNummer = (int)cmd.LastInsertedId;
            }
        }

      

        public void Speichern(Kunde k)
        {
            string sql = "Insert into Kunden (Vorname, Nachname) values (@Vorname, @Nachname)";
            using MySqlConnection con = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@Vorname", k.Vorname);
                cmd.Parameters.AddWithValue("@Nachname", k.Nachname);
                cmd.ExecuteNonQuery();
                k.KundenNummer = (int)cmd.LastInsertedId;
            }
        }

        public Kunde Laden(int id)
        {
            string sql = "Select * from Kunden where KundenNummer = @id";
            using MySqlConnection con = new MySqlConnection(constr);
            using (MySqlCommand cmd = new MySqlCommand(sql, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("@id", id);
                using MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Kunde(
                        reader.GetString("Vorname"),
                        reader.GetString("Nachname"),
                        reader.GetInt32("KundenNummer"));

                }
            }
            return null;
        }

        public void Loeschen(int id)
        {
            string sql = "Delete from Kunden where KundenNummer = @id";

            using MySqlConnection con = new MySqlConnection(constr);
            using MySqlCommand cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Aendern(Kunde kunde)
        {
            string sql = "UPDATE Kunden SET Vorname = @Vorname, Nachname = @Nachname WHERE KundenNummer = @Id;";

            using MySqlConnection con = new MySqlConnection(constr);
            using MySqlCommand cmd = new MySqlCommand(sql, con);

            con.Open();

            cmd.Parameters.AddWithValue("@id", kunde.KundenNummer);
            cmd.Parameters.AddWithValue("@Vorname", kunde.Vorname);
            cmd.Parameters.AddWithValue("@Nachname", kunde.Nachname);

            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
            {
                throw new Exception("Kein Kunde mit dieser ID gefunden.");
            }
        }
    }
}
