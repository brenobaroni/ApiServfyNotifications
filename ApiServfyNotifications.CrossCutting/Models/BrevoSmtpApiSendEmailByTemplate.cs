namespace ApiServfyNotifications.CrossCutting.Models
{
    public class BrevoSmtpApiSendEmailByTemplateRequest
    {
        public int TemplateId { get; set; }
        public IDictionary<string, string> Params { get; set; }
        public ICollection<BrevoSmtpApiSendEmailTo> To { get; set; }
    }

    public class BrevoSmtpApiSendEmailTo
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
