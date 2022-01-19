using ManageOwnerships.Infrastructure.Context;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Entities;

namespace ManageOwnerships.Infrastructure.Repositories
{
    public interface IDBManageOwnershipsRepository
    {
        IGenericRepository<OwnerEntity, OwnerContract, ManageOwnershipContext> Owner { get; }
        IGenericRepository<OwnershipEntity, OwnershipContract, ManageOwnershipContext> Ownership { get; }
        IGenericRepository<OwnershipImageEntity, OwnershipImageContract, ManageOwnershipContext> OwnershipImage { get; }
        IGenericRepository<OwnershipTraceEntity, OwnershipTraceContract, ManageOwnershipContext> OwnershipTrace { get; }
    }
}
