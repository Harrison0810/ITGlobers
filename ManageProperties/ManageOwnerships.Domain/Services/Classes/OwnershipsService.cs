using AutoMapper;
using ManageOwnerships.Domain.Models;
using ManageOwnerships.Infrastructure.Classes;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageOwnerships.Domain.Services
{
    public class OwnershipsService : IOwnershipsService
    {
        private readonly IDBManageOwnershipsRepository _dbManageOwnershipsRepository;
        private readonly IErrorLoggingClient _errorLoggingClient;
        private readonly IMapper _mapper;

        public OwnershipsService(
            IDBManageOwnershipsRepository dbManageOwnershipsRepository,
            IErrorLoggingClient errorLoggingClient,
            IMapper mapper
        )
        {
            _dbManageOwnershipsRepository = dbManageOwnershipsRepository;
            _errorLoggingClient = errorLoggingClient;
            _mapper = mapper;
        }

        async Task<MessageModel<OwnershipModel>> IOwnershipsService.AddOwnership(OwnershipModel ownershipModel)
        {
            MessageModel<OwnershipModel> message = new MessageModel<OwnershipModel>();

            try
            {
                // Map Ownerships
                OwnershipContract ownershipContract = _mapper.Map<OwnershipModel, OwnershipContract>(ownershipModel);

                // Add Ownership
                ownershipContract = _dbManageOwnershipsRepository.Ownership.Add(ownershipContract);

                foreach (OwnershipImageModel ownershipImage in ownershipModel.OwnershipImages)
                {
                    OwnershipImageContract ownershipImageContract = new OwnershipImageContract
                    {
                        OwnershipId = ownershipImage.OwnershipId,
                        Enabled = true,
                        File = ownershipImage.File
                    };

                    // Add ownership image
                    _dbManageOwnershipsRepository.OwnershipImage.Add(ownershipImageContract);
                }

                message.Data = _mapper.Map<OwnershipContract, OwnershipModel>(ownershipContract);
                message.Status = true;
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<OwnershipModel> IOwnershipsService.GetOwnership(int ownershipId, int ownerId)
        {
            MessageModel<OwnershipModel> message = new MessageModel<OwnershipModel>();

            try
            {
                // Find owner from owner id
                OwnershipContract ownershipContract = _dbManageOwnershipsRepository.Ownership.FindOne(x => x.OwnerId == ownershipId);

                if (ownershipContract is not null)
                {
                    message.Data = _mapper.Map<OwnershipContract, OwnershipModel>(ownershipContract);
                    message.Status = true;
                }
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<List<OwnershipModel>> IOwnershipsService.GetAllOwnerships(int ownerId)
        {
            MessageModel<List<OwnershipModel>> message = new MessageModel<List<OwnershipModel>>();

            try
            {
                // Find owner's ownerships 
                List<OwnershipContract> ownerships = _dbManageOwnershipsRepository.Ownership.FindBy(x => x.OwnerId == ownerId);
                if (ownerships is not null && ownerships.Count > 0)
                {
                    message.Data = _mapper.Map<List<OwnershipContract>, List<OwnershipModel>>(ownerships);
                    message.Status = true;
                }
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        MessageModel<OwnershipModel> IOwnershipsService.UpdateOwnership(OwnershipModel ownershipModel)
        {
            MessageModel<OwnershipModel> message = new MessageModel<OwnershipModel>();

            try
            {
                // Update ownership, from id
                OwnershipContract ownerContract = _mapper.Map<OwnershipModel, OwnershipContract>(ownershipModel);
                _dbManageOwnershipsRepository.Ownership.Edit(x => x.OwnershipId == ownerContract.OwnershipId && x.OwnerId == ownerContract.OwnerId, ownerContract, (entity, contract) =>
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

        MessageModel<OwnershipModel> IOwnershipsService.DeleteOwnership(int ownershipId, int ownerId)
        {
            MessageModel<OwnershipModel> message = new MessageModel<OwnershipModel>();

            try
            {
                // Delete ownership, from ownership id and owner id
                message.Status = _dbManageOwnershipsRepository.Ownership.Delete(x => x.OwnershipId == ownershipId && x.OwnerId == ownerId);
            }
            catch (Exception Ex)
            {
                message.Message = _errorLoggingClient.Report(Ex);
            }

            return message;
        }
    }
}
