using CIPlatform.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IAdminRepository
    {
        public Boolean validateadmin(string adminemail);

        public Boolean validateadmincred(string adminemail,string adminpassword);

        public Admin findadmin(string adminemail);
    }
}
