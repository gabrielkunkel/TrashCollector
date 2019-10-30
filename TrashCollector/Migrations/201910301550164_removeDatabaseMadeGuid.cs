namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class removeDatabaseMadeGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerModels", "AddressId", "dbo.AddressModels");
            DropPrimaryKey("dbo.AddressModels");
            AlterColumn("dbo.AddressModels", "AddressId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.AddressModels", "AddressId");
            AddForeignKey("dbo.CustomerModels", "AddressId", "dbo.AddressModels", "AddressId", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.CustomerModels", "AddressId", "dbo.AddressModels");
            DropPrimaryKey("dbo.AddressModels");
            AlterColumn("dbo.AddressModels", "AddressId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.AddressModels", "AddressId");
            AddForeignKey("dbo.CustomerModels", "AddressId", "dbo.AddressModels", "AddressId", cascadeDelete: true);
        }
    }
}
