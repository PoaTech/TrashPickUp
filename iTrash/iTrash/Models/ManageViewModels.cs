﻿using System.Collections.Generic;
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
        
        public SelectList days { get; set; }
        public int dayId { get; set; }
        public int altDayId { get; set; }
        public bool changePickupDate { get; set; }
        public bool changeAltPickupDate { get; set; }
        public bool removeAltPickupDate { get; set; }
        public string leavedate;
        public ApplicationUser user;
        public string pickupDate;
        public string altPickupDate;
        public void GetUser(string userId, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            user = query.a;
            pickupDate = GetPickupDate(user._PickupDay_ID, db);
            altPickupDate = GetPickupDate(user._AltPickupDay_ID, db);
            //Label leavedate = Page.FindControl("leaveDateInput").Controls. //<---------------THIS
        }
        public string GetPickupDate(int? dayId, ApplicationDbContext db)
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
        public void SetNewPickupDate(string userId, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            var user = query.a;
            user._PickupDay_ID = dayId;
            db.SaveChanges();
        }
        public void SetNewAltPickupDate(string userId, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            var user = query.a;
            user._AltPickupDay_ID = altDayId;
            db.SaveChanges();
        }
        public void RemoveAltPickupDate(string userId, ApplicationDbContext db)
        {
            var query = (from a in db.Users
                         where a.Id == userId
                         select new { a }).Single();
            var user = query.a;
            user._AltPickupDay_ID = null;
            db.SaveChanges();
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

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

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

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}