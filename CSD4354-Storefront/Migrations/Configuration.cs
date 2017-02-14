namespace CSD4354_Storefront.Migrations
{
    using System;
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSD4354_Storefront.DAL.StoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CSD4354_Storefront.DAL.StoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Products.AddOrUpdate(p => new { p.ProductName, p.Description, p.OnHandQuantity },
                new Product { ProductName = "T-Shirt", Description = "Plain White Tee", OnHandQuantity = 400 },
                new Product { ProductName = "Sweat Shirt", Description = "Rugged Hoodie", OnHandQuantity = 300 },
                new Product { ProductName = "Under Shirt", Description = "Flimsy Covering", OnHandQuantity = 600 }
                );
        }
    }
}
