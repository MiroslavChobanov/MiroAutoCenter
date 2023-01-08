using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiroAutoCenter.Core.Models.Users
{
    public class UserEditModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [Display(Name = "Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "{0} is a required field.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }
    }
}
