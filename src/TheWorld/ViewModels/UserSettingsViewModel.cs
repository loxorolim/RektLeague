using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class UserSettingsViewModel
    {
        [StringLength(20, MinimumLength = 5)]
        public string Login { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile Image { get; set; }


        //[StringLength(20, MinimumLength = 8)]
        //public string Password { get; set; }
        //[Required]
        //public string PasswordConfirmation { get; set; }
    }
}
