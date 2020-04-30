using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace StoreBuy.Utilities
{
    public class Utility
    {
        public static bool SendEmail(string Email,string Body,string Subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(Resources.MailHost);
                mail.From = new MailAddress(Resources.FromMailAddress);
                mail.To.Add(Resources.ToMailAddress);
                mail.Subject = Subject;
                mail.Body = Body;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(Resources.FromMailAddress, Resources.MailPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return true;
            }
            catch
            {
                return false;
               
            }
            
        }
        public static long GetRandomNumber()
        {
            Random random = new Random();
            var OTP = random.Next(100000, 999999);
            return OTP;
        }

        public static bool ValidateEmail(string email)
        {

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            return false;
        }

        public static bool ValidatePassword(string passWord)
        {
            if (passWord.Length < 8)
                return false;
            int validConditions = 0;
            foreach (char c in passWord)
            {
                if (c >= 'a' && c <= 'z')
                {
                    validConditions++;
                    break;
                }
            }
            foreach (char c in passWord)
            {
                if (c >= 'A' && c <= 'Z')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 0) return false;
            foreach (char c in passWord)
            {
                if (c >= '0' && c <= '9')
                {
                    validConditions++;
                    break;
                }
            }
            if (validConditions == 1) return false;
            if (validConditions == 2)
            {
                char[] special = { '@', '#', '$', '%', '^', '&', '+', '=' };
                if (passWord.IndexOfAny(special) == -1) return false;
            }
            return true;
        }
    }
}