namespace MedorDemo.Data
{
    public class Category : DataBaseEntity
    {
        public string Name { get; set; } = String.Empty;

        public ICollection<Product> Products { get; set; } = [];

        public ICollection<Client> Clients { get; set; } = [];
    }
}
