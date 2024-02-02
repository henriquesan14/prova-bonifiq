using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<TestDbContext> _context;

        public CustomerServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            optionsBuilder.UseInMemoryDatabase("MyMemoryDatabase");
            _context = new Mock<TestDbContext>(optionsBuilder.Options);
        }

        [Fact]
        public async Task CanPurchase_Executed_ReturnTrue()
        {
            var ordersSet = new List<Order>
            {
                new Order { Id = 1, CustomerId = 1, Value = 100 },
            };

            var productsSet = new List<Product>
            {
                new Product { Id = 1, Name = "Product1" },
                new Product { Id = 2, Name = "Product2" },
            };

            var customer = new Customer
            {
                Name = "Test",
                Id = 1,
                Orders = ordersSet
            };

            var customersSet = new List<Customer>
            {
                customer
            };

            _context.Setup(db => db.Customers).ReturnsDbSet(customersSet);
            _context.Setup(db => db.Products).ReturnsDbSet(productsSet);
            _context.Setup(db => db.Orders).ReturnsDbSet(ordersSet);
            
            _context.Setup(db => db.Customers.FindAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var customerService = new CustomerService(_context.Object);
            var result = await customerService.CanPurchase(1, 100);

            Assert.True(result);
        }

        [Fact]
        public async Task CanPurchase_WhenCustomerIdEqualZero_ArgumentOutOfRangeException()
        {
            var customerService = new CustomerService(_context.Object);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await customerService.CanPurchase(0, 100));
        }

        [Fact]
        public async Task CanPurchase_WhenPurchaseValueEqualZero_ArgumentOutOfRangeException()
        {
            var customerService = new CustomerService(_context.Object);

            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await customerService.CanPurchase(1, 0));
        }

        [Fact]
        public async Task CanPurchase_WhenCustomerIsNull_InvalidOperationException()
        {
            var ordersSet = new List<Order>
            {
                new Order { Id = 1, CustomerId = 1, Value = 100 }
            };
            var customersSet = new List<Customer>
            {
                new Customer { Id = 2, Name = "Test1", Orders = ordersSet },
            };

            _context.Setup(db => db.Customers).ReturnsDbSet(customersSet);

            var customerService = new CustomerService(_context.Object);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await customerService.CanPurchase(1, 100));
        }

        [Fact]
        public async Task CanPurchase_WhenOrdersInThisMonthGreaterThanZero_ReturnFalse()
        {
            
            var ordersSet = new List<Order>
            {
                new Order { Id = 1, CustomerId = 1, Value = 100, OrderDate =  DateTime.UtcNow.AddMonths(2)}
            };
            var customer = new Customer
            {
                Name = "Test",
                Id = 1,
                Orders = ordersSet
            };
            var customersSet = new List<Customer>
            {
                customer
            };

            _context.Setup(db => db.Customers).ReturnsDbSet(customersSet);
            _context.Setup(db => db.Orders).ReturnsDbSet(ordersSet);
            _context.Setup(db => db.Customers.FindAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var customerService = new CustomerService(_context.Object);

            var result = await customerService.CanPurchase(1, 100);
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_WhenHaveBoughtBeforeGreaterThanZeroAndPurchaseValueGreaterThanHundred_ReturnFalse()
        {

            var ordersSet = new List<Order>
            {
                new Order { Id = 1, CustomerId = 1, Value = 100, OrderDate =  DateTime.UtcNow.AddMonths(-2)}
            };
            var customer = new Customer
            {
                Name = "Test",
                Id = 2,
                Orders = new List<Order>
                {
                    new Order
                    {
                        Id = 2,
                         CustomerId = 2,
                    }
                }
            };
            var customersSet = new List<Customer>
            {
                customer
            };

            _context.Setup(db => db.Customers).ReturnsDbSet(customersSet);
            _context.Setup(db => db.Orders).ReturnsDbSet(ordersSet);
            _context.Setup(db => db.Customers.FindAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var customerService = new CustomerService(_context.Object);

            var result = await customerService.CanPurchase(1, 101);
            Assert.False(result);
        }
    }

}
