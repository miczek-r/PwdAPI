using System.Collections.Generic;
using System.Linq;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DatabaseContext _context;
        public HomeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Home> GetAll()
        {
            return _context.Homes.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Create(Home home)
        {
            _context.Homes.Add(home);
        }
        public void Update(Home home, Home changedHome)
        {
            home.City = changedHome.City;
            home.HomeId = changedHome.HomeId;
            home.PostCode = changedHome.PostCode;
            home.Street = changedHome.Street;
            home.HouseNumber = changedHome.HouseNumber;
            home.HomeName = changedHome.HomeName;
            _context.Homes.Update(home);
        }
        public void Delete(Home home)
        {
            _context.Homes.Remove(home);
        }
        public Home GetById(uint? id) => _context.Homes.FirstOrDefault(h => h.HomeId == id);
        public Home GetByUser(User user)
        {
            return _context.Homes.Find(user.HomeId);
        }
    }
}
