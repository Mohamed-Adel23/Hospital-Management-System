using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace HMSproject.Models;

public class EmailConfirm:IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // var fMail = "MoatafaConfirm@outlook.com";
        // var fMail = "HMSContactMostafa@outlook.com";
        var fMail = "HMSWebProject@outlook.com";
        var fPassword = "#Bev8Y_G*dp.4Ve";

        var theMsg = new MailMessage();
        theMsg.From = new MailAddress(fMail);
        theMsg.Subject = subject;
        theMsg.To.Add(email);
        theMsg.Body = $"<html><body>{htmlMessage}</body></html>";
        theMsg.IsBodyHtml = true;

        var smtpClint = new SmtpClient("smtp-mail.outlook.com")
        { 
            EnableSsl = true ,
            Credentials = new NetworkCredential(fMail, fPassword), 
            Port= 587
        };

        smtpClint.Send(theMsg);
    }
}