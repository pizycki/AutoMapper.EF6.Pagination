using System.ComponentModel.DataAnnotations;
using SampleDatabase.Context.Entities.Interfaces;

namespace SampleDatabase.Context.Entities
{
    public class Company : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int OwnerId { get; set; }
        public virtual Person Owner { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}