using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        void Save();
        void AddUser(User value);
        void Update(User user, User changedUser);
        void Delete(User user);

    }
}
