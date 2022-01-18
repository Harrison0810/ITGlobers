using ManageProperties.Domain.Models;
using System.Collections.Generic;

namespace ManageProperties.Domain.Services
{
    public interface IPropertiesService
    {
        MessageModel<PropertyModel> AddProperty(PropertyModel propertyModel);
        MessageModel<PropertyModel> GetProperty(int propertyId, int ownerId);
        MessageModel<List<PropertyModel>> GetAllProperties(int ownerId);
        MessageModel<PropertyModel> UpdateProperty(PropertyModel propertyModel);
        MessageModel<PropertyModel> DeleteProperty(int propertyId, int ownerId);
    }
}
