namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adamstest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trucks",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _TruckNumber = c.String(),
                        _Zipcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.Zipcodes", t => t._Zipcode, cascadeDelete: true)
                .Index(t => t._Zipcode);
            
            CreateTable(
                "dbo.Zipcodes",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Zipcode = c.String(),
                    })
                .PrimaryKey(t => t._ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trucks", "_Zipcode", "dbo.Zipcodes");
            DropIndex("dbo.Trucks", new[] { "_Zipcode" });
            DropTable("dbo.Zipcodes");
            DropTable("dbo.Trucks");
        }
    }
}
