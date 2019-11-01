namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class FixTypoForEmployeeId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EmployeeModels");
            AddColumn("dbo.EmployeeModels", "EmployeeId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.EmployeeModels", "EmployeeId");
            DropColumn("dbo.EmployeeModels", "EmloyeeId");
        }

        public override void Down()
        {
            AddColumn("dbo.EmployeeModels", "EmloyeeId", c => c.Guid(nullable: false));
            DropPrimaryKey("dbo.EmployeeModels");
            DropColumn("dbo.EmployeeModels", "EmployeeId");
            AddPrimaryKey("dbo.EmployeeModels", "EmloyeeId");
        }
    }
}
