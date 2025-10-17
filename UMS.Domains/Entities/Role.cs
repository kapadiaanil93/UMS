using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMS.Domains.Entities
{
    public class Role: CommonFeild
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

    }    

    public class RoleSaveEntity: CommonFeild
    {
        public int RoleId { get; set; }
        [Required(ErrorMessage = "Role is mandatory.")]
        [StringLength(25, MinimumLength =5, ErrorMessage = "Role is allow maximum 25 and minmum 5 characters.")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Description is mandatory.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description is allow maximum 100 and minmum 5 characters.")]
        public string Description { get; set; }
    }

    public class RoleUpdateEntity : CommonFeild
    {
        [Required(ErrorMessage ="RoleId is mandatory.")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Role is mandatory.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Role is allow maximum 25 and minmum 5 characters.")]
        public string RoleName { get; set; }

        [Required(ErrorMessage = "Description is mandatory.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description is allow maximum 100 and minmum 5 characters.")]
        public string Description { get; set; }
    }
}
