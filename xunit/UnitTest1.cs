using System;
using System.Collections.Generic;
using Xunit;
using project;

namespace xunittest
{
    public class BookStoreTests
    {
        [Fact]
        public void AddBook_ShouldAddBookToStore()
        {
            var store = new BookStore();
            var book = new Book { Title = "Test Book", ISBN = "12345" };

            store.AddBook(book);

            Assert.Contains(book, store.Books);
        }

        [Fact]
        public void RegisterClient_ShouldAddClientToStore()
        {
            var store = new BookStore();
            var client = new Client { Name = "Alice", Email = "alice@example.com" };

            store.RegisterClient(client);

            Assert.Contains(client, store.Clients);
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
