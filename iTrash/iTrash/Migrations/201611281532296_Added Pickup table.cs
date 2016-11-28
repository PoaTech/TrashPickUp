namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPickuptable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _User = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.AspNetUsers", t => t._User)
                .Index(t => t._User);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pickups", "_User", "dbo.AspNetUsers");
            DropIndex("dbo.Pickups", new[] { "_User" });
            DropTable("dbo.Pickups");
        }
    }
}
