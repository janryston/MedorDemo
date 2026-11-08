namespace MedorDemo.Data
{
    public class Product : DataBaseEntity
    {
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// can belong to 0-n categories
        /// </summary>
        public ICollection<Category> Categories { get; set; } = [];

        public ICollection<ClientProductStock> ClientStocks { get; set; } = [];
    }
}
