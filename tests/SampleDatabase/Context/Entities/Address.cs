using System.ComponentModel.DataAnnotations;
using SampleDatabase.Context.Entities.Interfaces;

namespace SampleDatabase.Context.Entities
{
    public class Address : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}