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
        public IEnumerable<Admin> getAdmins()
        {
            var admin = _ciPlatformDbContext.a;
            return Users;
        }
        bool IAdminRepository.validateadmin(string Email)
        {
            return _ciPlatformDbContext.Admin
        }
    }
}
