using MedorDemo.Api;
using MedorDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace MedorDemo
{
    public class ApiMapper
    {
        public static void MapApi(WebApplication app)
        {
            MapGetClients(app);

            MapGetProducts(app);

            MapGetInventory(app);
            MapPutInventory(app);
        }

        private static void MapGetProducts(WebApplication app)
        {
            app.MapGet("/api/products", async (AppDbContext dbContext) =>
            {
                var products = await dbContext.Products
                    .AsNoTracking()
                    .OrderBy(product => product.Name)
                    .Select(product => new ProductRD(
                        product.Id,
                        product.Name,
                        product.Categories.Select(category => new CategoryRD(
                                category.Id,
                                category.Name))
                .ToList())).ToListAsync();

                return Results.Ok(products);
            });
        }

        private static void MapGetClients(WebApplication app)
        {
            app.MapGet("/api/clients", async (AppDbContext dbContext) =>
            {
                var clients = await dbContext.Clients
                    .AsNoTracking()
                    .OrderBy(client => client.Name)
                    .Select(client => new ClientRD(
                        client.Id,
                        client.Name,
                        client.Categories
                            .Select(category => new CategoryRD(
                                category.Id,
                                category.Name))
                            .ToList()))
                    .ToListAsync();

                return Results.Ok(clients);
            });

        }

        private static void MapGetInventory(WebApplication app)
        {
            app.MapGet("/api/inventory", async (AppDbContext dbContext) =>
            {
                var inventory = await dbContext.ClientProductStocks
                    .AsNoTracking()
                    .OrderBy(stock => stock.Client.Name)
                    .ThenBy(stock => stock.Product.Name)
                    .Select(stock => new InventoryRowRD(
                        stock.ClientId,
                        stock.Client.Name,
                        stock.ProductId,
                        stock.Product.Name,
                        stock.Product.Categories
                            .Select(category => new CategoryRD(
                                category.Id,
                                category.Name))
                            .ToList(),
                        stock.Quantity))
                    .ToListAsync();

                return Results.Ok(inventory);
            });

        }

        private static void MapPutInventory(WebApplication app)
        {
            app.MapPut(
                "/api/inventory/{clientId:long}/{productId:long}",
                async Task<IResult> (
                long clientId,
                long productId,
                QuantityUpdateDC request,
                AppDbContext dbContext) =>
                {
                if (request.Quantity < 0)
                {
                return Results.BadRequest(new
                {
                    error = "Quantity cannot be negative."
                });
                }

                var stock = await dbContext.ClientProductStocks
                .SingleOrDefaultAsync(item =>
                    item.ClientId == clientId &&
                    item.ProductId == productId);

                if (stock is null)
                {
                return Results.NotFound();
                }

                stock.Quantity = request.Quantity;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
