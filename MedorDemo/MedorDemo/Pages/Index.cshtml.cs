using MedorDemo.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MedorDemo.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _dbContext;

 
    public IReadOnlyList<Product> Products { get; private set; } = [];

    public IndexModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task OnGetAsync()
    {
        Products = await _dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Name)
            .ToListAsync();
    }
}