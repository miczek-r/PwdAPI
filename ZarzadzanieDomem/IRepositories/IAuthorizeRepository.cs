using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    interface IAuthorizeRepository
    {
        public User GetUserByEmail(string email, string password);

    }
}
