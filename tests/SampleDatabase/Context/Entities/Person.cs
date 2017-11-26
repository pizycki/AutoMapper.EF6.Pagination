using System.ComponentModel.DataAnnotations;
using SampleDatabase.Context.Entities.Interfaces;

namespace SampleDatabase.Context.Entities
{
    public class Person : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}