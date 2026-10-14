using Ticketsystem_WPF.Model;

namespace Ticketsystem_WPF.Persistenz
{
    public interface IPersistenz<T>
    {
        public void AlleSpeichern(List<T> alleTickets);
        public void Speichern(T ticket);
        public List<Ticket> AlleLaden();
        public Ticket Laden(int id);
        public void Aendern(T ticket);
        public void Loeschen(int id);
    }
}
