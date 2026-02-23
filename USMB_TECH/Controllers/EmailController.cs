using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit;
using MailKit.Net.Smtp;
using USMB_TECH.Models;
using static System.Net.WebRequestMethods;

namespace USMB_TECH.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {

        private readonly IConfiguration _config;

        public EmailController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] EmailRequest request)
        {
            try
            {
                var emailAddress = _config["EmailSettings:Email"];
                var password = _config["EmailSettings:Password"];

                Console.WriteLine("Email: " + emailAddress);
                Console.WriteLine("Password: " + password);
                //await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                //await smtp.AuthenticateAsync(emailAddress, password);

                var email = new MimeMessage();

                // IMPORTANT : on envoie depuis TON compte SMTP
                email.From.Add(MailboxAddress.Parse(emailAddress));

                // On envoie au destinataire choisi
                email.To.Add(MailboxAddress.Parse(request.ToEmail));

                // Si quelqu’un répond, ça répond à l’expéditeur du formulaire
                email.ReplyTo.Add(MailboxAddress.Parse(request.FromEmail));

                email.Subject = "Message depuis le site";

                var builder = new BodyBuilder
                {
                    TextBody = request.Message
                };

                if (request.Attachment != null)
                {
                    using var ms = new MemoryStream();
                    await request.Attachment.CopyToAsync(ms);
                    builder.Attachments.Add(request.Attachment.FileName, ms.ToArray());
                }

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(
                    _config["EmailSettings:SmtpServer"],
                    int.Parse(_config["EmailSettings:Port"]),
                    MailKit.Security.SecureSocketOptions.StartTls
                );

                await smtp.AuthenticateAsync(emailAddress, password);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return Ok("Email envoyé");
            }
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, ex.ToString());
            }

        }
    }
}