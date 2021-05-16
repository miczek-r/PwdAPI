using ZarzadzanieDomem.Authentication;
using ZarzadzanieDomem.Models;

namespace ZarzadzanieDomem.IRepositories
{
    public interface IAuthorizeRepository
    {
        User GetUserByEmail(Auth auth);
        public void SendVerificationEmail(User user, string token);
        public string TokenGenerator(User user);
        public void SendRestorationEmail(User user, string token);
        public string EncodePassword(string password);
        public string DecodeFrom64(string encodedData);
        public User GetUserByRestorationToken(string token);
        public User GetUserByActivationToken(string token);
    }
}
