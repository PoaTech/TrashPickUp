using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrash.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string _FirstName { get; set; }
        public string _LastName { get; set; }
        [ForeignKey("address")]
        public int? _Address_ID { get; set; }
        public Address address { get; set; }
        [ForeignKey("billingInfo")]
        public int? _BillingInfo_ID { get; set; }
        public PaymentInfo billingInfo { get; set; }
        [ForeignKey("pickupDay")]
        public int _PickupDay_ID { get; set; }
        public WeekDay pickupDay { get; set; }
        [ForeignKey("altPickupDay")]
        public int? _AltPickupDay_ID { get; set; }
        public WeekDay altPickupDay { get; set; }
        [ForeignKey("leaveDate")]
        public int? _LeaveDate_ID { get; set; }
        public Date leaveDate { get; set; }
        [ForeignKey("returnDate")]
        public int? _ReturnDate_ID { get; set; }
        public Date returnDate { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Truck> Truck { get; set; }
        public DbSet<CalendarDay> CalendarDay { get; set; }
        public DbSet<Month> Month { get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<Date> Date { get; set; }
        public DbSet<CheckingAccount> CheckingAccount { get; set; }
        public DbSet<Zipcode> Zipcode { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<CardType> CardType { get; set; }
        public DbSet<ExpirationDate> ExpirationDate { get; set; }
        public DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<WeekDay> WeekDay { get; set; }

    }
}