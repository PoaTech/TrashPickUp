namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheGrandDatabasePopulationEvent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _StreetAddress1 = c.String(),
                        _StreetAddress2 = c.String(),
                        _City = c.Int(nullable: false),
                        _Zipcode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.Cities", t => t._City, cascadeDelete: true)
                .ForeignKey("dbo.Zipcodes", t => t._Zipcode, cascadeDelete: true)
                .Index(t => t._City)
                .Index(t => t._Zipcode);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _City = c.String(),
                        _State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.States", t => t._State, cascadeDelete: true)
                .Index(t => t._State);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _State = c.String(),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.CalendarDays",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Day = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.CardTypes",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Provider = c.String(),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.CheckingAccounts",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _RoutingNumber = c.Int(nullable: false),
                        _AccountNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Name = c.String(),
                        _CardNumber = c.String(),
                        _ExpirationDate = c.Int(nullable: false),
                        _BillingAddress = c.Int(nullable: false),
                        _CardType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.Addresses", t => t._BillingAddress, cascadeDelete: true)
                .ForeignKey("dbo.CardTypes", t => t._CardType, cascadeDelete: true)
                .ForeignKey("dbo.ExpirationDates", t => t._ExpirationDate, cascadeDelete: true)
                .Index(t => t._ExpirationDate)
                .Index(t => t._BillingAddress)
                .Index(t => t._CardType);
            
            CreateTable(
                "dbo.ExpirationDates",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Month = c.Int(nullable: false),
                        _Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.Months", t => t._Month, cascadeDelete: true)
                .ForeignKey("dbo.Years", t => t._Year, cascadeDelete: true)
                .Index(t => t._Month)
                .Index(t => t._Year);
            
            CreateTable(
                "dbo.Months",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Month = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.Dates",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Day = c.Int(nullable: false),
                        _Month = c.Int(nullable: false),
                        _Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.CalendarDays", t => t._Day, cascadeDelete: true)
                .ForeignKey("dbo.Months", t => t._Month, cascadeDelete: true)
                .ForeignKey("dbo.Years", t => t._Year, cascadeDelete: true)
                .Index(t => t._Day)
                .Index(t => t._Month)
                .Index(t => t._Year);
            
            CreateTable(
                "dbo.PaymentInfoes",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _CreditCard = c.Int(nullable: false),
                        _CheckingAccount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._ID)
                .ForeignKey("dbo.CheckingAccounts", t => t._CheckingAccount, cascadeDelete: true)
                .ForeignKey("dbo.CreditCards", t => t._CreditCard, cascadeDelete: true)
                .Index(t => t._CreditCard)
                .Index(t => t._CheckingAccount);
            
            CreateTable(
                "dbo.WeekDays",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Day = c.String(),
                    })
                .PrimaryKey(t => t._ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentInfoes", "_CreditCard", "dbo.CreditCards");
            DropForeignKey("dbo.PaymentInfoes", "_CheckingAccount", "dbo.CheckingAccounts");
            DropForeignKey("dbo.Dates", "_Year", "dbo.Years");
            DropForeignKey("dbo.Dates", "_Month", "dbo.Months");
            DropForeignKey("dbo.Dates", "_Day", "dbo.CalendarDays");
            DropForeignKey("dbo.CreditCards", "_ExpirationDate", "dbo.ExpirationDates");
            DropForeignKey("dbo.ExpirationDates", "_Year", "dbo.Years");
            DropForeignKey("dbo.ExpirationDates", "_Month", "dbo.Months");
            DropForeignKey("dbo.CreditCards", "_CardType", "dbo.CardTypes");
            DropForeignKey("dbo.CreditCards", "_BillingAddress", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "_Zipcode", "dbo.Zipcodes");
            DropForeignKey("dbo.Addresses", "_City", "dbo.Cities");
            DropForeignKey("dbo.Cities", "_State", "dbo.States");
            DropIndex("dbo.PaymentInfoes", new[] { "_CheckingAccount" });
            DropIndex("dbo.PaymentInfoes", new[] { "_CreditCard" });
            DropIndex("dbo.Dates", new[] { "_Year" });
            DropIndex("dbo.Dates", new[] { "_Month" });
            DropIndex("dbo.Dates", new[] { "_Day" });
            DropIndex("dbo.ExpirationDates", new[] { "_Year" });
            DropIndex("dbo.ExpirationDates", new[] { "_Month" });
            DropIndex("dbo.CreditCards", new[] { "_CardType" });
            DropIndex("dbo.CreditCards", new[] { "_BillingAddress" });
            DropIndex("dbo.CreditCards", new[] { "_ExpirationDate" });
            DropIndex("dbo.Cities", new[] { "_State" });
            DropIndex("dbo.Addresses", new[] { "_Zipcode" });
            DropIndex("dbo.Addresses", new[] { "_City" });
            DropTable("dbo.WeekDays");
            DropTable("dbo.PaymentInfoes");
            DropTable("dbo.Dates");
            DropTable("dbo.Years");
            DropTable("dbo.Months");
            DropTable("dbo.ExpirationDates");
            DropTable("dbo.CreditCards");
            DropTable("dbo.CheckingAccounts");
            DropTable("dbo.CardTypes");
            DropTable("dbo.CalendarDays");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
