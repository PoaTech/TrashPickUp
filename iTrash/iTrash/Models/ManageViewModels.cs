using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iTrash.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public ApplicationDbContext db;
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public int role;
        public string formattedAddress = "";
        public bool IsOnlyDriver(string userId, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            return (query.a.role == 1);
        }
        public void DisplayAddress(string userId)
        {
            var user = (from a in db.Users
                        where a.Id == userId
                        select new { a }).Single();
            int? addressId = user.a._Address_ID;
            var address = (from a in db.Address
                           where a._ID == addressId
                           select new { a }).Single();
            var city = (from a in db.City
                        where a._ID == address.a._City
                        select new { a }).Single();
            var state = (from a in db.State
                         where a._ID == city.a._State
                         select new { a }).Single();
            string formattedAddress = String.Format("{0} {1}, {2}, {3}", address.a._StreetAddress1, address.a._StreetAddress2, city.a._City, state.a._State);
        }
        public void GetData(string userID, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userID
                         select new { a }).Single();
            role = query.a.role;
            DisplayAddress(userID, db);
        }
        public void DisplayAddress(string userId, ApplicationDbContext db)
        {
            var user = (from a in db.Users
                        where a.Id == userId
                        select new { a }).Single();
            int? addressId = user.a._Address_ID;
            var address = (from a in db.Address
                           where a._ID == addressId
                           select new { a }).Single();
            var city = (from a in db.City
                        where a._ID == address.a._City
                        select new { a }).Single();
            var state = (from a in db.State
                         where a._ID == city.a._State
                         select new { a }).Single();
            formattedAddress = String.Format("{0} {1}, {2}, {3}", address.a._StreetAddress1, address.a._StreetAddress2, city.a._City, state.a._State);
        }
    }

    public class PickupSettingsViewModel
    {
        public bool changeAltPickupDate { get; set; }
        public bool removeAltPickupDate { get; set; }
        public bool removeSuspensionDates { get; set; }
        public string returnDateInput { get; set; }
        public bool changePickupDate { get; set; }
        public string leaveDateInput { get; set; }
        public SelectList days { get; set; }
        public int altDayId { get; set; }
        public int dayId { get; set; }
        public ApplicationUser user = new ApplicationUser();
        public string altPickupDate;
        public DateTime returnDate;
        public DateTime leaveDate;
        public string pickupDate;
        public int role;
        ApplicationDbContext db = new ApplicationDbContext();

        public void GetData(string userId, ApplicationDbContext db)
        {
            this.db = db;
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            user = query.a;
            days = new SelectList(db.WeekDay, "_ID", "_Day");
            pickupDate = GetPickupDate(user._PickupDay_ID);
            altPickupDate = GetPickupDate(user._AltPickupDay_ID);
            role = user.role;
            if (user._ReturnDate_ID != null && user._LeaveDate_ID != null)
            {
                returnDate = GetDate(user._ReturnDate_ID);
                leaveDate = GetDate(user._LeaveDate_ID);
            }
        }
        public string GetPickupDate(int? dayId)
        {
            if (dayId != null)
            {
                var query = (from a in db.WeekDay
                             where a._ID == dayId
                             select new { a._Day }).Single();
                return query._Day;
            }
            else
            {
                return "None";
            }
        }
        public void SetNewPickupDate(string userId)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            query.a._PickupDay_ID = dayId;
            db.SaveChanges();
        }
        public void SetNewAltPickupDate(string userId)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            query.a._AltPickupDay_ID = altDayId;
            db.SaveChanges();
        }
        public void RemoveAltPickupDate(string userId)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            query.a._AltPickupDay_ID = null;
            db.SaveChanges();
        }
        public void SetSuspensionDates(string userId)
        {
            int leaveYear;
            int leaveMonth;
            int leaveDay;
            int returnYear;
            int returnMonth;
            int returnDay;
            string month1 = leaveDateInput.Substring(6, 2);
            int.TryParse(leaveDateInput.Substring(0, 4), out leaveYear);
            int.TryParse(leaveDateInput.Substring(5, 2), out leaveMonth);
            int.TryParse(leaveDateInput.Substring(8, 2), out leaveDay);
            int.TryParse(returnDateInput.Substring(0, 4), out returnYear);
            int.TryParse(returnDateInput.Substring(5, 2), out returnMonth);
            int.TryParse(returnDateInput.Substring(8, 2), out returnDay);
            try
            {
                var user = (from a in db.Users
                             where a.Id == userId
                             select new { a }).Single();
                user.a._LeaveDate_ID = CreateDate(leaveYear, leaveMonth, leaveDay);
                user.a._ReturnDate_ID = CreateDate(returnYear, returnMonth, returnDay);
                db.SaveChanges();
        }
            catch
            {

            }
        }
        public int? CreateDate(int year, int month, int day)
        {
            if (CheckDate(year,month,day))
            {
                var date = (from a in db.Date
                             where a._Day == day && a._Month == month && year == ((
                             from b in db.Year
                             where b._Year == year
                             select new { b }).Single().b._Year)
                             select new { a._ID }).Single();
                return date._ID;
            }
            else
            {
                var query = (from a in db.Year
                             where a._Year == year
                             select new { a });
                if (query.Count() == 0)
                {
                    Year newYear = new Year();
                    newYear._Year = year;
                    db.Year.Add(newYear);
                    db.SaveChanges();
                }
                Date newDate = new Date();
                newDate._Day = day;
                newDate._Month = month;
                newDate._Year = (from a in db.Year
                                 where a._Year == year
                                 select new { a }).Single().a._ID;
                db.Date.Add(newDate);
                db.SaveChanges();
                return newDate._ID;
            }
        }
        public bool CheckDate(int year, int month, int day)
        {
            try
            {
                var query = (from a in db.Date
                            where a._Day == day && a._Month == month && year == ((
                            from b in db.Year
                            where b._Year == year
                            select new { a }).Single().a._Year)
                            select new { a._ID }).Single();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DateTime GetDate(int? dateId)
        {
            var queryDay = (from a in db.CalendarDay
                            where a._ID == ((from b in db.Date
                                             where b._ID == dateId
                                             select new { b._Day }).FirstOrDefault()._Day)
                            select new { a._Day }).First();

            var queryMonth = (from a in db.Month
                              where a._ID == ((from b in db.Date
                                               where b._ID == dateId
                                               select new { b._Month }).FirstOrDefault()._Month)
                              select new { a._Month }).First();

            var queryYear = (from a in db.Year
                             where a._ID == ((from b in db.Date
                                              where b._ID == dateId
                                              select new { b._Year }).FirstOrDefault()._Year)
                             select new { a._Year }).First();

            int day = queryDay._Day;
            int month = queryMonth._Month;
            int year = queryYear._Year;

            return new DateTime(year, month, day);
        }
        public void RemoveSuspensionDates(string userId)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            query.a._LeaveDate_ID = null;
            query.a._ReturnDate_ID = null;
            db.SaveChanges();
        }
    }
    public class BillingInfoSettingsViewModel
    {
        public ApplicationUser user;
        public int role;
        public decimal balance;

        public void GetData(string userID, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userID
                         select new { a }).Single();
            user = query.a;
            role = user.role;
            balance = user.balance;
        }
    }
    public class RouteViewModel
    {
        public SelectList trucks { get; set; }
        public List<string> addressesOnRoute = new List<string>();
        public int truckId { get; set; }
        public ApplicationDbContext db;
        public ApplicationUser user;
        public int route = 0;
        public int role;

        public void GetData(string userID, ApplicationDbContext db)
        {
            trucks = new SelectList(db.Truck, "_ID", "_TruckNumber");
            this.db = db;
            var query = (from a in db.Users
                         where a.Id == userID
                         select new { a }).Single();
            user = query.a;
            role = user.role;
            GetRoutes();
        }
        public void GetRoutes()
        {
            var trucksWithRoute = (from a in db.Truck
                      where a._Zipcode != null
                      select a).ToList();
            trucks = new SelectList(trucksWithRoute, "_ID", "_TruckNumber");
        }
        public void GetRouteInfo()
        {
            var query = (from a in db.Truck
                         where a._ID == route
                         select a).Single();
            int? truckZipcode = query._Zipcode;
            List<string> usersOnRoute = GetUsersOnRoute(truckZipcode);
            foreach (string user in usersOnRoute)
            {
                AddAddress(user);
            }
            addressesOnRoute = addressesOnRoute.Distinct().ToList();
        }
        public List<string> GetUsersOnRoute(int? truckZipcode)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<string> usersOnRoute = new List<string>();
            var usersId = (from b in db.Pickup
                         select b._User).ToList();
            foreach (string userId in usersId)
            {
                var user = (from a in db.Users
                            where a.Id == userId
                            select a).Single();
                users.Add(user);
            }
            foreach (ApplicationUser user in users)
            {
                int zipcode = (from a in db.Address
                               where a._ID == user._Address_ID
                               select a._Zipcode).Single();
                if (zipcode == truckZipcode)
                {
                    usersOnRoute.Add(user.Id);
                }
            }
            return usersOnRoute;
        }
        public void AddAddress(string userId)
        {

            var user = (from a in db.Users
                             where a.Id == userId
                             select new { a }).Single();
            int? addressId = user.a._Address_ID;
            var address = (from a in db.Address
                           where a._ID == addressId
                           select new { a }).Single();
            var city = (from a in db.City
                           where a._ID == address.a._City
                           select new { a }).Single();
            var state = (from a in db.State
                         where a._ID == city.a._State
                         select new { a }).Single();
            string formattedAddress = String.Format("{0} {1}, {2}, {3}", address.a._StreetAddress1, address.a._StreetAddress2, city.a._City, state.a._State).Replace(" ", "+");
            addressesOnRoute.Add(formattedAddress);
        }
        public string GetApiLink()
        {
            GetRouteInfo();
            string routeParameters = GetRouteParameters();
            return routeParameters;
        }
        public string GetRouteParameters()
        {
            string link = "https://www.google.com/maps/embed/v1/directions?key=AIzaSyDgaGKD2x4WZF367-tX6vUmF06vUXT3t4A&origin=";
            string start = addressesOnRoute[0];
            string destination = addressesOnRoute[addressesOnRoute.Count - 1];
            string waypoints = "";
            if (addressesOnRoute.Count == 1)
            {
                link = "https://www.google.com/maps/embed/v1/directions?key=AIzaSyDgaGKD2x4WZF367-tX6vUmF06vUXT3t4A&q=";
                return link + start;
            }
            else if (addressesOnRoute.Count == 2)
            {
                return link + start + "&destination=" + destination;
            }
            else
            {
                for (int i = 1; i < addressesOnRoute.Count - 1; i++)
                {
                    if (i != 1)
                    {
                        waypoints += "|";
                    }
                    waypoints += addressesOnRoute[i];
                }
                return link + start + "&destination=" + destination + "&waypoints=" + waypoints;
            }
        }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }
    //Adam's work
        //Here lies Adam's work. It was once great. Until Max ruined it.

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeAddressViewModel
    {
        [Required]
        [Display(Name = "Street Address Line 1")]
        public string _StreetAddress1 { get; set; }

        [Display(Name = "Street Address Line 2")]
        public string _StreetAddress2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string _CityID { get; set; }

        [Required]
        [Display(Name = "State")]
        public int _StateID { get; set; }
        public SelectList states { get; set; }

        [Required]
        [Display(Name = "Zipcode")]
        public int? _ZipcodeID { get; set; }

        
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}