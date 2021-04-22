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
        

    }
}
