namespace ManageProperties.Infrastructure.Contracts
{
    public class PropertyImageContract
    {
        public int IdPropertyImagen { get; set; }
        public int IdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
