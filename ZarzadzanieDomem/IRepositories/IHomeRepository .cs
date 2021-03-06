using System.Collections.Generic;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface IHomeRepository
    {
        public IEnumerable<Home> GetAll();
        void Save();
        void Create(Home home);
        void Update(Home home, Home changedHome);
        void Delete(Home home);
        Home GetById(uint? id);
        public Home GetByUser(User user);


    }
}
