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
            User users = _ciPlatformDbContext.Users.Where(user => user.UserId == userid).FirstOrDefault();
            _ciPlatformDbContext.Remove(users);
            _ciPlatformDbContext.SaveChanges();
        }
        User IAdminRepository.UpdateUserAdminget(long userid)
        {
            User edituser = _ciPlatformDbContext.Users.Where(user => user.UserId == userid).FirstOrDefault();
            return edituser;
        }
        void IAdminRepository.UpdateneedUser(User useredit)
        {
            _ciPlatformDbContext.Update(useredit);
            _ciPlatformDbContext.SaveChanges();

        }
        List<CmsPage> IAdminRepository.GetCmspages(string searchText)
        {
            if (searchText != null)
            {
                List<CmsPage>cms =_ciPlatformDbContext.CmsPages.Where(x => x.Title.Contains(searchText)).ToList();
                return cms;
            }
            else
            {
                return _ciPlatformDbContext.CmsPages.ToList();
            }
        }
        void IAdminRepository.AddCmsAdmin(CmsPage CMS)
        {
            _ciPlatformDbContext.Add(CMS);
            _ciPlatformDbContext.SaveChanges();
        }

        CmsPage IAdminRepository.GetCmsAdmin(long cmsId)
        {
            return _ciPlatformDbContext.CmsPages.Where(CmsPage=>CmsPage.CmsPageId == cmsId).FirstOrDefault();
        }
        void IAdminRepository.UpdateCmsAdmin(CmsPage cms)
        {
            _ciPlatformDbContext.Update(cms);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.DeleteCmsAdmin(long cmsId)
        {
            CmsPage cms = _ciPlatformDbContext.CmsPages.Where(CmsPages => CmsPages.CmsPageId == cmsId).FirstOrDefault();
            _ciPlatformDbContext.Remove(cms);
            _ciPlatformDbContext.SaveChanges();
        }

    }
}
