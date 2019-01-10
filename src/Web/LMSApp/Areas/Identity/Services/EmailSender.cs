
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace LMSApp.Areas.Identity.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options.SendGridKey, subject, message, email);
        }

        //public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        //{
        //    //var apiKey = "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";
        //    var client = new SendGridClient(this.Options.SendGridKey);
        //    var from = new EmailAddress("divelinas@gmail.com", "Ivelina Dimitrova");
        //    var to = new EmailAddress(email);
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
        //    var response = await client.SendEmailAsync(msg);
        //    var responseCode = response.StatusCode;
        //    var body = await response.Body.ReadAsStringAsync();
        //}

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("divelina@phys.uni-sofia.bg", "Ivelina Dimitrova"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}