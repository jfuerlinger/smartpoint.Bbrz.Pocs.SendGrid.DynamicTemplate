using SendGrid;
using SendGrid.Helpers.Mail;
using Spectre.Console;
using smartpoint.Bbrz.Pocs.SendGrid.DynamicTemplate;

const string TemplateId = "d-29a1c481e71c43be967c30b4ad6945c6";
const string CustomTitleOption = "✏️  Eigenen Titel eingeben...";
const string AllRecipientsOption = "📧 Alle Empfänger";

var recipients = new List<Recipient>
{
    new("josef.fuerlinger@smartpoint.at", "Joe"),
    new("michael.schlager@bbrz-gruppe.at", "Mike"),
    new("christoph.peterseil@bbrz-gruppe.at", "Christoph")
};

var titleOptions = new List<string>
{
    "Staplerführerschein für Experten",
    "Staplerführerschein für Anfänger",
    "Nachschulung Staplerführerschein",
    CustomTitleOption
};

var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
if (string.IsNullOrEmpty(apiKey))
{
    AnsiConsole.MarkupLine("[red]Fehler: SENDGRID_API_KEY Umgebungsvariable ist nicht gesetzt![/]");
    return;
}

var client = new SendGridClient(apiKey);
var from = new EmailAddress("no-reply@bbrz.at", "no-reply@bbrz.at");

AnsiConsole.Write(new FigletText("SendGrid Mailer").Color(Color.Blue));
AnsiConsole.MarkupLine("[grey]Dynamic Template Email Sender[/]\n");

bool continueLoop = true;

while (continueLoop)
{
    // Empfänger auswählen
    var recipientChoices = new List<string> { AllRecipientsOption };
    recipientChoices.AddRange(recipients.Select(r => r.Label));

    var selectedRecipientLabel = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[green]Wähle den/die Empfänger:[/]")
            .PageSize(10)
            .HighlightStyle(new Style(Color.Green))
            .AddChoices(recipientChoices));

    List<Recipient> selectedRecipients;
    if (selectedRecipientLabel == AllRecipientsOption)
    {
        selectedRecipients = recipients;
    }
    else
    {
        selectedRecipients = [recipients.First(r => r.Label == selectedRecipientLabel)];
    }

    // Titel auswählen
    var selectedTitle = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[green]Wähle den Titel:[/]")
            .PageSize(10)
            .HighlightStyle(new Style(Color.Green))
            .AddChoices(titleOptions));

    if (selectedTitle == CustomTitleOption)
    {
        selectedTitle = AnsiConsole.Prompt(
            new TextPrompt<string>("[green]Eigenen Titel eingeben:[/]")
                .PromptStyle("yellow"));
    }

    // Zusammenfassung anzeigen
    var recipientNames = string.Join(", ", selectedRecipients.Select(r => r.Label));
    AnsiConsole.WriteLine();

    var panel = new Panel(
        new Markup($"[bold]Empfänger:[/] {recipientNames}\n[bold]Titel:[/] {selectedTitle}"))
        .Header("[blue]Zusammenfassung[/]")
        .BorderColor(Color.Blue);
    AnsiConsole.Write(panel);
    AnsiConsole.WriteLine();

    // E-Mails versenden mit Spinner
    await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .SpinnerStyle(Style.Parse("green bold"))
        .StartAsync("[yellow]Sende E-Mails...[/]", async ctx =>
        {
            foreach (var recipient in selectedRecipients)
            {
                ctx.Status($"[yellow]Sende an {recipient.Label}...[/]");

                var dynamicEmailData = new { titel = selectedTitle };

                var msg = new SendGridMessage();
                msg.SetFrom(from);
                msg.AddTo(new EmailAddress(recipient.Email, recipient.Label));
                msg.SetTemplateId(TemplateId);
                msg.SetTemplateData(dynamicEmailData);

                var response = await client.SendEmailAsync(msg);

                if (response.IsSuccessStatusCode)
                {
                    AnsiConsole.MarkupLine($"[green]✓[/] E-Mail an [bold]{recipient.Label}[/] erfolgreich gesendet");
                }
                else
                {
                    var body = await response.Body.ReadAsStringAsync();
                    AnsiConsole.MarkupLine($"[red]✗[/] Fehler beim Senden an [bold]{recipient.Label}[/]: {response.StatusCode}");
                    AnsiConsole.MarkupLine($"[grey]{body}[/]");
                }

                await Task.Delay(500); // Kurze Pause zwischen den Mails
            }
        });

    AnsiConsole.WriteLine();

    // Fragen ob nochmal
    continueLoop = AnsiConsole.Confirm("[blue]Möchtest du weitere E-Mails versenden?[/]");

    if (continueLoop)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("SendGrid Mailer").Color(Color.Blue));
        AnsiConsole.MarkupLine("[grey]Dynamic Template Email Sender[/]\n");
    }
}

AnsiConsole.MarkupLine("\n[green]Auf Wiedersehen! 👋[/]");
