using System;
using System.Data.Entity.ModelConfiguration;

namespace SampleDatabase.SampleDbContext.Entities
{
    public class Bar
    {
        public Guid FooId { get; set; }

        public virtual Foo Foo { get; set; }

        public string Value { get; set; }
    }

    public class Bar_EntityConfgiuration : EntityTypeConfiguration<Bar>
    {
        public Bar_EntityConfgiuration()
        {
            HasKey(bar => bar.FooId);

            HasRequired(bar => bar.Foo)
                .WithOptional(foo => foo.Bar)
                .WillCascadeOnDelete();
        }
    }
}
