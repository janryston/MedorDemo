namespace MedorDemo.Api;

public sealed record InventoryRowRD(
    long ClientId,
    string ClientName,
    long ProductId,
    string ProductName,
    IReadOnlyList<CategoryRD> Categories,
    int Quantity);