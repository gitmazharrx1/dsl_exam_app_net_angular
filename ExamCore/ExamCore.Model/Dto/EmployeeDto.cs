using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCore.Model.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, provide first name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Please, provide last name.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be between 2 and 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please, provide a phone number.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please, provide an email address.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, select a country.")]
        public int CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}
