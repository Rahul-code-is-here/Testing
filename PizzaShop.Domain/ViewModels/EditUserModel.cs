using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PizzaShop.Domain.DataModels;

namespace PizzaShop.Domain.ViewModels
{
    public class EditUserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        // [RegularExpression(@"^[A-Za-z][A-Za-z\s]*$", ErrorMessage = "First Name must start with a letter and contain only letters and spaces")]

        [MaxLength(10)]
        [RegularExpression(@"^\S+$", ErrorMessage = "No whiteSpaces are allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        // [RegularExpression(@"^[A-Za-z][A-Za-z\s]*$", ErrorMessage = "First Name must start with a letter and contain only letters and spaces")]
        [RegularExpression(@"^\S+$", ErrorMessage = "No whiteSpaces are allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"^\S+$", ErrorMessage = "No whiteSpaces are allowed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        // [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Password Must contain Special Symbol, Number,Alphabet")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$", ErrorMessage = "Password eg.Abc@123")]
        public string Password { get; set; }

        public string status { get; set; }
        [Required]
        public int RoleId { get; set; }

        public int CountryID { get; set; }
        public int StateID { get; set; }
        public int CityID { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]

        public string Phone { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Pincode must be exactly 6 digits")]
        public string Zipcode { get; set; }

        public string? PathOfProfilePicture { get; set; }
        public IFormFile? ProfilePicture { get; set; }


        public List<Country> Countries { get; set; }
        public List<State> States { get; set; }
        public List<City> Cities { get; set; }
    }
}