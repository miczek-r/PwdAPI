using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id) => _context.Users.FirstOrDefault(u => u.UserId == id);

        public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email.Equals(email.Trim()));

        public IEnumerable<User> GetByHomeId(int HomeId) => _context.Users.Where(u => u.HomeId == HomeId);

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Create(User value)
        {
            _context.Users.Add(value);
        }
        public void Update(User user, User changedUser)
        {
            user.FirstName = changedUser.FirstName;
            user.LastName = changedUser.LastName;
            user.DateOfBirth = changedUser.DateOfBirth;
            user.Saldo = changedUser.Saldo;
            user.ExpenseLimit = changedUser.ExpenseLimit;
            user.HomeId = changedUser.HomeId;
            _context.Users.Update(user);
        }
        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }
        public void SendVerificationEmail(User user, string token)
        {
            NetworkCredential login = new NetworkCredential("piwo.inf.elektr.@gmail.com", "jasnepelne");
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            MailMessage msg = new MailMessage { From = new MailAddress("piwo.inf.elektr.@gmail.com", "no-reply@CashBuddy.com", Encoding.UTF8) };
            msg.To.Add(new MailAddress(user.Email));
            msg.Subject = "Rejestracja w serwisie PWD";
            msg.Body = "Kliknij w link, aby potwierdzic swoj email: \n" + token;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            string userstate = "Testowanie";
            client.SendAsync(msg, userstate);
        }
        public void SendRestorationEmail(User user, string token)
        {
            NetworkCredential login = new NetworkCredential("piwo.inf.elektr.@gmail.com", "jasnepelne");
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            MailMessage msg = new MailMessage { From = new MailAddress("piwo.inf.elektr.@gmail.com", "no-reply@CashBuddy.com", Encoding.UTF8) };
            msg.To.Add(new MailAddress(user.Email));
            msg.Subject = "CashBuddy: Zmiana hasla";
            msg.Body = "W serwisie cashbuddy zostala zgloszona proba zmiany hasla na twoim koncie. Jesli to ty, to kliknij w link: \n" + token;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            string userstate = "Testowanie";
            client.SendAsync(msg, userstate);
        }

        public string TokenGenerator(User user)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[20];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string finalString = new String(stringChars);
            return finalString;

        }
        public string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public User GetUserByActivationToken(string token) => _context.Users.FirstOrDefault(u => u.ActivationToken == token);
        public User GetUserByRestorationToken(string token) => _context.Users.FirstOrDefault(u => u.PasswordRestorationToken == token);
    }
}
