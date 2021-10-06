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

        [Theory]
        [InlineData("")]
        public void CustomerShouldNotAllowInvalidName(string name)
        {
            //Arrange
            Customer test = new Customer();

            //Act & Assert
            //When I try to set the restaurant's name to an invalid data
            //We make sure that the program throws input invalid exception
            Assert.Throws<InputInvalidException>(() => test.Email = name);
        }

        [Theory]
        [InlineData("")]
        public void CustomerShouldNotAllowInvalidEmail(string email)
        {
            //Arrange
            Customer test = new Customer();

            //Act & Assert
            //When I try to set the restaurant's name to an invalid data
            //We make sure that the program throws input invalid exception
            Assert.Throws<InputInvalidException>(() => test.Email = email);
        }

        // [Fact]
        // [InlineData("")]
        // public void 

        
    }
}
