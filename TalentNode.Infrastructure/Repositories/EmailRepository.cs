using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TalentNode.Domain.Entities;
using TalentNode.Infrastructure.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using TalentNode.Infrastructure.Migrations;
using TalentNode.Domain.Models;
using TalentNode.Domain.interfaces;

namespace TalentNode.Infrastructure.Repositories
{
    public class EmailRepository: IEmailRepository
    {
        private readonly TalentNodeDbContext _context;
        private readonly IConfiguration _config;

        public EmailRepository(TalentNodeDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<int> SendEmailAsync(string to, string subject, string htmlBody)
        {
            try
            {
                // Build email
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("JobWorld4U", _config["SMTP:From"]));
                email.To.Add(new MailboxAddress("", to));
                email.Subject = subject;
                email.Body = new TextPart("html")
                {
                    Text = htmlBody
                };

                using var smtp = new SmtpClient();
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                // Connect with SSL → required for port 465
                await smtp.ConnectAsync(
                    _config["SMTP:Host"],
                    int.Parse(_config["SMTP:Port"]),
                    SecureSocketOptions.SslOnConnect
                );

                // Authenticate
                await smtp.AuthenticateAsync(
                    _config["SMTP:Username"],
                    _config["SMTP:Password"]
                );

                // Send email
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return 1;
            }
            catch (Exception ex)
            {
                // Log ex if needed
                return 0;
            }
        }
        public async Task<int> SendSignupOtp( EmailVerification request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return 0;

            // 1. Generate OTP
            string otp = new Random().Next(100000, 999999).ToString();

            // 2. Store OTP in DB
            var otpEntry = new EmailOTP
            {
                Email = request.Email,
                OTP = otp,
                ExpireAt = DateTime.UtcNow.AddMinutes(10),
                IsVerified = false
            };

            _context.EmailOTP.Add(otpEntry);
            await _context.SaveChangesAsync();

            // 3. Load Template from DB
            var template = await _context.EmailTemplates
                .FirstOrDefaultAsync(x => x.Name == "EmailVerificationOTP");

            if (template == null)
                return 0;

            // 4. Replace placeholders in email body
            string emailBody = template.Body
                .Replace("{{OTP_CODE}}", otp)
                .Replace("{{CURRENT_YEAR}}", DateTime.Now.Year.ToString());

            // 5. Send Email
          var result=  await SendEmailAsync(request.Email, template.Subject, emailBody);

            return result;
        }
    }
}
