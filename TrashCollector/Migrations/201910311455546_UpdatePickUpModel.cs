namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdatePickUpModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PickUpModels", "Status", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.PickUpModels", "Status", c => c.String());
        }
    }
}
