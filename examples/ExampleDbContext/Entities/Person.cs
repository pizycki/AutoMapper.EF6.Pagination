using System;

namespace ExampleDbContext.Entities
{
    public abstract class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }

    public class Customer : Person { }

    public class Employer : Person { }

    public class Director : Person { }

    public class Manager : Person { }
}