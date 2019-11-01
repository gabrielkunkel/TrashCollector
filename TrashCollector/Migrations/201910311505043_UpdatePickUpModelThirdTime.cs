namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatePickUpModelThirdTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUpModels", "Recurring", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PickUpModels", "Recurring");
        }
    }
}
