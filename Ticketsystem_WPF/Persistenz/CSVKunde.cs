using System.IO;
using Ticketsystem_WPF.Model;
namespace Ticketsystem_WPF.Persistenz

{
    public class CSVKunde
    {
        private string path = "Kunden.csv";
        public void kundenSpeichern(Dictionary<int, Kunde> kunden)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var k in kunden.Values)
                {
                    string csvKundeZeile = string.Join(";", k.KundenNummer, k.Vorname, k.Nachname);
                    sw.WriteLine(csvKundeZeile);
                }
            }
        }

        public Dictionary<int, Kunde> kundenLaden()
        {
            Dictionary<int, Kunde> kundenDic = new Dictionary<int, Kunde>();

            using (StreamReader sr = new StreamReader(path))
            {
                string kundeZeile = sr.ReadLine();
                while (kundeZeile != null)
                {
                    string[] kundenAttribute = kundeZeile.Split(";");

                    Kunde k = new Kunde(
                        kundenAttribute[1],
                        kundenAttribute[2],
                        Convert.ToInt32(kundenAttribute[0])
                        );

                    kundenDic[k.KundenNummer] = k;

                    kundeZeile = sr.ReadLine();
                }
            }

            return kundenDic;
        }
    }
}
