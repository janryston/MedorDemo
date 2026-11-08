namespace MedorDemo.Api
{
    public sealed record ClientRD(
        long Id,
        string Name,
        IReadOnlyList<CategoryRD> Categories
        );
}
