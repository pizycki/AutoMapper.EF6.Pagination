using System.Data.Entity.Migrations;

namespace SampleDatabase.DbContext.Migrations
{
    public partial class _100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foos",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Foos");
        }
    }
}
