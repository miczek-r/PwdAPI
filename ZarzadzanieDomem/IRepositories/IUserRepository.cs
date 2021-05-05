using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        User GetById(int id);
        User GetUserByEmail(string email); //TODO: perhaps deletable
        void Save();
        void Create(User value);
        void Update(User user, User changedUser);
        void Delete(User user);
        public void SendVerificationEmail(User user, string token);
        public string TokenGenerator(User user);
        public void SendRestorationEmail(User user, string token);
        public string EncodePasswordToBase64(string password);
        public string DecodeFrom64(string encodedData);
        public User GetUserByRestorationToken(string token);
        public User GetUserByActivationToken(string token);

    }
}
