using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Domain.Entities
{
    public class CommonFeild
    {
        public Guid UserId { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
    public class User: CommonFeild
    {
        //[Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email id mandatory")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "First is mandatory.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name is allow only 50 character.")]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name is mandatory")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name is allow only 50 character.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile is mandatory.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Mobile is allow only 25 characters.")]
        public string Mobile{ get; set; }

        [Required(ErrorMessage = "Gender is mandatory.")]
        [RegularExpression(@"^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is mandatory.")]
        [DataType(DataType.DateTime)]
        public DateTime DoB { get; set; }
        public bool? isVerified { get; set; }
        public UserRole Role { get; set; }
        public UserDetail UserDetail { get; set; }
    }

    public class UserRole: CommonFeild
    {
        [Required(ErrorMessage = "Role is mandatory.")]
        [StringLength(25, ErrorMessage = "Role is allow maximum 25 char.")]
        public string Role { get; set; }
    }
    public class UserDetail : CommonFeild
    {
        [Required(ErrorMessage = "Passowrd is mandatory.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }
    }

    public class AuthenticationResponse: AuthenticationRequest
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }

    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Email is mandatory.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

    }
}
