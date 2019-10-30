namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressModels",
                c => new
                {
                    AddressId = c.Guid(nullable: false),
                    StreetAddress = c.String(),
                    SecondaryAddress = c.String(),
                    City = c.String(),
                    State = c.String(),
                    ZipCode = c.String(),
                })
                .PrimaryKey(t => t.AddressId);

            CreateTable(
                "dbo.CustomerModels",
                c => new
                {
                    CustomerId = c.Guid(nullable: false),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Status = c.String(),
                    PickUpDay = c.Int(nullable: false),
                    BaseCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    ApplicationId = c.String(maxLength: 128),
                    AddressId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.AddressModels", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId)
                .Index(t => t.AddressId);

            CreateTable(
                "dbo.EmployeeModels",
                c => new
                {
                    EmloyeeId = c.Guid(nullable: false),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    ApplicationId = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.EmloyeeId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.ApplicationId);

            CreateTable(
                "dbo.PickUpModels",
                c => new
                {
                    PickUpId = c.Guid(nullable: false),
                    Scheduled = c.DateTime(nullable: false),
                    Status = c.String(),
                    Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    CustomerId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.PickUpId)
                .ForeignKey("dbo.CustomerModels", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.PickUpModels", "CustomerId", "dbo.CustomerModels");
            DropForeignKey("dbo.EmployeeModels", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerModels", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerModels", "AddressId", "dbo.AddressModels");
            DropIndex("dbo.PickUpModels", new[] { "CustomerId" });
            DropIndex("dbo.EmployeeModels", new[] { "ApplicationId" });
            DropIndex("dbo.CustomerModels", new[] { "AddressId" });
            DropIndex("dbo.CustomerModels", new[] { "ApplicationId" });
            DropTable("dbo.PickUpModels");
            DropTable("dbo.EmployeeModels");
            DropTable("dbo.CustomerModels");
            DropTable("dbo.AddressModels");
        }
    }
}
