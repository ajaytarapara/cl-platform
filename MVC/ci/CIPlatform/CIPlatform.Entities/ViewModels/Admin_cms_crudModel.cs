using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class Admin_cms_crudModel
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public int? CmsId { get; set; }
        public List<CmsPage>? CmsPage { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Slug { get; set; }
        public bool? Status { get; set; }
    }
}
