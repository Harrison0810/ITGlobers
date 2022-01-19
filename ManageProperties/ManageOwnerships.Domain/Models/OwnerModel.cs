using System;

namespace ManageOwnerships.Domain.Models
{
    public class OwnerModel
    {
        public int OwnerId { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
