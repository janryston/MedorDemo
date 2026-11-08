namespace MedorDemo.Data;

public class ClientCategory
{
    public long ClientId { get; set; }

    public Client Client { get; set; } = null!;

    public long CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}