namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AndNowForTheUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "_FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "_LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "_Address", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_BillingInfo", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_PickupDay", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_AltPickupDay", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_LeaveDate", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "_ReturnDate", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "address__ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "altPickupDay__ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "billingInfo__ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "leaveDate__ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "pickupDay__ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "returnDate__ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "address__ID");
            CreateIndex("dbo.AspNetUsers", "altPickupDay__ID");
            CreateIndex("dbo.AspNetUsers", "billingInfo__ID");
            CreateIndex("dbo.AspNetUsers", "leaveDate__ID");
            CreateIndex("dbo.AspNetUsers", "pickupDay__ID");
            CreateIndex("dbo.AspNetUsers", "returnDate__ID");
            AddForeignKey("dbo.AspNetUsers", "address__ID", "dbo.Addresses", "_ID");
            AddForeignKey("dbo.AspNetUsers", "altPickupDay__ID", "dbo.WeekDays", "_ID");
            AddForeignKey("dbo.AspNetUsers", "billingInfo__ID", "dbo.PaymentInfoes", "_ID");
            AddForeignKey("dbo.AspNetUsers", "leaveDate__ID", "dbo.Dates", "_ID");
            AddForeignKey("dbo.AspNetUsers", "pickupDay__ID", "dbo.WeekDays", "_ID");
            AddForeignKey("dbo.AspNetUsers", "returnDate__ID", "dbo.Dates", "_ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "returnDate__ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUsers", "pickupDay__ID", "dbo.WeekDays");
            DropForeignKey("dbo.AspNetUsers", "leaveDate__ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUsers", "billingInfo__ID", "dbo.PaymentInfoes");
            DropForeignKey("dbo.AspNetUsers", "altPickupDay__ID", "dbo.WeekDays");
            DropForeignKey("dbo.AspNetUsers", "address__ID", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "returnDate__ID" });
            DropIndex("dbo.AspNetUsers", new[] { "pickupDay__ID" });
            DropIndex("dbo.AspNetUsers", new[] { "leaveDate__ID" });
            DropIndex("dbo.AspNetUsers", new[] { "billingInfo__ID" });
            DropIndex("dbo.AspNetUsers", new[] { "altPickupDay__ID" });
            DropIndex("dbo.AspNetUsers", new[] { "address__ID" });
            DropColumn("dbo.AspNetUsers", "returnDate__ID");
            DropColumn("dbo.AspNetUsers", "pickupDay__ID");
            DropColumn("dbo.AspNetUsers", "leaveDate__ID");
            DropColumn("dbo.AspNetUsers", "billingInfo__ID");
            DropColumn("dbo.AspNetUsers", "altPickupDay__ID");
            DropColumn("dbo.AspNetUsers", "address__ID");
            DropColumn("dbo.AspNetUsers", "_ReturnDate");
            DropColumn("dbo.AspNetUsers", "_LeaveDate");
            DropColumn("dbo.AspNetUsers", "_AltPickupDay");
            DropColumn("dbo.AspNetUsers", "_PickupDay");
            DropColumn("dbo.AspNetUsers", "_BillingInfo");
            DropColumn("dbo.AspNetUsers", "_Address");
            DropColumn("dbo.AspNetUsers", "_LastName");
            DropColumn("dbo.AspNetUsers", "_FirstName");
        }
    }
}
