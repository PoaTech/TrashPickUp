namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Trucknumberrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trucks", "_TruckNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trucks", "_TruckNumber", c => c.String());
        }
    }
}
