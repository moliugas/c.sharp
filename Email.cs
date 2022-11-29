using System.Net.Mail;
using System.Net;

namespace RestaurantSystem
{
    public static class Email
    {
        public static void Send(string to, string subject, string body)
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("3571ea93f08ef3", "7ded61f0df0622"),
                EnableSsl = true,
            };

            var message = new MailMessage("restaurant@project.com", to)
            {
                IsBodyHtml = true,
                Body = body,
                Subject = subject
            };

            client.Send(message);

            Console.WriteLine($"Sent to {to}");

        }
    }
}
