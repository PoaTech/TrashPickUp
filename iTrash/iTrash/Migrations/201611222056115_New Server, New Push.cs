namespace iTrash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewServerNewPush : DbMigration
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
                "dbo.Zipcodes",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Zipcode = c.String(),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        _FirstName = c.String(),
                        _LastName = c.String(),
                        _Address_ID = c.Int(nullable: false),
                        _BillingInfo_ID = c.Int(nullable: false),
                        _PickupDay_ID = c.Int(nullable: false),
                        _AltPickupDay_ID = c.Int(nullable: false),
                        _LeaveDate_ID = c.Int(nullable: false),
                        _ReturnDate_ID = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t._Address_ID)
                .ForeignKey("dbo.WeekDays", t => t._AltPickupDay_ID)
                .ForeignKey("dbo.PaymentInfoes", t => t._BillingInfo_ID)
                .ForeignKey("dbo.Dates", t => t._LeaveDate_ID)
                .ForeignKey("dbo.WeekDays", t => t._PickupDay_ID)
                .ForeignKey("dbo.Dates", t => t._ReturnDate_ID)
                .Index(t => t._Address_ID)
                .Index(t => t._BillingInfo_ID)
                .Index(t => t._PickupDay_ID)
                .Index(t => t._AltPickupDay_ID)
                .Index(t => t._LeaveDate_ID)
                .Index(t => t._ReturnDate_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.WeekDays",
                c => new
                    {
                        _ID = c.Int(nullable: false, identity: true),
                        _Day = c.String(),
                    })
                .PrimaryKey(t => t._ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "_ReturnDate_ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUsers", "_PickupDay_ID", "dbo.WeekDays");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "_LeaveDate_ID", "dbo.Dates");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "_BillingInfo_ID", "dbo.PaymentInfoes");
            DropForeignKey("dbo.AspNetUsers", "_AltPickupDay_ID", "dbo.WeekDays");
            DropForeignKey("dbo.AspNetUsers", "_Address_ID", "dbo.Addresses");
            DropForeignKey("dbo.Trucks", "_Zipcode", "dbo.Zipcodes");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
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
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "_ReturnDate_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_LeaveDate_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_AltPickupDay_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_PickupDay_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_BillingInfo_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "_Address_ID" });
            DropIndex("dbo.Trucks", new[] { "_Zipcode" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
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
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.WeekDays");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Trucks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PaymentInfoes");
            DropTable("dbo.Dates");
            DropTable("dbo.Years");
            DropTable("dbo.Months");
            DropTable("dbo.ExpirationDates");
            DropTable("dbo.CreditCards");
            DropTable("dbo.CheckingAccounts");
            DropTable("dbo.CardTypes");
            DropTable("dbo.CalendarDays");
            DropTable("dbo.Zipcodes");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
