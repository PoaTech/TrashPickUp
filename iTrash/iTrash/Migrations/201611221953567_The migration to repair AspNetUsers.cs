namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemigrationtorepairAspNetUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "_Address_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_BillingInfo_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_PickupDay_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_AltPickupDay_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_LeaveDate_ID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_ReturnDate_ID", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "_Address");
            DropColumn("dbo.AspNetUsers", "_BillingInfo");
            DropColumn("dbo.AspNetUsers", "_PickupDay");
            DropColumn("dbo.AspNetUsers", "_AltPickupDay");
            DropColumn("dbo.AspNetUsers", "_LeaveDate");
            DropColumn("dbo.AspNetUsers", "_ReturnDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "_ReturnDate", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_LeaveDate", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_AltPickupDay", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_PickupDay", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_BillingInfo", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_Address", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "_ReturnDate_ID");
            DropColumn("dbo.AspNetUsers", "_LeaveDate_ID");
            DropColumn("dbo.AspNetUsers", "_AltPickupDay_ID");
            DropColumn("dbo.AspNetUsers", "_PickupDay_ID");
            DropColumn("dbo.AspNetUsers", "_BillingInfo_ID");
            DropColumn("dbo.AspNetUsers", "_Address_ID");
        }
    }
}
