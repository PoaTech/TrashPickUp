namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedbalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "balance", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "balance");
        }
    }
}
