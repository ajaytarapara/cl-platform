using CIPlatform.Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class Admin_Banner_crudModel
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public List<Banner>? Banner { get; set; }
        public IFormFile? bannerimg { get; set; }
        [Required]
        public int? sortorder { get; set; }
        [Required]
        public string? text { get; set; } 
        public int? bannerid { get; set; }
    }
}
