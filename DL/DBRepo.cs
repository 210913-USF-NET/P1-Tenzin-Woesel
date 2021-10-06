using System;
using System.Collections.Generic;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.Common;
using System.Threading.Tasks;

namespace DL
{
    public class DBRepo : IRepo
    {
        private readonly StoreDBContext _context;

        public DBRepo(StoreDBContext context)
        {
            _context = context;
        }
        public Order AddAnOrder(Order order)
        {
            Order orderToAdd = new Order()
            {
                Total = order.Total,
                CustomerId = order.CustomerId,
                StoreFrontId = order.StoreFrontId
            };

            orderToAdd = _context.Orders.Add(orderToAdd).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Order()
            {
                Id = orderToAdd.Id,
                Total = (decimal)orderToAdd.Total,
                CustomerId = orderToAdd.CustomerId,
                StoreFrontId = orderToAdd.StoreFrontId,
                OrderDate = (DateTime)orderToAdd.OrderDate
            };
        }

        public Customer AddCustomer(Customer customer)
        {
            customer = _context.Add(customer).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return customer;
        }
        public Product AddProduct(Product product)

        {
            Product productToAdd = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category
            };

            productToAdd = _context.Add(productToAdd).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Product
            {
                Id = productToAdd.Id,
                Name = productToAdd.Name,
                Price = (decimal)productToAdd.Price,
                Description = productToAdd.Description,
                Category = productToAdd.Category
            };
        }

        public void DeleteCustomer(string name)
        {
            Customer customerToDelete = GetCustomer(name);


            _context.Remove(customerToDelete);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.Select(customer => new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Email = customer.Email
            }).ToList();
        }

        public Customer GetCustomer(string name)
        {
            Customer customerByName = _context.Customers.FirstOrDefault(s => s.Name == name);

            if (customerByName == null)
            {
                return null;
            }
            else
            {
                Customer returnedCustomer = new Customer()
                {
                    Id = customerByName.Id,
                    Name = customerByName.Name,
                    Address = customerByName.Address,
                    Email = customerByName.Email
                };

                return returnedCustomer;

            }
        }

        public Customer UpdateCustomer(Customer customerToUpdate)
        {
            Customer updateCustomer = new Customer()
            {
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name,
                Address = customerToUpdate.Address,
                Email = customerToUpdate.Email
            };

            updateCustomer = _context.Customers.Update(updateCustomer).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Customer()
            {
                Id = updateCustomer.Id,
                Name = updateCustomer.Name,
                Address = updateCustomer.Address,
                Email = updateCustomer.Email
            };
        }

        public List<Customer> SearchCustomer(string queryStr)
        {
            return _context.Customers.Where(
                custo => custo.Name.Contains(queryStr)).Select(
                    c => new Customer()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Email = c.Email
                    }
                ).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Select(product => new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = (decimal)product.Price,
                Description = product.Description,
                Category = product.Category

            }).ToList();
        }
        public List<StoreFront> GetAllStores()
        {
            return _context.StoreFronts.Select(stores => new StoreFront()
            {
                Id = stores.Id,
                Name = stores.Name,
                Address = stores.Address,
                Inventories = stores.Inventories,
                Orders = stores.Orders
            }).ToList();
        }

        public List<Order> GetAllOrders()
        {
            return _context.Orders.Select(order => new Order()
            {
                Id = order.Id,
                Total = (decimal)order.Total,
                OrderDate = (DateTime)order.OrderDate,
                CustomerId = order.CustomerId,
                StoreFrontId = order.StoreFrontId
            }).ToList();
        }

        public List<LineItems> GetLineItems()
        {
            return _context.LineItems.Select(items => new LineItems()
            {
                Id = items.Id,
                Quantity = (int)items.Quantity
            }).ToList();
        }


        public List<Inventory> GettAllInventories()
        {
            return _context.Inventories.Include("Product").Select(inventory => new Inventory()
            {
                Quantity = (int)inventory.Quantity,
                ProductID = (int)inventory.ProductID,
                StoreID = (int)inventory.StoreID,
                Product = new Product
                {
                    Id = inventory.Product.Id,
                    Name = inventory.Product.Name,
                    Description = inventory.Product.Description,
                    Price = (decimal)inventory.Product.Price,
                    Category = inventory.Product.Category
                }
            }).ToList();
        }


        public Product UpdateProduct(Product productToUpdate)
        {
            Product updateProduct = new Product()
            {
                Id = productToUpdate.Id,
                Name = productToUpdate.Name,
                Price = productToUpdate.Price,
                Description = productToUpdate.Description,
                Category = productToUpdate.Category
            };

            updateProduct = _context.Products.Update(updateProduct).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Product()
            {
                Id = updateProduct.Id,
                Name = updateProduct.Name,
                Price = (decimal)updateProduct.Price,
                Description = updateProduct.Description,
                Category = updateProduct.Category
            };
        }

        public StoreFront UpdateStore(StoreFront storeToUpdate)
        {
            storeToUpdate = _context.Update(storeToUpdate).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return storeToUpdate;
        }


        public StoreFront AddStore(StoreFront storeFront)
        {

            storeFront = _context.Add(storeFront).Entity;

            _context.SaveChanges();

            _context.ChangeTracker.Clear();

            return storeFront;
        }

        public LineItems AddLineItem(LineItems lineItems)
        {
            

            lineItems = _context.Add(lineItems).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return lineItems;
        }

        public Inventory AddInventory(Inventory inventory)
        {
            

            inventory = _context.Add(inventory).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return inventory;
        }

        public List<Inventory> GetInventoriesByStoreId(int storeId)
        {
            return _context.Inventories.Where(inv => inv.StoreID == storeId).Select(newInv => new Inventory()
            {
                Id = newInv.Id,
                StoreID = (int)newInv.StoreID,
                Quantity = (int)newInv.Quantity,
                ProductID = (int)newInv.ProductID
            }).ToList();
        }

        public Product GetProductById(int productId)
        {
            Product productById = _context.Products.FirstOrDefault(p => p.Id == productId);

            return new Product()
            {
                Id = productById.Id,
                Name = productById.Name,
                Price = (decimal)productById.Price,
                Description = productById.Description,
                Category = productById.Category
            };
        }

        public Inventory UpdateInventory(Inventory inventoryToUpdate)
        {
            Inventory updateInventory = new Inventory()
            {
                Id = inventoryToUpdate.Id,
                StoreID = inventoryToUpdate.StoreID,
                ProductID = inventoryToUpdate.ProductID,
                Quantity = inventoryToUpdate.Quantity,
            };

            updateInventory = _context.Inventories.Update(updateInventory).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Inventory()
            {
                // Id = updateInventory.Id,
                StoreID = (int)updateInventory.StoreID,
                ProductID = (int)updateInventory.ProductID
            };
        }

        public Order UpdateOrder(Order orderToUpdate)
        {
            Order updateOrder = new Order()
            {
                Id = orderToUpdate.Id,
                Total = orderToUpdate.Total,
                CustomerId = orderToUpdate.CustomerId,
                StoreFrontId = orderToUpdate.StoreFrontId,
                OrderDate = orderToUpdate.OrderDate
            };

            updateOrder = _context.Orders.Update(updateOrder).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            return new Order()
            {
                Id = updateOrder.Id,
                Total = (decimal)updateOrder.Total,
                CustomerId = updateOrder.CustomerId,
                StoreFrontId = updateOrder.StoreFrontId,
                OrderDate = orderToUpdate.OrderDate
            };
        }

        public StoreFront GetStoreById(int storeId)
        {
            StoreFront  storeById = _context.StoreFronts.Include(s => s.Inventories).FirstOrDefault(s => s.Id == storeId);

            return storeById;
        }
        /// <summary>
        /// Deletes a Store
        /// </summary>
        /// <param name="storeId"> id of the store to be deleted. </param>
        public void DeleteStore(int storeId)
        {
            _context.StoreFronts.Remove(GetStoreById(storeId));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

    }
}