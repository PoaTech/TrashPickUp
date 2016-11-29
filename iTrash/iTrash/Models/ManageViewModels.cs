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
        public void SetLeaveDate(string userId)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            query.a._LeaveDate_ID = CreateDate();
            db.SaveChanges();
        }
        public int? CreateDate()
        {
            if(CheckDate())
            {
                var query = (from a in db.Users
                             where a.Id == userId
                             select new { a }).Single();
                query.a._LeaveDate_ID = CreateDate();
            }
        }
        public bool CheckDate()
        {
            var query = from a in db.Date
                        where a._Day = 
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