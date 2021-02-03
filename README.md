[![NuGet Gallery](https://img.shields.io/badge/NuGet%20Gallery-enbrea.bbsplanung.db-blue.svg)](https://www.nuget.org/packages/Enbrea.BbsPlanung.Db/)
![GitHub](https://img.shields.io/github/license/stuebersystems/enbrea.bbsplanung.db)

# ENBREA BBS-PLANUNG.DB

Eine .NET-Bibliothek zum direkten Lesen von Daten aus der Datenbankdatei **s_daten.mdb** von [BBS-Planung](https://wordpress.nibis.de/bbsplan).

## Installation

```
dotnet add package Enbrea.BbsPlanung.Db
```

## Systemvoraussetzungen

Voraussetzung für den Zugriff ist eine Installation von BBS-Planung. BBS-Planung basiert auf MS Access. ENBREA BBSPLANUNG.DB greift direkt per ADO.NET auf die Access-Datenbank zu. Dabei wird der ODBC-Treiber von MS Access verwendet. Dieser ist standardmäßig nicht Teil des Windows-Betriebssystems, kann jedoch, falls noch nicht vorhanden, nachinstalliert werden:

1. Öffne die Webseite zum Download der [Microsoft Access Database Engine 2016 Redistributable](https://www.microsoft.com/en-us/download/details.aspx?id=54920)

2. Klicke auf **Download** und lade Dir das Setup für 64-Bit `accessdatabaseengine_X64.exe` herunter. :fire: *Sollte MS Access in der 32-Bit-Version installiert sein, dann musst Du Dir das Setup für 32-Bit `accessdatabaseengine.exe` herunterladen, da die Access-Datenbank-Engines für 64-Bit und 32-Bit nicht koexistieren können.*

3. Starte das Setup und folge den Anweisungen.

Der Zugriff per ODBC auf MS Access ist auch Thema eines [Troubleshooting-Artikels](https://docs.microsoft.com/de-de/office/troubleshoot/access/cannot-use-odbc-or-oledb) in der Microsoft-Dokumentation.

## Dokumentation

Die Dokumentation zu dieser Bibliothek findest Du im [GitHub-Wiki](https://github.com/stuebersystems/enbrea.bbsplanung.db/wiki).

## Kann ich mithelfen?

Ja, sehr gerne. Der beste Weg mitzuhelfen ist es, Rückmeldung per Issue-Tracker zu geben und/oder Korrekturen per Pull-Request zu übermitteln.

## Code of conduct (Verhaltensregeln)

In diesem Projekt wurde der [STÜBER SYSTEMS Code of conduct](https://www.stueber.de/code-of-conduct.php) übernommen.
