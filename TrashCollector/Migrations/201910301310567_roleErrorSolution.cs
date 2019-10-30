namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class roleErrorSolution : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AddressModels", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerModels", "Status", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.CustomerModels", "Status", c => c.String());
            AlterColumn("dbo.AddressModels", "State", c => c.String());
        }
    }
}
