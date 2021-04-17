using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            return _context.Users.FirstOrDefault(u => u.Email == auth.Email && u.Password == auth.Password);
        }
    }
}

