using AutoMapper;
using ManageProperties.Domain.Models;
using ManageProperties.Domain.Services.Classes;
using ManageProperties.Infrastructure.Classes;
using ManageProperties.Infrastructure.Contracts;
using ManageProperties.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;

namespace ManageProperties.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IConfiguration _configuration;
        private readonly IDBManagePropertiesRepository _dbManagePropertiesRepository;
        private readonly IErrorLoggingClient _errorLoggingClient;
        private readonly IMapper _mapper;

        public OwnerService(
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

        MessageModel<OwnerModel> IOwnerService.AddOwner(OwnerModel ownerModel)
        {
            MessageModel<OwnerModel> message = new MessageModel<OwnerModel>();

            try
            {
                // Encript Password
                OwnerContract ownerContract = _mapper.Map<OwnerContract>(ownerModel);
                ownerContract.Password = EncodeService.EncodePassword(ownerModel.Password, _configuration.GetSection("Encription")["Salt"]);

                // Add owner
                ownerContract = _dbManagePropertiesRepository.Owner.Add(ownerContract);

                message.Data = _mapper.Map<OwnerContract, OwnerModel>(ownerContract);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<OwnerModel> IOwnerService.GetOwner(int ownerId)
        {
            MessageModel<OwnerModel> message = new MessageModel<OwnerModel>();

            try
            {
                // Find owner from owner id
                OwnerContract ownerContract = _dbManagePropertiesRepository.Owner.FindOne(x => x.IdOwner == ownerId);

                if (ownerContract is not null)
                {
                    message.Data = _mapper.Map<OwnerContract, OwnerModel>(ownerContract);
                    message.Status = true;
                }
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<OwnerModel> IOwnerService.UpdateOwner(OwnerModel ownerModel)
        {
            MessageModel<OwnerModel> message = new MessageModel<OwnerModel>();

            try
            {
                // Update owner properties, from owner id
                OwnerContract ownerContract = _mapper.Map<OwnerModel, OwnerContract>(ownerModel);
                _dbManagePropertiesRepository.Owner.Edit(x => x.IdOwner == ownerContract.IdOwner, ownerContract, (entity, contract) =>
                    {
                        entity.Name = contract.Name;
                        entity.Address = contract.Address;
                        entity.Photo = contract.Photo;
                        entity.Birthday = contract.Birthday;
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

        MessageModel<string> IOwnerService.DeleteOwner(int ownerId)
        {
            MessageModel<string> message = new MessageModel<string>();

            try
            {
                // Delete owner, from owner id
                message.Status = _dbManagePropertiesRepository.Owner.Delete(x => x.IdOwner == ownerId);
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }
    }
}
