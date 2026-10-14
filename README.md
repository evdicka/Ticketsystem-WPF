# Ticketsystem WPF

Kleine Desktop-App in C# (WPF, .NET 8) zum Verwalten von Support-Tickets.
Man kann Tickets anlegen, bearbeiten, löschen und nach Ticket-ID suchen. Jedes Ticket gehört zu einem Kunden, neue Kunden lassen sich direkt beim Anlegen eines Tickets hinzufügen.
Die Tickets werden in einer MySQL-Datenbank gespeichert, alternativ in einer CSV-Datei.

## Voraussetzungen

- Windows
- Visual Studio 2022 mit „.NET-Desktopentwicklung“ (oder das .NET SDK 9 oder neuer)
- MySQL oder MariaDB, z. B. über [XAMPP](https://www.apachefriends.org/)

## Einrichten

**1. Datenbank anlegen**

`datenbank.sql` ausführen, z. B. in phpMyAdmin über „Importieren“ oder in der Konsole:

```
mysql -u root -e "source datenbank.sql"
```

Das legt die Datenbank `ticketsystem` mit den Tabellen `Kunden` und `Tickets` an, inklusive ein paar Beispieldaten.

**2. Verbindung anpassen**

Die Verbindungsdaten stehen in `Ticketsystem_WPF/Persistenz/DBTicket.cs` und `Ticketsystem_WPF/Persistenz/DBKunde.cs`:

```csharp
string constr = "Server=localhost;Database=ticketsystem;Uid=root;Pwd=;";
```

Das passt zu einer frischen XAMPP-Installation (Benutzer `root`, kein Passwort). Wenn deine Datenbank andere Zugangsdaten hat, `Uid` und `Pwd` in **beiden** Dateien ändern.

**3. Starten**

`Ticketsystem_WPF.sln` in Visual Studio öffnen und mit F5 starten, oder in der Konsole:

```
dotnet run --project Ticketsystem_WPF
```

## Hinweise

- Die App lädt beim Start direkt aus der Datenbank, MySQL muss also laufen.
- Mit den Buttons **DB** und **CSV** wechselt man, wo die Tickets gespeichert werden. Die `Tickets.csv` wird beim ersten Klick auf CSV automatisch angelegt. Die Kunden kommen immer aus der Datenbank.

## Aufbau

- `Model/` – Ticket, Kunde und die Logik (`TicketSystemLogik`)
- `Persistenz/` – Speichern in MySQL (`DBTicket`, `DBKunde`) oder CSV (`CSVTicket`)
- `UI/` – die Fenster (Hauptfenster, Ticket anlegen/bearbeiten, Kunde anlegen)
