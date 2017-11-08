using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SampleDatabase.SampleDbContext.Entities
{
    public class Foo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual Bar Bar { get; set; }
    }

    public class Foo_EntityConfiguration : EntityTypeConfiguration<Foo>
    {
        public Foo_EntityConfiguration()
        {
            // Id
            HasKey(foo => foo.Id);
            Property(foo => foo.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Name
            Property(foo => foo.Name).HasMaxLength(200);
        }
    }
}
