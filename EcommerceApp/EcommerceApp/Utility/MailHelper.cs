using Entities;
using System.Net;
using System.Net.Mail;

namespace EcommerceApp.Utility;

public class MailHelper
{
    public static bool SendMail(Contact contact)
    {
		try
		{
			SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
			client.Credentials = new NetworkCredential("YOUR GMAIL ADDRESS", "PASSWORD");
			//client.EnableSsl = true; if you use mail ssl in your email address then make this true
			MailMessage message = new MailMessage();
			message.From = new MailAddress("YOUR GMAIL ADDRESS");
			message.To.Add("OTHER MAIL");
			message.Subject = "You have a message from the website";
			message.Body = $"<p> About Message; <hr /> Name: {contact.Name} <hr /> Email: {contact.Email} <hr /> Phone Number: {contact.PhoneNumber} <hr /> Message: {contact.Message} <hr /> Date: {contact.CreateDate}";
			message.IsBodyHtml = true;

			client.Send(message);

			return true;
		}
		catch (Exception)
		{

			return false;
		}
    }
}
