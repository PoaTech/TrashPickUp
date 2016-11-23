using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace iTrash.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string _FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string _LastName { get; set; }

        [Required]
        [Display(Name ="Street Address Line 1")]
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
        public int _ZipcodeID { get; set; }

        [Display(Name = "Pickup day")]
        public int _dayID { get; set; }
        public SelectList days { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class AddressCreationModel
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public int GetAddressID(string addressLine1, string addressLine2, string city, int state, int zipcode)
        {
            if (!CityExists(city, state))
            {
                CreateCity(city, state);
            }
            if (!ZipcodeExists(zipcode))
            {
                CreateZipcode(zipcode);
            }
            if (!AddressExists(addressLine1, addressLine2, city, state, zipcode))
            {
                CreateAddress(addressLine1, addressLine2, city, state, zipcode);
            }
            var cityID = from a in db.City
                         where a._City == city && a._State == state
                         select a._ID;
            var zipcodeID = from a in db.Zipcode
                            where a._Zipcode == zipcode
                            select a._ID;
            var addressID = from a in db.Address
                            where a._City == cityID.First<int>() && a._Zipcode == zipcodeID.First<int>() && a._StreetAddress1 == addressLine1 && a._StreetAddress2 == addressLine2
                            select a._ID;
            return addressID.First<int>();
        }
        private bool CityExists(string city, int state)
        {
            var cityID = from a in db.City
                         where a._City == city && a._State == state
                         select a;
            return (cityID == null);
        }
        private void CreateCity(string city, int state)
        {
            var newCity = new City();
            newCity._City = city;
            newCity._State = state;
            db.City.Add(newCity);
        }
        private bool ZipcodeExists(int zipcode)
        {
            var zipcodeID = from a in db.Zipcode
                            where a._Zipcode == zipcode
                            select a;
            return (zipcodeID == null);
        }
        private void CreateZipcode(int zipcode)
        {
            var newZipcode = new Zipcode();
            newZipcode._Zipcode = zipcode;
            db.Zipcode.Add(newZipcode);
        }
        private bool AddressExists(string addressLine1, string addressLine2, string city, int state, int zipcode)
        {
            var cityID = from a in db.City
                         where a._City == city && a._State == state
                         select a._ID;
            var zipcodeID = from a in db.Zipcode
                            where a._Zipcode == zipcode
                            select a._ID;
            var addressID = from a in db.Address
                            where a._City == cityID.First<int>() && a._Zipcode == zipcodeID.First<int>() && a._StreetAddress1 == addressLine1 && a._StreetAddress2 == addressLine2
                            select a;
            return (addressID == null);
        }
        private void CreateAddress(string addressLine1, string addressLine2, string city, int state, int zipcode)
        {
            var cityID = from a in db.City
                         where a._City == city && a._State == state
                         select a._ID;
            var zipcodeID = from a in db.Zipcode
                            where a._Zipcode == zipcode
                            select a._ID;
            var newAddress = new Address();
            newAddress._StreetAddress1 = addressLine1;
            newAddress._StreetAddress2 = addressLine2;
            newAddress._Zipcode = zipcodeID.First<int>();
            newAddress._City = cityID.First<int>();
            db.Address.Add(newAddress);
        }
    }
}