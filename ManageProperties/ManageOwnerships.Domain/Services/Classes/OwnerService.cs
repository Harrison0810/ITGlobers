using AutoMapper;
using ManageOwnerships.Domain.Models;
using ManageOwnerships.Domain.Services.Classes;
using ManageOwnerships.Infrastructure.Classes;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;

namespace ManageOwnerships.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IConfiguration _configuration;
        private readonly IDBManageOwnershipsRepository _dbManageOwnershipsRepository;
        private readonly IErrorLoggingClient _errorLoggingClient;
        private readonly IMapper _mapper;

        public OwnerService(
            IConfiguration configuration,
            IDBManageOwnershipsRepository dbManageOwnershipsRepository,
            IErrorLoggingClient errorLoggingClient,
            IMapper mapper
        )
        {
            _configuration = configuration;
            _dbManageOwnershipsRepository = dbManageOwnershipsRepository;
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
                ownerContract.Password = EncodeHelper.EncodePassword(ownerModel.Password, _configuration.GetSection("Encription")["Salt"]);

                // Add owner
                ownerContract = _dbManageOwnershipsRepository.Owner.Add(ownerContract);

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
                OwnerContract ownerContract = _dbManageOwnershipsRepository.Owner.FindOne(x => x.OwnerId == ownerId);

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
                // Update owner ownerships, from owner id
                OwnerContract ownerContract = _mapper.Map<OwnerModel, OwnerContract>(ownerModel);
                _dbManageOwnershipsRepository.Owner.Edit(x => x.OwnerId == ownerContract.OwnerId, ownerContract, (entity, contract) =>
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
                message.Status = _dbManageOwnershipsRepository.Owner.Delete(x => x.OwnerId == ownerId);
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }
    }
}
