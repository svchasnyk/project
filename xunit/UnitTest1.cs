using System;
using System.Collections.Generic;
using Xunit;
using project;

namespace xunittest
{
    public class BookStoreTests
    {
        [Fact]
        public void AddBook_ShouldTriggerEvent()
        {
            var store = new BookStore();
            var book = new Book { Title = "Test Book", ISBN = "12345", Author = new Author { Name = "Автор" } };
            bool eventTriggered = false;

            store.OnBookAdded += b => eventTriggered = true;
            store.AddBook(book);

            Assert.True(eventTriggered);
            Assert.Contains(book, store.Books);
        }

        [Fact]
        public void CreateOrder_ShouldAddOrderToStore_AndClientHistory()
        {
            var store = new BookStore();
            var client = new Client { Name = "Bob", Email = "bob@example.com" };
            var book = new Book { Title = "Test Book", ISBN = "12345" };
            var order = new Order { Client = client, Books = new List<Book> { book } };

            store.CreateOrder(order);

            Assert.Contains(order, store.Orders);
            Assert.Contains(order, client.OrdersHistory);
        }
    }

    public class OrderProcessorTests
    {
        [Fact]
        public void Process_ShouldTriggerEvent()
        {
            var processor = new OrderProcessor();
            var client = new Client { Name = "Alice", Email = "alice@example.com" };
            var order = new Order { Client = client, Books = new List<Book> { new Book { Title = "Book1" } } };

            bool eventTriggered = false;
            processor.OnOrderProcessed += o => eventTriggered = true;

            processor.Process(order);

            Assert.True(eventTriggered);
        }
    }

    public class DelegateTests
    {
        [Fact]
        public void Action_ShouldPrintBook()
        {
            var book = new Book { Title = "Book1", Author = new Author { Name = "Author1" }, Price = 100 };
            Action<Book> printBook = b => Assert.Equal("Book1", b.Title);

            printBook(book);
        }

        [Fact]
        public void Predicate_ShouldCheckClientEmail()
        {
            var client = new Client { Name = "Test", Email = "test@example.com" };
            Predicate<Client> hasEmail = c => !string.IsNullOrWhiteSpace(c.Email);

            Assert.True(hasEmail(client));
        }

        [Fact]
        public void Func_ShouldValidateOrder()
        {
            var order = new Order { Books = new List<Book> { new Book { Title = "Book1" } } };
            Func<Order, bool> isValidOrder = o => o.Books.Count > 0;

            Assert.True(isValidOrder(order));
        }
    }

    public class EmployeeTests
    {
        [Fact]
        public void EmployeeWork_ShouldNotThrow()
        {
            var employee = new Employee { Name = "John", Position = "Seller", Experience = 5 };
            var exception = Record.Exception(() => employee.Work());

            Assert.Null(exception);
        }

        [Fact]
        public void EmployeeClone_ShouldCreateCopy()
        {
            var employee = new Employee { Name = "John", Position = "Seller", Experience = 5 };
            var clone = (Employee)employee.Clone();

            Assert.Equal(employee.Name, clone.Name);
            Assert.Equal(employee.Position, clone.Position);
            Assert.Equal(employee.Experience, clone.Experience);
        }
    }
}
