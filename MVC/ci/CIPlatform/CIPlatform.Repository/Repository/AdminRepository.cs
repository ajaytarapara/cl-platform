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
        List<User> IAdminRepository.GetUsers(string searchtext)
        {
            if (searchtext != null)
            {
                return _ciPlatformDbContext.Users.Where(x => x.FirstName.Contains(searchtext) || x.LastName.Contains(searchtext) || x.Department.Contains(searchtext)).ToList();
            }
            else
            {
                return _ciPlatformDbContext.Users.ToList();
            }
        }
        void IAdminRepository.AddUserAdmin(User user)
        {
            _ciPlatformDbContext.Users.Add(user);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.RemoveUserAdmin(long userid)
        {
            User users=_ciPlatformDbContext.Users.Where(user=>user.UserId== userid).FirstOrDefault();   
            _ciPlatformDbContext.Remove(users);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.UpdateUserAdmin(long userid)
        {
            User edituser=_ciPlatformDbContext.Users.Where(user=>user.UserId==userid).FirstOrDefault();
            _ciPlatformDbContext.Update(edituser);
            _ciPlatformDbContext.SaveChanges();
        }
    }
}
