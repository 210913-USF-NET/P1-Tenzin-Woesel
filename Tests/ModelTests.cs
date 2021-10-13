using System;
using Xunit;
using Models;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CustomerShouldBeValidName()
        {
            //Arrange
            Customer customer = new Customer();
            string testName = "Tenzin";

            //Act
            customer.Name = testName;

            //Assert
            Assert.Equal(testName, customer.Name);

        }

        [Fact]
        public void ProductNameShouldBeValid()
        {
            Product product = new Product();
            string testProdName = "Chair";

            //Act
            product.Name = testProdName;

            Assert.Equal(testProdName, product.Name);
        }


        [Fact]
        public void CustomerShouldBeCreated()
        {

            Customer customer = new Customer();
            customer.Name = "Tenzin";
            customer.Address = "Queens";

            Assert.NotNull(customer);

        }

        [Fact]
        public void CustomerShouldHaveListOfOrder()
        {
            Customer customer = new Customer();

            Assert.True(customer.Orders != null);
        }

        [Fact]
        public void ProductShouldBeInstantiated()
        {
            Product product = new Product();

            Assert.NotNull(product);
        }

        [Fact]
        public void StoreShouldBeInstantiated()
        {
            StoreFront store = new StoreFront();

            Assert.NotNull(store);
        }

        [Fact]
        public void OrderShouldBeInstantiated()
        {
            Order order = new Order();
            Assert.NotNull(order);
        }

        [Fact]
        public void InventoryShouldHaveQuantity()
        {
            Inventory inventory = new Inventory();
            int quantity = 12;

            inventory.Quantity = quantity;

            Assert.Equal(quantity, inventory.Quantity);
            
        }
    }
}
