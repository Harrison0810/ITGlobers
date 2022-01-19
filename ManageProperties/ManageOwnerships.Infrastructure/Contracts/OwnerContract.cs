using System;

namespace ManageOwnerships.Infrastructure.Contracts
{
    public class OwnerContract
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
