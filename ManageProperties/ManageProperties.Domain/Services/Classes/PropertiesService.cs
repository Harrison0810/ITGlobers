using AutoMapper;
using ManageProperties.Domain.Models;
using ManageProperties.Domain.Services.Classes;
using ManageProperties.Infrastructure.Classes;
using ManageProperties.Infrastructure.Contracts;
using ManageProperties.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ManageProperties.Domain.Services
{
    public class PropertiesService : IPropertiesService
    {
        private readonly IConfiguration _configuration;
        private readonly IDBManagePropertiesRepository _dbManagePropertiesRepository;
        private readonly IErrorLoggingClient _errorLoggingClient;
        private readonly IMapper _mapper;

        public PropertiesService(
            IConfiguration configuration,
            IDBManagePropertiesRepository dbManagePropertiesRepository,
            IErrorLoggingClient errorLoggingClient,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _dbManagePropertiesRepository = dbManagePropertiesRepository;
            _errorLoggingClient = errorLoggingClient;
            _mapper = mapper;
        }

        MessageModel<PropertyModel> IPropertiesService.AddProperty(PropertyModel propertyModel)
        {
            MessageModel<PropertyModel> message = new MessageModel<PropertyModel>();

            try
            {
                // Encript Password
                PropertyContract propertyContract = _mapper.Map<PropertyModel, PropertyContract>(propertyModel);

                // Add owner
                propertyContract = _dbManagePropertiesRepository.Property.Add(propertyContract);

                message.Data = _mapper.Map<PropertyContract, PropertyModel>(propertyContract);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<PropertyModel> IPropertiesService.GetProperty(int propertyId, int ownerId)
        {
            MessageModel<PropertyModel> message = new MessageModel<PropertyModel>();

            try
            {
                // Find owner from owner id
                PropertyContract propertyContract = _dbManagePropertiesRepository.Property.FindOne(x => x.IdOwner == propertyId);

                if (propertyContract is not null)
                {
                    message.Data = _mapper.Map<PropertyContract, PropertyModel>(propertyContract);
                    message.Status = true;
                }
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<List<PropertyModel>> IPropertiesService.GetAllProperties(int ownerId)
        {
            MessageModel<List<PropertyModel>> message = new MessageModel<List<PropertyModel>>();

            try
            {
                // Find owner's properties 
                List<PropertyContract> properties = _dbManagePropertiesRepository.Property.FindBy(x => x.IdOwner == ownerId);
                if (properties is not null && properties.Count > 0)
                {
                    message.Data = _mapper.Map<List<PropertyContract>, List<PropertyModel>>(properties);
                    message.Status = true;
                }
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<PropertyModel> IPropertiesService.UpdateProperty(PropertyModel propertyModel)
        {
            MessageModel<PropertyModel> message = new MessageModel<PropertyModel>();

            try
            {
                // Update property, from id
                PropertyContract ownerContract = _mapper.Map<PropertyModel, PropertyContract>(propertyModel);
                _dbManagePropertiesRepository.Property.Edit(x => x.IdProperty == ownerContract.IdProperty && x.IdOwner == ownerContract.IdOwner, ownerContract, (entity, contract) =>
                {
                    entity.Name = contract.Name;
                    entity.Address = contract.Address;
                    entity.Price = contract.Price;
                    entity.CodeInternal = contract.CodeInternal;
                    entity.Year = contract.Year;
                    return entity;
                });
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<PropertyModel> IPropertiesService.DeleteProperty(int propertyId, int ownerId)
        {
            MessageModel<PropertyModel> message = new MessageModel<PropertyModel>();

            try
            {
                // Delete property, from property id and owner id
                message.Status = _dbManagePropertiesRepository.Property.Delete(x => x.IdProperty == propertyId && x.IdOwner == ownerId);
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }
    }
}
