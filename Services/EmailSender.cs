using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using PostmarkDotNet;

namespace QuizzesApp.Services
{
    public class AuthMessageSenderOptions
    {
        public string? PostmarkServerToken { get; set; } = string.Empty; //API Key: PostmarkServerToken
    }
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                           ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.PostmarkServerToken))
            {
                throw new Exception("Check SMTP server token");
                //check %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\
            }
            await Execute(Options.PostmarkServerToken, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var msg = new PostmarkMessage()
            {
                //No reply address sender setup
                To = toEmail,
                From = "quizzes_admin@rhdeveloping.com",
                Subject = subject,
                TextBody = message,
                HtmlBody = message,
                TrackOpens = true
            };

            var client = new PostmarkClient(apiKey);
            var response = await client.SendMessageAsync(msg);

            //_logger.LogInformation((response.ErrorCode == 200 & response.Message == "OK")
            _logger.LogInformation(response.Message == "OK"
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }

}
