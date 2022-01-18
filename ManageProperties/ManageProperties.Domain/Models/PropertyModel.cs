namespace ManageProperties.Domain.Models
{
    public class PropertyModel
    {
        public int IdProperty { get; private set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CodeInternal { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
    }
}
