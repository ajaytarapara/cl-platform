using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
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
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Slug{ get; set; }
        public string? Status { get; set; }
    }
}
