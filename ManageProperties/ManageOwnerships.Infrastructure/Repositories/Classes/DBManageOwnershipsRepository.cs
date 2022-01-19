using ManageOwnerships.Infrastructure.Context;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Entities;

namespace ManageOwnerships.Infrastructure.Repositories
{
    public class DBManageOwnershipsRepository : IDBManageOwnershipsRepository
    {
        public IGenericRepository<OwnerEntity, OwnerContract, ManageOwnershipContext> Owner { get; private set; }
        public IGenericRepository<OwnershipEntity, OwnershipContract, ManageOwnershipContext> Ownership { get; private set; }
        public IGenericRepository<OwnershipImageEntity, OwnershipImageContract, ManageOwnershipContext> OwnershipImage { get; private set; }
        public IGenericRepository<OwnershipTraceEntity, OwnershipTraceContract, ManageOwnershipContext> OwnershipTrace { get; private set; }

        public DBManageOwnershipsRepository(
            IGenericRepository<OwnerEntity, OwnerContract, ManageOwnershipContext> _Owner,
            IGenericRepository<OwnershipEntity, OwnershipContract, ManageOwnershipContext> _Ownership,
            IGenericRepository<OwnershipImageEntity, OwnershipImageContract, ManageOwnershipContext> _OwnershipImage,
            IGenericRepository<OwnershipTraceEntity, OwnershipTraceContract, ManageOwnershipContext> _OwnershipTrace
        )
        {
            Owner = _Owner;
            Ownership = _Ownership;
            OwnershipImage = _OwnershipImage;
            OwnershipTrace = _OwnershipTrace;
        }
    }
}
