using System.Collections.Generic;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        User GetById(uint id);
        User GetUserByEmail(string email);
        IEnumerable<User> GetByHomeId(uint? HomeId);
        void Save();
        void Create(User value);
        void Update(User user, User changedUser);
        void Delete(User user);


    }
}
