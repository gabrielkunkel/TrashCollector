namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPickUpPaid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PickUpModels", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickUpModels", "Paid");
        }
    }
}
