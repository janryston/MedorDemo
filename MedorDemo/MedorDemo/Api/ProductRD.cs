using MedorDemo.Data;

namespace MedorDemo.Api
{
    public sealed record ProductRD(
        long Id, 
        string Name, 
        IReadOnlyList<CategoryRD> Categories
        );
}
