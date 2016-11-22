namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Madeusertablenullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "_Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUsers", "_AltPickupDay_ID", "dbo.WeekDays");
            DropForeignKey("dbo.AspNetUsers", "_BillingInfo_ID", "dbo.PaymentInfoes");
            DropForeignKey("dbo.AspNetUsers", "_LeaveDate_ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUsers", "_ReturnDate_ID", "dbo.Dates");
            DropIndex("dbo.AspNetUsers", new[] { "_Address_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_BillingInfo_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_AltPickupDay_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_LeaveDate_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_ReturnDate_ID" });
            AlterColumn("dbo.AspNetUsers", "_Address_ID", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "_BillingInfo_ID", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "_AltPickupDay_ID", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "_LeaveDate_ID", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "_ReturnDate_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "_Address_ID");
            CreateIndex("dbo.AspNetUsers", "_BillingInfo_ID");
            CreateIndex("dbo.AspNetUsers", "_AltPickupDay_ID");
            CreateIndex("dbo.AspNetUsers", "_LeaveDate_ID");
            CreateIndex("dbo.AspNetUsers", "_ReturnDate_ID");
            AddForeignKey("dbo.AspNetUsers", "_Address_ID", "dbo.Addresses", "_ID");
            AddForeignKey("dbo.AspNetUsers", "_AltPickupDay_ID", "dbo.WeekDays", "_ID");
            AddForeignKey("dbo.AspNetUsers", "_BillingInfo_ID", "dbo.PaymentInfoes", "_ID");
            AddForeignKey("dbo.AspNetUsers", "_LeaveDate_ID", "dbo.Dates", "_ID");
            AddForeignKey("dbo.AspNetUsers", "_ReturnDate_ID", "dbo.Dates", "_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "_ReturnDate_ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUsers", "_LeaveDate_ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUsers", "_BillingInfo_ID", "dbo.PaymentInfoes");
            DropForeignKey("dbo.AspNetUsers", "_AltPickupDay_ID", "dbo.WeekDays");
            DropForeignKey("dbo.AspNetUsers", "_Address_ID", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "_ReturnDate_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_LeaveDate_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_AltPickupDay_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_BillingInfo_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_Address_ID" });
            AlterColumn("dbo.AspNetUsers", "_ReturnDate_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "_LeaveDate_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "_AltPickupDay_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "_BillingInfo_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "_Address_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "_ReturnDate_ID");
            CreateIndex("dbo.AspNetUsers", "_LeaveDate_ID");
            CreateIndex("dbo.AspNetUsers", "_AltPickupDay_ID");
            CreateIndex("dbo.AspNetUsers", "_BillingInfo_ID");
            CreateIndex("dbo.AspNetUsers", "_Address_ID");
            AddForeignKey("dbo.AspNetUsers", "_ReturnDate_ID", "dbo.Dates", "_ID", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "_LeaveDate_ID", "dbo.Dates", "_ID", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "_BillingInfo_ID", "dbo.PaymentInfoes", "_ID", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "_AltPickupDay_ID", "dbo.WeekDays", "_ID", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "_Address_ID", "dbo.Addresses", "_ID", cascadeDelete: true);
        }
    }
}
