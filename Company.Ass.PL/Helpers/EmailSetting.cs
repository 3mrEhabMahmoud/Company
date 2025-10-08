using System.Net;
using System.Net.Mail;

namespace Company.Ass.PL.Helpers
{
    public class EmailSetting
    {
        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("mrftoo7@gmail.com", "jytuyrhgki8u");//Sender
                client.Send("mrftoo7@gmail.com", email.To, email.Subject, email.Body);

                return true;
           
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
