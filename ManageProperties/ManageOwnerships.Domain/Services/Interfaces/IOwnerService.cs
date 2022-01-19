using ManageOwnerships.Domain.Models;

namespace ManageOwnerships.Domain.Services
{
    public interface IOwnerService
    {
        MessageModel<OwnerModel> AddOwner(OwnerModel ownerModel);
        MessageModel<OwnerModel> GetOwner(int ownerId);
        MessageModel<OwnerModel> UpdateOwner(OwnerModel ownerModel);
        MessageModel<string> DeleteOwner(int ownerId);
    }
}
