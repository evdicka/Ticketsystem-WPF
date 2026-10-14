using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketsystem_WPF.Model
{
    public class Kunde
    {
        private static int _fortlaufendeNr = 0;

        private string _vorname;
        private string _nachname;
        private int _kundenNummer;

        public string Vorname { get => _vorname; set => _vorname = value; }
        public string Nachname { get => _nachname; set => _nachname = value; }
        public int KundenNummer { get => _kundenNummer; set => _kundenNummer = value; }

        public Kunde()
        {
            _kundenNummer = ++_fortlaufendeNr;
        }

        public Kunde(string vorname, string nachname)
        {
            Vorname = vorname;
            Nachname = nachname;
            _kundenNummer = ++_fortlaufendeNr;
        }

        public Kunde(string vorname, string nachname, int kundenNr)
        {
            Vorname = vorname;
            Nachname = nachname;
            _kundenNummer = kundenNr;

            if (kundenNr > _fortlaufendeNr)
            {
                _fortlaufendeNr = kundenNr;
            }
        }

        public override string ToString()
        {
            return $"{_vorname} {_nachname}";
        }
    }
}
