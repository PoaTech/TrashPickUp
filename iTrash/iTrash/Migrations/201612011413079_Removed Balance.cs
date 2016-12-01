namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedBalance : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "balance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "balance", c => c.Int(nullable: false));
        }
    }
}
