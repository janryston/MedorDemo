namespace MedorDemo.Data;

public class Client : DataBaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Category> Categories { get; set; } = [];

    public ICollection<ClientProductStock> ProductStocks { get; set; } = [];
}