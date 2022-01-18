using ManageProperties.Infrastructure.Context;
using ManageProperties.Infrastructure.Contracts;
using ManageProperties.Infrastructure.Entities;

namespace ManageProperties.Infrastructure.Repositories
{
    public class DBManagePropertiesRepository : IDBManagePropertiesRepository
    {
        public IGenericRepository<OwnerEntity, OwnerContract, ManagePropertiesContext> Owner { get; private set; }
        public IGenericRepository<PropertyEntity, PropertyContract, ManagePropertiesContext> Property { get; private set; }
        public IGenericRepository<PropertyImageEntity, PropertyImageContract, ManagePropertiesContext> PropertyImage { get; private set; }
        public IGenericRepository<PropertyTraceEntity, PropertyTraceContract, ManagePropertiesContext> PropertyTrace { get; private set; }

        public DBManagePropertiesRepository(
            IGenericRepository<OwnerEntity, OwnerContract, ManagePropertiesContext> _Owner,
            IGenericRepository<PropertyEntity, PropertyContract, ManagePropertiesContext> _Property,
            IGenericRepository<PropertyImageEntity, PropertyImageContract, ManagePropertiesContext> _PropertyImage,
            IGenericRepository<PropertyTraceEntity, PropertyTraceContract, ManagePropertiesContext> _PropertyTrace
        )
        {
            Owner = _Owner;
            Property = _Property;
            PropertyImage = _PropertyImage;
            PropertyTrace = _PropertyTrace;
        }
    }
}
