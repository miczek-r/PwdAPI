using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using ZarzadzanieDomem.Authentication;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private readonly DatabaseContext _context;
        public AuthorizeRepository(DatabaseContext context)
        {
            _context = context;
        }
        public User GetUserByEmail(Auth auth)
        {

            /* Fetch the stored value */
            User user = _context.Users.FirstOrDefault(u => u.Email == auth.Email);
            string savedPasswordHash = user.Password;
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(auth.Password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    throw new UnauthorizedAccessException();
            return user;
        }

        private static void SendEmail(User user, string subject, string body)
        {
            NetworkCredential login = new NetworkCredential("testowymiczek.@gmail.com", "!Admin123");
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = login
            };

            MailMessage msg = new MailMessage { From = new MailAddress("testowymiczek.@gmail.com", "no-reply@CashBuddy.com", Encoding.UTF8) };
            msg.To.Add(new MailAddress(user.Email));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            string userstate = "Testowanie";
            client.SendAsync(msg, userstate);
        }

        public void SendVerificationEmail(User user, string token)
        {
            SendEmail(user, "Rejestracja w serwisie PWD", "Kliknij w link, aby potwierdzic swoj email: \n" + token);
        }

        public void SendRestorationEmail(User user, string token)
        {
            SendEmail(user, "CashBuddy: Zmiana hasla", "W serwisie cashbuddy zostala zgloszona proba zmiany hasla na twoim koncie. Jesli to ty, to kliknij w link: \n" + token);
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
        public string EncodePassword(string password)
        {
            try
            {
                byte[] salt = new byte[16];
                new RNGCryptoServiceProvider().GetBytes(salt);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                return savedPasswordHash;
            }
            catch (Exception ex)
            {
                throw new ArithmeticException("Error in password base64 conversion" + ex.Message);
            }
        } //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
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

