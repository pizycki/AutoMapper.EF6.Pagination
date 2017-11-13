namespace SampleDatabase.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _100 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Companies", "OwnerId", "dbo.People");
            DropForeignKey("dbo.Companies", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Companies", new[] { "AddressId" });
            DropIndex("dbo.Companies", new[] { "OwnerId" });
            DropTable("dbo.People");
            DropTable("dbo.Companies");
            DropTable("dbo.Addresses");
        }
    }
}
