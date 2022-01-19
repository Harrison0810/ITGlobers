using ManageOwnerships.Domain.Models;
using ManageOwnerships.Domain.Services.Classes;
using ManageOwnerships.Infrastructure.Classes;
using ManageOwnerships.Infrastructure.Contracts;
using ManageOwnerships.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ManageOwnerships.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IDBManageOwnershipsRepository _dbManageOwnershipsRepository;
        private readonly IErrorLoggingClient _errorLoggingClient;

        public UserService(
            IConfiguration configuration,
            IErrorLoggingClient errorLoggingClient,
            IDBManageOwnershipsRepository dbManageOwnershipsRepository
        )
        {
            _configuration = configuration;
            _errorLoggingClient = errorLoggingClient;
            _dbManageOwnershipsRepository = dbManageOwnershipsRepository;
        }

        MessageModel<string> IUserService.Authenticate(AuthModel authModel)
        {
            MessageModel<string> message = new MessageModel<string>();

            try
            {
                authModel.Password = EncodeHelper.EncodePassword(authModel.Password, _configuration.GetSection("Encription")["Salt"]);
                OwnerContract ownerContract = _dbManageOwnershipsRepository.Owner.FindOne(x => x.Name == authModel.Username && x.Password == authModel.Password);

                if (ownerContract is not null)
                {
                    message.Data = GenerateJwtToken(ownerContract);
                    message.Status = true;
                }
            }
            catch (Exception Ex)
            {
                _errorLoggingClient.Report(Ex);
            }

            return message;
        }

        private string GenerateJwtToken(OwnerContract ownerContract)
        {
            string configKey = _configuration.GetValue<string>("SecretKey");

            // Generate token that is valid for 1 hour
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(configKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("OwnerId", ownerContract.OwnerId.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
