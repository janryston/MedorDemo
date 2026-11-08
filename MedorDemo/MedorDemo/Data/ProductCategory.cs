namespace MedorDemo.Data
{
    /// <summary>
    /// n-n Entity relation for <see cref="Product"/> and <see cref="Category"/>
    /// </summary>

    public class ProductCategory : DataBaseEntity
    {
        public long ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public long CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
