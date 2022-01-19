using ManageOwnerships.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageOwnerships.Domain.Services
{
    public interface IOwnershipsService
    {
        Task<MessageModel<OwnershipModel>> AddOwnership(OwnershipModel ownershipModel);
        MessageModel<OwnershipModel> GetOwnership(int ownershipId, int ownerId);
        MessageModel<List<OwnershipModel>> GetAllOwnerships(int ownerId);
        MessageModel<OwnershipModel> UpdateOwnership(OwnershipModel ownershipModel);
        MessageModel<OwnershipModel> DeleteOwnership(int ownershipId, int ownerId);
    }
}
