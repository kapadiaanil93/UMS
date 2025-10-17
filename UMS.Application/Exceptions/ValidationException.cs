using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UMS.Application.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException() : base("One or more validations occured") { 
            Errors = new List<string>();
        }
        public List<string> Errors { get; set; }
        public ValidationException(List<ValidationFailure> errors) : this()
        {
            foreach(var error in errors)
            {
                Errors.Add(error.ToString());
            }
        }
    }
}
