using System;
using System.Collections.Generic;
using System.Text;

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

    public abstract class Person
    {
        public abstract string Name { get; set; }
        public abstract void Introduce();
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

    public class Book : IComparable<Book>
    {
        public string Title { get; set; } = string.Empty;
        public Author Author { get; set; } = new();
        public string ISBN { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public Genre Genre { get; set; }

        public int CompareTo(Book? other)
        {
            if (other == null) return 1;
            return Title.CompareTo(other.Title);
        }
    }

    public class Author : Person
    {
        public override string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;

        public override void Introduce()
        {
            Console.WriteLine($"Я автор: {Name}");
        }
    }

    public class Client : Person
    {
        public override string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Order> OrdersHistory { get; set; } = new();

        public override void Introduce()
        {
            Console.WriteLine($"Я клієнт: {Name}");
        }
    }

    public class Order
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public List<Book> Books { get; set; } = new();
        public Client Client { get; set; } = new();
        public OrderStatus Status { get; set; } = OrderStatus.New;
    }

    public class Employee : Person, IEmployee, ICloneable
    {
        public override string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int Experience { get; set; }

        public void Work()
        {
            Console.WriteLine($"{Name} працює на посаді {Position} з досвідом {Experience} років.");
        }

        public object Clone() => MemberwiseClone();

        public override void Introduce()
        {
            Console.WriteLine($"Я працівник: {Name}, посада: {Position}");
        }
    }

    class Program
    {
        static void ShowMenu()
        {
            Console.WriteLine("=== Книжковий магазин ===");
            Console.WriteLine("1. Додати книгу");
            Console.WriteLine("2. Зареєструвати клієнта");
            Console.WriteLine("3. Створити замовлення");
            Console.WriteLine("4. Вийти");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; 

            Console.WriteLine("Приложение project запущено!");
            ShowMenu();
            List<Person> people = new List<Person>
            {
                new Client { Name = "Alice" },
                new Author { Name = "Bob" },
                new Employee { Name = "John", Position = "Seller", Experience = 5 }
            };

            foreach (var person in people)
            {
                person.Introduce();
            }
        }
    }
}
