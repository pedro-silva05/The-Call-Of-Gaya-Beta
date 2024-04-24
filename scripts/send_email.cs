using Godot;
using System;
using System.Net;
using System.Net.Mail;
using Environment = System.Environment;


public partial class send_email : Node
{
    public static void Main(string userEmail)
    {
        string Email = Environment.GetEnvironmentVariable("EMAIL_USER");
        string password = Environment.GetEnvironmentVariable("EMAIL_PASS");
        string toEmail = userEmail;
        string subject = "Agradecimento dos time de The Call Of Gaya";
        string body = @"<html>
                        <body>
                            <h1>Obrigado por participar!</h1>
                            <p>Olá, agradecemos seu suporte ao nosso projeto!</p>
                            <p>Atenciosamente, <br> Equipe de The Call Of Gaya</p>
                        </body>
                        </html>";
        SendEmail(Email, password, toEmail, subject, body);
    }

    static void SendEmail(string fromEmail, string password, string toEmail, string subject, string body)
    {
        // Crie uma instância de SmtpClient
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com") // Substitua "smtp.example.com" pelo seu servidor SMTP
        {
            Port = 587, // Geralmente 587 ou 465 para SSL (altere conforme necessário)
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true, // True se o servidor SMTP requer SSL
        };

        // Crie uma mensagem de email
        MailMessage mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true // True se o corpo do email contém HTML, false caso contrário
        };
        mailMessage.To.Add(toEmail);

        try
        {
            // Enviar email
            smtpClient.Send(mailMessage);
            GD.Print("Email enviado com sucesso!");
        }
        catch (Exception ex)
        {
            GD.PrintErr("Falha ao enviar email: " + ex.Message);
        }
    }

}