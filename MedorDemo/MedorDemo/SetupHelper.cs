using MedorDemo.Api;
using MedorDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace MedorDemo
{
    public class SetupHelper
    {
        public static async Task InitializeDatabaseAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

            var databaseWasCreated = await dbContext.Database.EnsureCreatedAsync();

            if (databaseWasCreated)
            {
                TestSetup(app);
            }
            else
            {
                try
                {
                    await dbContext.Products.AnyAsync();
                    await dbContext.Categories.AnyAsync();
                    await dbContext.Clients.AnyAsync();
                }
                catch (Microsoft.Data.Sqlite.SqliteException ex)
                    when (ex.SqliteErrorCode == 1)
                {
                    throw new InvalidOperationException(
                        "The existing SQLite database has an outdated schema. " +
                        "The database was preserved. Apply an EF Core migration " +
                        "or recreate the development database manually.",
                        ex);
                }
            }
        }

        public static async void TestSetup(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                await dbContext.Database.EnsureCreatedAsync();

                var category1 = new Category
                {
                    Name = "Sample category 1"
                };

                var category2 = new Category
                {
                    Name = "Sample category 2"
                };

                var product1 = new Product
                {
                    Name = "Sample product 1"
                };

                var product2 = new Product
                {
                    Name = "Sample product 2"
                };

                var product3 = new Product
                {
                    Name = "Complex product 3"
                };

                var client1 = new Client
                {
                    Name = "Sample client 1"
                };

                var client2 = new Client
                {
                    Name = "Sample client 2"
                };

                var stock1 = new ClientProductStock
                {
                    Client = client1,
                    Product = product1,
                    Quantity = 101
                };

                var stock2 = new ClientProductStock
                {
                    Client = client1,
                    Product = product3,
                    Quantity = 103
                };

                var stock3 = new ClientProductStock
                {
                    Client = client2,
                    Product = product1,
                    Quantity = 201
                };

                var stock4 = new ClientProductStock
                {
                    Client = client2,
                    Product = product2,
                    Quantity = 202
                };

                var stock5 = new ClientProductStock
                {
                    Client = client2,
                    Product = product3,
                    Quantity = 203
                };

                product1.Categories.Add(category1);
                product2.Categories.Add(category2);
                product3.Categories.Add(category1);
                product3.Categories.Add(category2);

                client1.Categories.Add(category1);

                client2.Categories.Add(category1);
                client2.Categories.Add(category2);

                dbContext.AddRange(
                    category1,
                    category2,
                    product1,
                    product2,
                    product3,
                    client1,
                    client2,
                    stock1,
                    stock2,
                    stock3,
                    stock4,
                    stock5);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
