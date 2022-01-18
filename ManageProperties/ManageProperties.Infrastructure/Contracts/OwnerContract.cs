using System;

namespace ManageProperties.Infrastructure.Contracts
{
    public class OwnerContract
    {
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
