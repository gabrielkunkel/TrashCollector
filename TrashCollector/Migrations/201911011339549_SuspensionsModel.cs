namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SuspensionsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuspensionModels",
                c => new
                {
                    SuspensionId = c.Guid(nullable: false),
                    Start = c.DateTime(nullable: false),
                    End = c.DateTime(nullable: false),
                    CustomerId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.SuspensionId);

        }

        public override void Down()
        {
            DropTable("dbo.SuspensionModels");
        }
    }
}
