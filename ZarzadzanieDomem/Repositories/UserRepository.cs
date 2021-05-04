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
                user.Email = changedUser.Email;
                user.Password = changedUser.Password;
                user.DateOfBirth = changedUser.DateOfBirth;
                user.Saldo = changedUser.Saldo;
                user.UserId = changedUser.UserId;
                _context.Users.Update(user);
        }
        public void Delete(User user)
        {           
                _context.Users.Remove(user);
        }
        public void SendVerificationEmail(User user,string token)
        {
            NetworkCredential login = new NetworkCredential("piwo.inf.elektr.@gmail.com", "jasnepelne");
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            MailMessage msg = new MailMessage { From = new MailAddress("piwo.inf.elektr.@gmail.com", "no-reply@CashBuddy.com", Encoding.UTF8) };
            msg.To.Add(new MailAddress(user.Email));
            msg.Subject = "Rejestracja w serwisie PWD";
            msg.Body = "Kliknij w link, aby potwierdzic swoj email: \n"+token;
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

            string finalString =new String(stringChars);
            return finalString;
            
        }

    }
}
