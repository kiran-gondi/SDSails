namespace SDSailsModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserInfo
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter email addess.")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter your valid email address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
    }
}
