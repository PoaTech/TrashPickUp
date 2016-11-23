namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changedzipcodetoint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zipcodes", "_Zipcode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zipcodes", "_Zipcode", c => c.String());
        }
    }
}
