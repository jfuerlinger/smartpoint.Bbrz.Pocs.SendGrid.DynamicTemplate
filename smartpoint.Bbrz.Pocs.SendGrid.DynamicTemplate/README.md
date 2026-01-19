# SendGrid Dynamic Template Mailer

Eine interaktive Konsolen-Anwendung zum Versenden von E-Mails über SendGrid Dynamic Templates mit einer modernen TUI (Text User Interface).

## ✨ Features

- **Interaktive Benutzeroberfläche** mit [Spectre.Console](https://spectreconsole.net/)
- **Empfänger-Auswahl** aus einer vordefinierten Liste oder alle Empfänger auf einmal
- **Titel-Auswahl** aus vordefinierten Optionen oder Freitext-Eingabe
- **Progress-Spinner** während des E-Mail-Versands
- **Wiederholter Versand** ohne Neustart der Anwendung
- **Fehlerbehandlung** mit detaillierten Fehlermeldungen

## 📋 Voraussetzungen

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Ein [SendGrid](https://sendgrid.com/) Account mit API-Key
- Ein konfiguriertes Dynamic Template in SendGrid

## ⚙️ Konfiguration

### Umgebungsvariablen

Die Anwendung benötigt folgende Umgebungsvariable:

| Variable | Beschreibung |
|----------|--------------|
| `SENDGRID_API_KEY` | Dein SendGrid API-Key mit Berechtigung zum E-Mail-Versand |

### Umgebungsvariable setzen

**Windows (PowerShell):**
```powershell
$env:SENDGRID_API_KEY = "SG.xxxxxxxxxxxxxxxxxxxxx"
```

**Windows (CMD):**
```cmd
set SENDGRID_API_KEY=SG.xxxxxxxxxxxxxxxxxxxxx
```

**Linux/macOS:**
```bash
export SENDGRID_API_KEY="SG.xxxxxxxxxxxxxxxxxxxxx"
```

**Dauerhaft in Windows (User-Ebene):**
```powershell
[Environment]::SetEnvironmentVariable("SENDGRID_API_KEY", "SG.xxxxxxxxxxxxxxxxxxxxx", "User")
```

## 🚀 Starten der Anwendung

1. **Repository klonen:**
   ```bash
   git clone https://github.com/jfuerlinger/smartpoint.Bbrz.Pocs.SendGrid.DynamicTemplate.git
   cd smartpoint.Bbrz.Pocs.SendGrid.DynamicTemplate
   ```

2. **Abhängigkeiten wiederherstellen:**
   ```bash
   dotnet restore
   ```

3. **Anwendung starten:**
   ```bash
   dotnet run --project smartpoint.Bbrz.Pocs.SendGrid.DynamicTemplate
   ```

## 📖 Verwendung

Nach dem Start der Anwendung wirst du durch folgende Schritte geführt:

1. **Empfänger auswählen** - Wähle mit den Pfeiltasten einen einzelnen Empfänger oder "Alle Empfänger"
2. **Titel auswählen** - Wähle einen vordefinierten Titel oder gib einen eigenen ein
3. **Zusammenfassung prüfen** - Die ausgewählten Optionen werden angezeigt
4. **E-Mail-Versand** - Die E-Mails werden mit einem Progress-Spinner versendet
5. **Wiederholen oder Beenden** - Entscheide ob du weitere E-Mails versenden möchtest

### Verfügbare Empfänger

| Label | E-Mail |
|-------|--------|
| Joe | josef.fuerlinger@smartpoint.at |
| Mike | michael.schlager@bbrz-gruppe.at |
| Christoph | christoph.peterseil@bbrz-gruppe.at |

### Verfügbare Titel

- Staplerführerschein für Experten
- Staplerführerschein für Anfänger
- Nachschulung Staplerführerschein
- ✏️ Eigenen Titel eingeben...

## 🔧 SendGrid Template

Die Anwendung verwendet das Dynamic Template mit der ID:
- **Template-ID:** `d-29a1c481e71c43be967c30b4ad6945c6`

### Template-Variablen

Das Template erwartet folgende dynamische Daten:

```json
{
  "titel": "Der gewählte Titel"
}
```

## 📦 Abhängigkeiten

- [SendGrid](https://www.nuget.org/packages/SendGrid) - SendGrid API Client
- [Spectre.Console](https://www.nuget.org/packages/Spectre.Console) - Moderne Console UI

## 📄 Lizenz

MIT
