using System.Collections.Generic;
using System.Linq;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DatabaseContext _context;
        public NotificationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetAll()
        {
            return _context.Notifications.ToList();
        }

        public Notification GetById(uint id) => _context.Notifications.FirstOrDefault(u => u.NotificationId == id);

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Create(Notification value)
        {
            _context.Notifications.Add(value);
        }
        public void Delete(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }

        public IEnumerable<Notification> GetByUserEmail(string email)
        {
            return _context.Notifications.Where(n => n.ReceiverEmail == email).ToList();
        }
        public void ChangeToSeen(Notification notification)
        {
            notification.Read = true;
            _context.Notifications.Update(notification);
        }

    }
}
