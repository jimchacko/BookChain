using System.ComponentModel.DataAnnotations;

namespace BookChain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "Employee Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Letters only please")]
        public string Name { get; set; }
        [Display(Name = "Contact Address")]
        public string Address { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        public string Company { get; set; }
        public string Designation { get; set; }
    }

}