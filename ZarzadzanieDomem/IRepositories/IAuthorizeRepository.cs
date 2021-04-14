using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    interface IAuthorizeRepository
    {
        User GetUserByEmail(string email, string password);

    }
}
