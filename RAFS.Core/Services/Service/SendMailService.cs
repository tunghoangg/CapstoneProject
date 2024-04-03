using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RAFS.Common.Models;
using RAFS.Core.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;

namespace RAFS.Core.Services.Service
{
    public class SendMailService : ISendMailService
    {
        private readonly MailSettings _settings;
        private readonly ILogger<SendMailService> _logger;

        public SendMailService(IOptions<MailSettings> settings, ILogger<SendMailService> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }
        public async Task sendMailAsync(string toAddress, string subject, string body)
        {
            var email = new MimeMessage();

            email.Sender = new MailboxAddress(_settings.DisplayName, _settings.Mail);
            email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Mail));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                try
                {
                    //smtp.Connect(_settings.Host, _settings.Port, false);
                    smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_settings.Mail, _settings.Password);
                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                    //System.IO.Directory.CreateDirectory("mailssave");
                    //var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                    //await email.WriteToAsync(emailsavefile);

                    _logger.LogError(ex.Message);
                }

                smtp.Disconnect(true);

                _logger.LogInformation("send mail to " + toAddress);
            }
        }
    }
}
