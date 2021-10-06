using System.Collections.Generic;
using System.Linq;
using DL;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;
using Entity = DL.Entities;

namespace Tests
{
    public class DLTests
    {
        private readonly DbContextOptions<Entity.P0TenzinStoreContext> options;

        public DLTests()
        {
            options = new DbContextOptionsBuilder<Entity.P0TenzinStoreContext>().UseSqlite("Filename= Test.db").Options;
            Seed();
        }

        //Testing Read operation
        [Fact]
        public void GetAllCustomersShouldGetAllCustomer()
        {
            using(var context = new Entity.P0TenzinStoreContext(options))
            {
                //ARRANGE
                ICustomerRepo repo = new DBCustomerRepo(context);

                //ACT
                var customers = repo.GetAllCustomers();

                //ASSERT
                Assert.Equal(1, customers.Count);
            }
        }

        
        [Fact]
        public void AddingCustomerShouldAddACustomer()
        {
            using (var context = new Entity.P0TenzinStoreContext(options))
            {
                ICustomerRepo repo = new DBCustomerRepo(context);

                Models.Customer custToAdd = new Models.Customer()
                {
                    Id = 1,
                    Name = "Tenzin",
                    Address = "234 City",
                    Email = "hr@net.com"
                };

                repo.AddCustomer(custToAdd);
            }

            using (var context = new Entity.P0TenzinStoreContext(options))
            {
                //ASSERT
                Entity.Customer custo = context.Customers.FirstOrDefault(c => c.Id ==1);

                Assert.NotNull(custo);
                Assert.Equal("Tenzin", custo.Name);
                Assert.Equal("234 City", custo.Address);
            }


        }
        // [Fact]
        // public void AddingOrderToACustomer()
        // {
        //     Models.Order orderToAdd;
        //     using(var context =  new Entity.P0TenzinStoreContext(options))
        //     {
                
        //         ICustomerRepo repo = new DBCustomerRepo(context);

        //         Models.Customer custToAdd = new Models.Customer()
        //         {
        //             Id = 1,
        //             Name = "Tenzin",
        //             Address = "234 City",
        //             Email = "hr@net.com"
        //         };

        //         custToAdd = repo.AddCustomer(custToAdd);

        //         Models.StoreFront storeFront = new Models.StoreFront()
        //         {
        //             Id = 1,
        //             Name = "SLS 2",
        //             Address = "123 NY"
        //         };

        //         storeFront = repo.AddStore(storeFront);
                
        //         Models.Product product = new Models.Product()
        //         {
        //             Id = 1,
        //             Name = "Sofa",
        //             Price = 100.99M,
        //             Description = "Head rest chair",
        //             Category = "Chair"
        //         };

        //         // product = repo.Ad

        //         Models.LineItems items = new Models.LineItems()
        //         {
        //             Id = 1,
        //             Quantity = 4,
        //             ProductId = product.Id
        //         };

        //         List<LineItems> lineItems = new List<LineItems>();
        //         lineItems.Add(items);


        //         orderToAdd = new Models.Order()
        //         {
        //             Id =1,
        //             Total = 100.99M,
        //             StoreFrontId = storeFront.Id,
        //             CustomerId = custToAdd.Id,
        //             LineItems = lineItems
        //         };

        //         orderToAdd = repo.AddAnOrder(orderToAdd);
        //     }

        //     using (var context = new Entity.P0TenzinStoreContext(options))
        //     {
        //         //ASSERT
        //         Entity.Customer custo = context.Customers.FirstOrDefault(c => c.Id == 1);

        //         Assert.NotNull(custo);
        //         Assert.Equal(custo.Name, "Tenzin");
        //         Assert.Equal(custo.Id, 1);
        //         Assert.Equal(custo.Address, "234 City");
        //         // Assert.Equal(custo.Orders.Contains(orderToAdd));
        //     }
        // }

        private void Seed()
        {
            using(var context = new Entity.P0TenzinStoreContext(options))
            {
                //first we are going to make sure the DB is in clean state
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                context.Customers.AddRange(new Entity.Customer(){
                    Id = 1,
                    Name = "Tenzin",
                    Address = "234 City",
                    Email = "hr@net.com"
                });

                context.SaveChanges();
            }
        }
    }
}