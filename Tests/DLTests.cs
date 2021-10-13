using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebUI.Controllers;
using StoreBL;
using Models;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using DL;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class DLTests
    {
        private readonly DbContextOptions<StoreDBContext> options;

        public DLTests()
        {
            options = new DbContextOptionsBuilder<StoreDBContext>().UseSqlite("Filename= Test.db").Options;
            Seed();
        }

        //Testing Read operation
        [Fact]
        public void GetAllCustomersShouldGetAllCustomer()
        {
            using var context = new StoreDBContext(options);
            //ARRANGE
            IRepo repo = new DBRepo(context);

            //ACT
            var customers = repo.GetAllCustomers();

            //ASSERT
            Assert.NotNull(customers);
            Assert.Equal(2, customers.Count);
        }


        [Fact]
        public void AddingCustomerShouldAddACustomer()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                Customer custToAdd = new Customer()
                {
                    Id = 3,
                    Name = "Tenzin",
                    Address = "234 City",
                    Email = "hr@net.com"
                };

                repo.AddCustomer(custToAdd);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                Customer custo = context.Customers.FirstOrDefault(c => c.Id == 3);

                Assert.NotNull(custo);
                Assert.Equal("Tenzin", custo.Name);
                Assert.Equal("234 City", custo.Address);
                Assert.Equal("hr@net.com", custo.Email);

            }

        }
        [Fact]
        public void AddingStoreShouldAddAStore()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                StoreFront storeToAdd = new StoreFront()
                {
                    Id = 3,
                    Name = "SLS1",
                    Address = "234 City",
                };

                repo.AddStore(storeToAdd);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                StoreFront store = context.StoreFronts.FirstOrDefault(c => c.Id == 3);

                Assert.NotNull(store);
                Assert.Equal("SLS1", store.Name);
                Assert.Equal("234 City", store.Address);

            }

        }
        [Fact]
        public void AddingAProductShouldAddAProduct()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                Product productToAdd = new Product()
                {
                    Id = 4,
                    Name = "Chair",
                    Price = 99,
                };

                repo.AddProduct(productToAdd);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                Product product = context.Products.FirstOrDefault(c => c.Id == 3);

                Assert.NotNull(product);
                Assert.Equal("Chair", product.Name);
                Assert.Equal(99, product.Price);

            }

        }
        [Fact]
        public void GetAllProductsShouldGetAllProducts()
        {
            using var context = new StoreDBContext(options);
            //ARRANGE
            IRepo repo = new DBRepo(context);

            //ACT
            var products = repo.GetAllProducts();

            //ASSERT
            Assert.NotNull(products);
            Assert.Single(products);
        }

        [Fact]
        public void UpdatingACustomerShouldUpdateACustomer()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                Customer custToAdd = new Customer()
                {
                    Id = 6,
                    Name = "Tenzin",
                    Address = "234 City",
                    Email = "hr@net.com"
                };

                repo.AddCustomer(custToAdd);

                Customer updateCustomer = new Customer()
                {
                    Id = 6,
                    Name = "Woesel",
                    Address = "Manhattan",
                    Email = "ten@gmail.com"
                };

                repo.UpdateCustomer(updateCustomer);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                Customer custo = context.Customers.FirstOrDefault(c => c.Id == 6);

                Assert.NotNull(custo);
                Assert.Equal("Woesel", custo.Name);
                Assert.NotEqual("234 City", custo.Address);
                Assert.NotEqual("hr@net.com", custo.Email);

            }

        }

        [Fact]
        public void UpdatingAProductShouldUpdateAProduct()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                Product productToAdd = new Product()
                {
                    Id = 5,
                    Name = "Phone",
                    Price = 500,
                };

                productToAdd = repo.AddProduct(productToAdd);

                Product productToUpdate = new Product()
                {
                    Id = 5,
                    Name = "Mobile Phone",
                    Price = 100,
                };

                productToUpdate = repo.UpdateProduct(productToUpdate);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                Product product = context.Products.FirstOrDefault(c => c.Id == 5);

                Assert.NotNull(product);
                Assert.Equal("Mobile Phone", product.Name);
                Assert.Equal(100, product.Price);

            }

        }

        [Fact]
        public void GetCustomerByNameShouldReturnACustomer()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                Customer custToGet = new Customer()
                {
                    Id = 1,
                    Name = "Bone Fish",
                    Address = "Lakeland",
                    Email = "ten@gmail.com"
                };

                repo.GetCustomer(custToGet.Name);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                Customer custo = context.Customers.FirstOrDefault(c => c.Name == "Bone Fish");

                Assert.NotNull(custo);
                Assert.Equal("Bone Fish", custo.Name);
                Assert.NotEqual("234 City", custo.Address);
                Assert.Equal("ten@gmail.com", custo.Email);

            }

        }

        [Fact]
        public void GetProductByIdShouldReturnAProduct()
        {
            using (var context = new StoreDBContext(options))
            {
                IRepo repo = new DBRepo(context);

                Product productToGet = new Product()
                {
                    Id = 3,
                    Name = "Chair",
                    Price = 99
                };

                repo.GetProductById(productToGet.Id);
            }

            using (var context = new StoreDBContext(options))
            {
                //ASSERT
                Product product = context.Products.FirstOrDefault(c => c.Id == 3);

                Assert.NotNull(product);
                Assert.Equal("Chair", product.Name);
                Assert.Equal(99 , product.Price);

            }

        }



        private void Seed()
        {
            using (var context = new StoreDBContext(options))
            {
                //first, we are going to make sure
                //that the DB is in clean slate
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.AddRange(
                    new Customer()
                    {
                        Id = 1,
                        Name = "Bone Fish",
                        Address = "Lakeland",
                        Email = "ten@gmail.com"
                    },
                    new Customer()
                    {
                        Id = 2,
                        Name = "Grumpy Monk",
                        Address = "Myrtle Beach",
                        Email = "yahoo@gmail.com"
                    });

                context.Products.AddRange(
                    new Product()
                    {
                        Id = 3,
                        Name = "Chair",
                        Price = 99,
                    });

                context.SaveChanges();
                context.ChangeTracker.Clear();
            }
        }
    }
}