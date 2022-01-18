using System;

namespace ManageProperties.Domain.Models
{
    public class OwnerModel
    {
        public int IdOwner { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
