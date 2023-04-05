﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class EditPasswordModel
    {
        [Required]
        public string Oldpassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Please enter more than 8 characters")]
        public string Newpassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Please enter more than 8 characters")]
        public string ConfirmPassword { get; set; }

        [Required]
        public int userid { get; set; }

    }
}
