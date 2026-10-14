-- Legt die Datenbank für das Ticketsystem an.
-- Einmal ausführen, z. B. in phpMyAdmin, MySQL Workbench oder mit: mysql -u root < datenbank.sql

CREATE DATABASE IF NOT EXISTS ticketsystem DEFAULT CHARACTER SET utf8mb4;
USE ticketsystem;

CREATE TABLE Kunden (
    KundenNummer INT AUTO_INCREMENT PRIMARY KEY,
    Vorname      VARCHAR(50) NOT NULL,
    Nachname     VARCHAR(50) NOT NULL
);

CREATE TABLE Tickets (
    Id               INT AUTO_INCREMENT PRIMARY KEY,
    Ueberschrift     VARCHAR(100) NOT NULL,
    Beschreibung     TEXT NOT NULL,
    ErstellungsDatum DATETIME NOT NULL,
    Status           VARCHAR(20) NOT NULL, -- Neu, InBearbeitung, Testing oder Fertig
    KundenId         INT NOT NULL,
    -- Wird bei jeder Änderung automatisch neu gesetzt. So merkt DBTicket.Aendern,
    -- wenn ein anderer Benutzer das Ticket in der Zwischenzeit geändert hat.
    LastModified     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (KundenId) REFERENCES Kunden(KundenNummer)
);

-- Beispieldaten zum Ausprobieren
INSERT INTO Kunden (Vorname, Nachname) VALUES
    ('Max', 'Mustermann'),
    ('Erika', 'Musterfrau');

INSERT INTO Tickets (Ueberschrift, Beschreibung, ErstellungsDatum, Status, KundenId) VALUES
    ('Drucker druckt nicht', 'Der Drucker meldet einen Papierstau, obwohl kein Papier feststeckt.', NOW(), 'Neu', 1),
    ('Passwort vergessen', 'Ich komme nicht mehr in mein E-Mail-Postfach.', NOW(), 'InBearbeitung', 2);
