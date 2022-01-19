using ManageOwnerships.Domain.Models;

namespace ManageOwnerships.Domain.Services
{
    public interface IUserService
    {
        MessageModel<string> Authenticate(AuthModel authModel);
    }
}
