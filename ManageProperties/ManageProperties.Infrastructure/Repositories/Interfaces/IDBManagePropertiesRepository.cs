using ManageProperties.Infrastructure.Context;
using ManageProperties.Infrastructure.Contracts;
using ManageProperties.Infrastructure.Entities;

namespace ManageProperties.Infrastructure.Repositories
{
    public interface IDBManagePropertiesRepository
    {
        IGenericRepository<OwnerEntity, OwnerContract, ManagePropertiesContext> Owner { get; }
        IGenericRepository<PropertyEntity, PropertyContract, ManagePropertiesContext> Property { get; }
        IGenericRepository<PropertyImageEntity, PropertyImageContract, ManagePropertiesContext> PropertyImage { get; }
        IGenericRepository<PropertyTraceEntity, PropertyTraceContract, ManagePropertiesContext> PropertyTrace { get; }
    }
}
