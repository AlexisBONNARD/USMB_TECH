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
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] EmailRequest request)
        {
            try
            {
                var email = new MimeMessage();

                email.From.Add(MailboxAddress.Parse("tonemail@gmail.com"));
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
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
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // IMPORTANT : Ici dois étre mettre mis le email SMTP réel (pas celui du champ FromEmail)
                await smtp.AuthenticateAsync("tonemail@gmail.com", "APP_PASSWORD");

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return Ok("Email envoyé");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}