namespace TrashCollector.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddZipCodeToEmployeeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeModels", "ZipCode", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.EmployeeModels", "ZipCode");
        }
    }
}
