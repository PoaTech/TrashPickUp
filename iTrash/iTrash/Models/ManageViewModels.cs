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
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }
    public class PersonalSettingsViewModel
    {
        public ApplicationUser user;

        public void GetUser(string userID, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userID
                         select new { a }).Single();
            user = query.a;
        }
    }

    public class PickupSettingsViewModel
    {
        public bool changeAltPickupDate { get; set; }
        public bool removeAltPickupDate { get; set; }
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
        ApplicationDbContext db;

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
            int.TryParse(leaveDateInput.Substring(0, 4), out leaveYear);
            int.TryParse(leaveDateInput.Substring(6, 2), out leaveMonth);
            int.TryParse(leaveDateInput.Substring(8, 2), out leaveDay);
            int.TryParse(returnDateInput.Substring(0, 4), out returnYear);
            int.TryParse(returnDateInput.Substring(6, 2), out returnMonth);
            int.TryParse(returnDateInput.Substring(8, 2), out returnDay);
            var user = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            user.a._LeaveDate_ID = CreateDate(leaveYear, leaveMonth, leaveDay);
            user.a._ReturnDate_ID = CreateDate(returnYear, returnMonth, returnDay);
            db.SaveChanges();
        }
        public int? CreateDate(int year, int month, int day)
        {
            int.TryParse(leaveDateInput.Substring(0, 4), out year);
            int.TryParse(leaveDateInput.Substring(6, 2), out month);
            int.TryParse(leaveDateInput.Substring(9, 2), out day);
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
                                 select new { a }).Single().a._Year;
                db.Date.Add(newDate);
                db.SaveChanges();
                return newDate._ID;
            }
        }
        public bool CheckDate(int year, int month, int day)
        {
            var query = (from a in db.Date
                        where a._Day == day && a._Month == month && year == ((
                        from b in db.Year
                        where b._Year == year
                        select new { a }).Single().a._Year)
                        select new { a._ID });
            return (query.Count() == 0);
        }
        public DateTime GetDate(int? dateId)
        {
            var queryDay = (from a in db.CalendarDay
                            where a._ID == ((from b in db.Date
                                             where b._ID == dateId
                                             select new { b._Day }).Single()._Day)
                            select new { a._Day }).Single();

            var queryMonth = (from a in db.Month
                              where a._ID == ((from b in db.Date
                                               where b._ID == dateId
                                               select new { b._Month }).Single()._Month)
                              select new { a._Month }).Single();

            var queryYear = (from a in db.Year
                             where a._ID == ((from b in db.Date
                                              where b._ID == dateId
                                              select new { b._Year }).Single()._Year)
                             select new { a._Year }).Single();

            int day = queryDay._Day;
            int month = queryMonth._Month;
            int year = queryYear._Year;

            return new DateTime(year, month, day);
        }
    }
    public class BillingInfoSettingsViewModel
    {
        public ApplicationUser user;

        public void GetUser(string userID, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userID
                         select new { a }).Single();
            user = query.a;
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