namespace MedorDemo.Data
{
    public class Product : DataBaseEntity
    {

        public string Name { get; set; } = String.Empty;

        public ICollection<Category> Categories { get; set; } = [];

        public ICollection<ClientProductStock> ClientStocks { get; set; } = [];
    }
}
