namespace SampleDatabase.SampleDbContext.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class _101 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bars",
                c => new
                {
                    FooId = c.Guid(nullable: false),
                    Value = c.String(),
                })
                .PrimaryKey(t => t.FooId)
                .ForeignKey("dbo.Foos", t => t.FooId, cascadeDelete: true)
                .Index(t => t.FooId);

            AlterColumn("dbo.Foos", "Name", c => c.String(maxLength: 200));
        }

        public override void Down()
        {
            DropForeignKey("dbo.Bars", "FooId", "dbo.Foos");
            DropIndex("dbo.Bars", new[] { "FooId" });
            AlterColumn("dbo.Foos", "Name", c => c.String());
            DropTable("dbo.Bars");
        }
    }
}
