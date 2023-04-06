﻿using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class EditProfileModel
    {
        [Key]
        public long userid { get; set; }
        [Required]
        public string? useremail { get; set; }
        [Required]
        public string? firstname { get; set; }
        [Required]
        public string? lastname { get; set; }
        public string? avatar { get; set; }
        public string? department { get; set; }
        [Required]
        public string? profiletext { get; set; }
        [Required]
        public string ?whyivol { get; set; }

        public IEnumerable<City> ?cities { get; set; }
        
        public IEnumerable<Country>? country { get; set; }
        [Required]
        public long? cityofuser { get; set; }
        [Required]
        public long? countrofuser { get; set; }
        [Required]
        public string? linkedinurl { get; set; }
        [Required]
        public string? userskills { get; set; }

        public EditPasswordModel password { get; set; }
        public string? email { get; set; }

        [Required]
        public string?subject { get; set; }
        [Required]
        public string? message { get; set; }

        public string? employeeid { get; set; }
        public string? title { get; set; }

    }
}
