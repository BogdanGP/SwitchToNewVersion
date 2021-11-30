using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using UmsaCollege.Models;

namespace UmsaCollege.Viewscs
{
    public class RegisterView
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string StudentCode { get;}
        public List<string> Errors { get; set; }
    }
}
