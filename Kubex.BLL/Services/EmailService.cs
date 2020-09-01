using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Kubex.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Kubex.BLL.Services
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IEnumerable<Attachment> Attachments;

        public Message(IEnumerable<string> to, string subject, string content, IEnumerable<Attachment> attachments)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ICompanyService _companyService;
        private readonly IConverter _converter;
        private readonly IDailyActivityReportService _dailyActivityReportService;

        public EmailService(IConfiguration configuration,
            ICompanyService companyService,
            IDailyActivityReportService dailyActivityReportService,
            IConverter converter)
        {
            _dailyActivityReportService = dailyActivityReportService;
            _converter = converter;
            _companyService = companyService;
            _configuration = configuration;
        }

        private async Task SendMail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await Send(emailMessage);
        }

        private async Task Send(MimeMessage emailMessage)
        {
            using var client = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                await client.ConnectAsync(
                    _configuration.GetSection("EmailConfiguration:SmtpServer").Value,
                    int.Parse(_configuration.GetSection("EmailConfiguration:Port").Value),
                    true
                );

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.AuthenticateAsync(
                    _configuration.GetSection("EmailConfiguration:Username").Value,
                    _configuration.GetSection("EmailConfiguration:Password").Value
                );

                await client.SendAsync(emailMessage);

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_configuration.GetSection("EmailConfiguration:From").Value));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            var bodyBuilder = new BodyBuilder();

            if (message.Attachments != null && message.Attachments.Any())
            {
                foreach (var attachment in message.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.Name, attachment.ContentStream, new ContentType("application", "pdf"));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        public async Task SendReport(int compantId)
        {
            var company = await _companyService.GetCompanyAsync(compantId);
            var builder = new HtmlContentBuilder();

            builder.AppendHtml("<html><head></head><body>");

            if (company.Posts == null || company.Posts.Count() <= 0)
                throw new ApplicationException("The company does not have any posts to generate a PDF report from.");

            foreach (var post in company.Posts)
            {
                builder.AppendHtmlLine($"<h1>{post.Name}</h1>");
                
                var dars = await _dailyActivityReportService.GetDailyActivityReportsForPostAsync(post.Id);

                foreach (var dar in dars)
                {
                    builder.AppendHtml($"<h2>{dar.Date.ToShortDateString()}</h2>");
                    builder.AppendHtml("<ul>");
                    foreach (var entry in dar.Entries)
                    {

                        builder.AppendHtml($"<li><p>{entry.OccuranceDate.ToShortTimeString()}: {entry.Description}</p></li>");
                        if (entry.ChildEntries.Count() > 0)
                        {
                            builder.AppendHtml("<ul>");
                            foreach (var child in entry.ChildEntries)
                            {
                                builder.AppendHtml($"<li><p>{child.OccuranceDate.ToShortTimeString()}: {child.Description}</li></p>");
                            }
                            builder.AppendHtml("</ul>");
                        }
                    }
                    builder.AppendHtml("</ul>");
                }
            }
            builder.AppendHtml("</body></html>");

            using var writer = new StringWriter();
            builder.WriteTo(writer, HtmlEncoder.Default);

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = writer.ToString(),
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"Daily Activity Reports of {company.Name}",
            };

            var pdf = new HtmlToPdfDocument()
            {
                Objects = { objectSettings },
                GlobalSettings = globalSettings
            };

            var byteArray = _converter.Convert(pdf);
            using var ms = new MemoryStream(byteArray);
            var attachment = new Attachment(ms, "Reports", "application/pdf");

            var message = new Message(
                to: new List<string> { company.Email },
                subject: "Kubex Daily Activity Reports",
                content: "The attachments contains the generated report.",
                attachments: new List<Attachment> { attachment }
            );

            await SendMail(message);
        }
    }
}