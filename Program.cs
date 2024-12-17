using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

Console.WriteLine("Email Sending Program");

static KeyValuePair<bool, string> SendMail(
	string stremailFrom, string strpassword, string strHost, string fromDisplayName,
	string subject, string body, string emailTo = "", string cc = "", string bcc = "",
	string displayTo = "", string displayCC = "", string displayBCC = "")
{
	try
	{
		using (MailMessage mail = new MailMessage())
		{
			mail.Subject = subject;
			mail.Body = body;
			mail.IsBodyHtml = true;

			if (!string.IsNullOrEmpty(emailTo))
			{
				foreach (var item in emailTo.Split(';'))
				{
					mail.To.Add(new MailAddress(item.Trim(), string.IsNullOrEmpty(displayTo) ? item.Trim() : displayTo));
				}
			}

			mail.From = new MailAddress(stremailFrom, fromDisplayName);
			mail.ReplyToList.Add(new MailAddress(stremailFrom, fromDisplayName));

			var smtp = new SmtpClient
			{
				Host = strHost,
				Port = 587,
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(stremailFrom, strpassword),
				Timeout = 20000,
				DeliveryMethod = SmtpDeliveryMethod.Network
			};

			smtp.Send(mail);
		}
		return new KeyValuePair<bool, string>(true, "Successfully sent mail.");
	}
	catch (Exception e)
	{
		return new KeyValuePair<bool, string>(false, "Error: " + e.Message);
	}
}

// Email details
string emailFrom = "hpatel.datasos@gmail.com";
string password = "paqo gknp esod tzmk";
string host = "smtp.gmail.com";
string fromDisplayName = "Harsh Patel";
string todaydate = DateTime.Now.ToString("yyyy-MM-dd");
string subject = $" Search Results - {todaydate}";
string body = $"<h2>For 10  data is updated in the table. Please review and update the BBL in the AM.</h2>";
string emailTo = "mkoradiya.datasos@gmail.com";

KeyValuePair<bool, string> result = SendMail(emailFrom, password, host, fromDisplayName, subject, body, emailTo);

if (result.Key)
{
	Console.WriteLine("Email sent successfully!");
}
else
{
	Console.WriteLine("Failed to send email: " + result.Value);
}
