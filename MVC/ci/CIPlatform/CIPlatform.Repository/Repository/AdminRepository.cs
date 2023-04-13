using CIPlatform.Entities.DataModels;
using CIPlatform.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly CIPlatformDbContext _ciPlatformDbContext;

        public AdminRepository(CIPlatformDbContext cIPlatformDbContext)
        {
            _ciPlatformDbContext = cIPlatformDbContext;
        }
        bool IAdminRepository.validateadmin(string adminemail)
        {
            return _ciPlatformDbContext.Admins.Any(x => x.Email == adminemail);
        }
        bool IAdminRepository.validateadmincred(string adminemail, string adminpassword)
        {
            return _ciPlatformDbContext.Admins.Any(x => x.Email == adminemail && x.Password == adminpassword);
        }
        Admin IAdminRepository.findadmin(string adminemail)
        {
            return _ciPlatformDbContext.Admins.Where(x => x.Email == adminemail).FirstOrDefault();
        }
    }
}
