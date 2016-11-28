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
        public int? _ZipcodeID { get; set; }
        
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
        
        [Display(Name = "Pickup day")]
        public int _dayID { get; set; }
        public SelectList days { get; set; }

        public RegisterViewModel()
        {

        }

        public RegisterViewModel(ApplicationDbContext db)
        {
            
        }
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
        private int cityID;
        private int stateID;
        private int zipcodeID;
        ApplicationDbContext db;
        public AddressCreationModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public int GetAddressID(string addressLine1, string addressLine2, string city, int state, int? zipcodeNullable)
        {
            stateID = state;
            int zipcode;
            if (zipcodeNullable == null)
            {
                return 0;
            }
            else
            {
                zipcode = (int)zipcodeNullable;
            }
            if (!CityExists(city, state))
            {
                CreateCity(city, state);
            }
            if (!ZipcodeExists(zipcode))
            {
                CreateZipcode(zipcode);
            }
            cityID = GetCityID(city, state);
            zipcodeID = GetZipcodeID(zipcode);
            if (!AddressExists(addressLine1, addressLine2))
            {
                CreateAddress(addressLine1, addressLine2);
            }
            var query = (from a in db.Address
                            where a._City == cityID && a._Zipcode == zipcodeID && a._StreetAddress1 == addressLine1 && a._StreetAddress2 == addressLine2
                            select new { a._ID }).Single();
            int addressID = query._ID;
            return addressID;
        }
        private bool CityExists(string city, int state)
        {
            try
            {
                var query = (from a in db.City
                             where a._City == city && a._State == state
                             select new { a._City, a._State }).Single();
                string _City = query._City;
                int _State = query._State;
                return ((city == _City) && (state == _State));
            }
            catch
            {
                return false;
            }
        }
        private void CreateCity(string city, int state)
        {
            var newCity = new City();
            newCity._City = city;
            newCity._State = state;
            db.City.Add(newCity);
            db.SaveChanges();
        }
        private bool ZipcodeExists(int zipcode)
        {
            try
            {
                var query = (from a in db.Zipcode
                                where a._Zipcode == zipcode
                                select new { a._Zipcode }).Single();
                int _Zipcode = query._Zipcode;
                return (_Zipcode == zipcode);
            }
            catch
            {
                return false;
            }
        }
        private void CreateZipcode(int zipcode)
        {
            var newZipcode = new Zipcode();
            newZipcode._Zipcode = zipcode;
            db.Zipcode.Add(newZipcode);
            db.SaveChanges();
        }
        private bool AddressExists(string addressLine1, string addressLine2)
        {
            try
            {
                var query = (from a in db.Address
                                where a._City == cityID && a._Zipcode == zipcodeID && a._StreetAddress1 == addressLine1 && a._StreetAddress2 == addressLine2
                                select new { a._ID }).Single();
                int addressID = query._ID;
                return (addressID > 0);
            }
            catch
            {
                return false;
            }
        }
        private void CreateAddress(string addressLine1, string addressLine2)
        {
            var newAddress = new Address();
            newAddress._StreetAddress1 = addressLine1;
            newAddress._StreetAddress2 = addressLine2;
            newAddress._Zipcode = zipcodeID;
            newAddress._City = cityID;
            db.Address.Add(newAddress);
            db.SaveChanges();
        }
        private int GetCityID(string city, int state)
        {
            try
            {
                var query = (from a in db.City
                             where a._City == city && a._State == state
                             select new { a._ID }).Single();
                int _ID = query._ID;
                return _ID;
            }
            catch
            {
                return 0;
            }
        }
        private int GetZipcodeID(int zipcode)
        {
            try
            {
                var query = (from a in db.Zipcode
                             where a._Zipcode == zipcode
                             select new { a._ID }).Single();
                int _ID = query._ID;
                return _ID;
            }
            catch
            {
                return 0;
            }
        }
    }
}