using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    interface IHomeRepository
    {
        public IEnumerable<Home> GetHomes();
        void Save();
        void AddHome(Home value);
        void Update(Home value);
        void Delete(int id);
        Home GetHomeById(int id);


    }
}
