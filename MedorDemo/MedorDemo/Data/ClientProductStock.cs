namespace MedorDemo.Data;

public class ClientProductStock
{
    public long ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public long ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
}