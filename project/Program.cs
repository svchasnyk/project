using System;
using System.Collections.Generic;

namespace project
{
    public enum Genre { Fantasy, Detective, Science, Novel, Children }
    public enum OrderStatus { New, Processing, Delivered, Cancelled }

    public interface IEmployee
    {
        string Name { get; set; }
        string Position { get; set; }
        int Experience { get; set; }
        void Work();
    }

    public class BookStore
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<Book> Books { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
        public List<Client> Clients { get; set; } = new();
        public List<Order> Orders { get; set; } = new();

        public void AddBook(Book book) => Books.Add(book);
        public void RegisterClient(Client client) => Clients.Add(client);
        public void CreateOrder(Order order)
        {
            Orders.Add(order);
            order.Client.OrdersHistory.Add(order);
        }
    }

    public class Book
    {
        public string Title { get; set; } = string.Empty;
        public Author Author { get; set; } = new();
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Genre Genre { get; set; }
    }

    public class Author
    {
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
    }

    public class Client
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Order> OrdersHistory { get; set; } = new();
    }

    public class Order
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public List<Book> Books { get; set; } = new();
        public Client Client { get; set; } = new();
        public OrderStatus Status { get; set; } = OrderStatus.New;
    }

    public class Employee : IEmployee
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Experience { get; set; }

        public void Work()
        {
            Console.WriteLine($"{Name} працює на посаді {Position} з досвідом {Experience} років.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Приложение project запущено!");
        }
    }
}
