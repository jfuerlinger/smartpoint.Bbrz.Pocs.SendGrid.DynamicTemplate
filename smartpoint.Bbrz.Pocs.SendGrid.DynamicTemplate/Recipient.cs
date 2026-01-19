namespace smartpoint.Bbrz.Pocs.SendGrid.DynamicTemplate;

public record Recipient(string Email, string Label)
{
    public override string ToString() => Label;
}
