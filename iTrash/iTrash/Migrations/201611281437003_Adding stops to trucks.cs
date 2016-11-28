namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addingstopstotrucks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Truck__ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Truck__ID");
            AddForeignKey("dbo.AspNetUsers", "Truck__ID", "dbo.Trucks", "_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Truck__ID", "dbo.Trucks");
            DropIndex("dbo.AspNetUsers", new[] { "Truck__ID" });
            DropColumn("dbo.AspNetUsers", "Truck__ID");
        }
    }
}
