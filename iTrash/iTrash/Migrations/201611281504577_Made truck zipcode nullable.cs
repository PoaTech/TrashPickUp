namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Madetruckzipcodenullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trucks", "_Zipcode", "dbo.Zipcodes");
            DropIndex("dbo.Trucks", new[] { "_Zipcode" });
            AlterColumn("dbo.Trucks", "_Zipcode", c => c.Int());
            CreateIndex("dbo.Trucks", "_Zipcode");
            AddForeignKey("dbo.Trucks", "_Zipcode", "dbo.Zipcodes", "_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trucks", "_Zipcode", "dbo.Zipcodes");
            DropIndex("dbo.Trucks", new[] { "_Zipcode" });
            AlterColumn("dbo.Trucks", "_Zipcode", c => c.Int(nullable: false));
            CreateIndex("dbo.Trucks", "_Zipcode");
            AddForeignKey("dbo.Trucks", "_Zipcode", "dbo.Zipcodes", "_ID", cascadeDelete: true);
        }
    }
}
