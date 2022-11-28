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
                EnableSsl = true
            };
            client.Send("restaurant@project.com", to, subject, body);
            Console.WriteLine($"Sent to {to}");

        }
    }
}
