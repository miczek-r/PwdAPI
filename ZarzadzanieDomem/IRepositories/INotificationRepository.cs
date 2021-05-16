using System.Collections.Generic;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface INotificationRepository
    {
        public IEnumerable<Notification> GetAll();
        public IEnumerable<Notification> GetByUserEmail(string email);
        Notification GetById(uint id);
        void Save();
        void Create(Notification value);
        void Delete(Notification notification);
        void ChangeToSeen(Notification notification);



    }
}
