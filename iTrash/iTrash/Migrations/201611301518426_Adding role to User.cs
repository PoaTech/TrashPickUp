namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingroletoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "role");
        }
    }
}
