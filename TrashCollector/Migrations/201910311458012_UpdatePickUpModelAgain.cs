namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatePickUpModelAgain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUpModels", "Pending", c => c.Boolean(nullable: false));
            AddColumn("dbo.PickUpModels", "Completed", c => c.Boolean(nullable: false));
            DropColumn("dbo.PickUpModels", "Status");
        }

        public override void Down()
        {
            AddColumn("dbo.PickUpModels", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.PickUpModels", "Completed");
            DropColumn("dbo.PickUpModels", "Pending");
        }
    }
}
