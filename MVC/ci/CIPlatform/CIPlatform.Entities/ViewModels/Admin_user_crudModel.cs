﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModels
{
    public class Admin_user_crudModel
    {
        public string? adminname { get; set; }
        public string? adminavatar { get; set; }
        public Admin_NavbarModel? Navbar { get; set; }
    }
}
