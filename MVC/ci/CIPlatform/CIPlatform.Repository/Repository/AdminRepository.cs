using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using CIPlatform.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
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
        //========================
        //Admin login 
        //==========================
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
        //========================
        //Admin user crud
        //==========================
        AdminPageList<User> IAdminRepository.GetUsers(string searchtext, int pageNumber, int pageSize)
        {
            IEnumerable<User> userPages;
            if (searchtext != null)
            {
                userPages = _ciPlatformDbContext.Users.Where(page => page.DeletedAt == null && page.Status == true).Where
                    (x => x.FirstName.Contains(searchtext) || x.LastName.Contains
                 (searchtext) || x.Department.Contains(searchtext));
            }
            else
            {
                userPages = _ciPlatformDbContext.Users.Where
                    (page => page.DeletedAt == null && page.Status == true);

            }
            var totalCounts = userPages.Count();
            var records = userPages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<User>(records, totalCounts);

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
        //========================
        //Admin cms crud
        //==========================
        AdminPageList<CmsPage> IAdminRepository.GetCmspages(string searchText, int pageNumber, int pageSize)
        {
            IEnumerable<CmsPage> CmsPages;
            if (searchText != null)
            {
                CmsPages = _ciPlatformDbContext.CmsPages.Where(page => page.DeletedAt == null && page.Status == true).Where
                    (x => x.Title.Contains(searchText)).ToList();
            }
            else
            {
                CmsPages = _ciPlatformDbContext.CmsPages.Where
                    (page => page.DeletedAt == null && page.Status == true);
            }
            var totalCounts = CmsPages.Count();
            var records = CmsPages.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<CmsPage>(records, totalCounts);
        }
        void IAdminRepository.AddCmsAdmin(CmsPage CMS)
        {
            _ciPlatformDbContext.Add(CMS);
            _ciPlatformDbContext.SaveChanges();
        }

        CmsPage IAdminRepository.GetCmsAdmin(long cmsId)
        {
            return _ciPlatformDbContext.CmsPages.Where(CmsPage => CmsPage.CmsPageId == cmsId).FirstOrDefault();
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
        //========================
        //Admin story crud
        //==========================
        AdminPageList<Story> IAdminRepository.GetStoryAdmin(string searchText, int pageNumber, int pageSize)
        {
            IEnumerable<Story> Story;
            if (searchText != null)
            {
                Story = _ciPlatformDbContext.Stories.Where(page => page.DeletedAt == null && page.Status !="approved" && page.Status != "rejected").
                    Include(user => user.User).Include(mission => mission.Mission).Where
                    (stories => stories.Title.Contains(searchText)||stories.Mission.Title.Contains(searchText)
                    ||stories.User.FirstName.Contains(searchText)||stories.User.LastName.Contains(searchText)).ToList();
            }
            else
            {
                Story = _ciPlatformDbContext.Stories.Where
                    (page => page.DeletedAt == null && page.Status != "approved" && page.Status != "rejected").Include
                    (User => User.User).Include(x=>x.Mission).ToList();
            }
            var totalCounts = Story.Count();
            var records = Story.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<Story>(records, totalCounts);
        }
        Story IAdminRepository.GetstoryForApprove(long storyId)
        {
            return _ciPlatformDbContext.Stories.Where(story=>story.StoryId == storyId).FirstOrDefault();
        }
        void IAdminRepository.ApproveStory(Story story)
        {
            _ciPlatformDbContext.Stories.Update(story);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.DeleteStory(Story story)
        {
            _ciPlatformDbContext.Stories.Update(story);
            _ciPlatformDbContext.SaveChanges();
        }
        //=============================
        //Admin missionapplication crud
        //==============================
        AdminPageList<MissionApplication> IAdminRepository.GetMissionApplicationAdmin(string searchText, int pageNumber, int pageSize)
        {
            IEnumerable<MissionApplication> applications;
            if (searchText != null)
            {
                applications = _ciPlatformDbContext.MissionApplications.Where(page => page.DeletedAt == null && page.ApprovalStatus != "approved" && page.ApprovalStatus != "rejected").Include
                     (user => user.User).Include(mission => mission.Mission).Where
                     (MissionApplication => MissionApplication.Mission.Title.Contains(searchText)
                 || MissionApplication.User.FirstName.Contains(searchText)
                 || MissionApplication.User.LastName.Contains(searchText)).ToList();
            }
            else
            {
                applications= _ciPlatformDbContext.MissionApplications.Where(page => page.DeletedAt == null && page.ApprovalStatus != "approved" && page.ApprovalStatus != "rejected").Include(Mission => Mission.Mission).Include(User => User.User).ToList();
            }
            var totalCounts = applications.Count();
            var records = applications.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<MissionApplication>(records, totalCounts);
        }
        MissionApplication IAdminRepository.GetApplicationForApprove(long missionAppId)
        {
            return _ciPlatformDbContext.MissionApplications.Where
                (application => application.MissionApplicationId == missionAppId).FirstOrDefault();
        }
        void IAdminRepository.ApproveApplication(MissionApplication application)
        {
            _ciPlatformDbContext.Update(application);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.DeleteApplication(MissionApplication application)
        {
            _ciPlatformDbContext.Update(application);
            _ciPlatformDbContext.SaveChanges();
        }
        //=============================
        //Admin mission theme crud
        //==============================
        AdminPageList<MissionTheme> IAdminRepository.GetMissionThemeAdmin(string searchText, int pageNumber, int pageSize)
        {
            IEnumerable<MissionTheme> themes;
            if (searchText != null)
            {
                themes = _ciPlatformDbContext.MissionThemes.Where
                   (MissionThemes => MissionThemes.Title.Contains(searchText) && MissionThemes.DeletedAt == null).ToList();
            }
            else
            {
                themes= _ciPlatformDbContext.MissionThemes.Where(x=>x.DeletedAt==null).ToList();
            }
            var totalCounts = themes.Count();
            var records = themes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<MissionTheme>(records, totalCounts);
        }
        void IAdminRepository.AddThemeAdmin(MissionTheme theme)
        {
            _ciPlatformDbContext.Add(theme);
            _ciPlatformDbContext.SaveChanges();
        }
        MissionTheme IAdminRepository.GetThemeAdmin(long themeId)
        {
            return _ciPlatformDbContext.MissionThemes.Where(x=>x.MissionThemeId == themeId).FirstOrDefault();
        }
        void IAdminRepository.EditThemeAdmin(MissionTheme theme)
        {
            _ciPlatformDbContext.Update(theme);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.DeleteThemeAdmin(MissionTheme theme)
        {
            _ciPlatformDbContext.Update(theme);
            _ciPlatformDbContext.SaveChanges();
        }
        //=============================
        //Admin skill crud
        //==============================
        AdminPageList<Skill> IAdminRepository.GetSkillAdmin(string searchText, int pageNumber, int pageSize)
        {
            IEnumerable<Skill> skills;
            if (searchText != null)
            {

                skills = _ciPlatformDbContext.Skills.Where
                    (skills => skills.SkillName.Contains(searchText) && skills.DeletedAt == null).ToList();
            }
            else
            {
                skills = _ciPlatformDbContext.Skills.Where(skills => skills.DeletedAt == null).ToList();
            }
            var totalCounts = skills.Count();
            var records = skills.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<Skill>(records, totalCounts);
        }
        void IAdminRepository.AddSkillAdmin(Skill skill)
        {
            _ciPlatformDbContext.Skills.Add(skill);
            _ciPlatformDbContext.SaveChanges();
        }
        Skill IAdminRepository.GetSkill(long skillId)
        {
            return _ciPlatformDbContext.Skills.Where(x=>x.SkillId == skillId).FirstOrDefault(); 
        }
        void IAdminRepository.EditSkill(Skill skill)
        {
            _ciPlatformDbContext.Skills.Update(skill);
            _ciPlatformDbContext.SaveChanges();
        }
        void IAdminRepository.DeleteSkill(Skill skill)
        {
            _ciPlatformDbContext.Skills.Update(skill);
            _ciPlatformDbContext.SaveChanges();
        }
        AdminPageList<Banner> IAdminRepository.GetBanner(string searchText, int pageNumber, int pageSize)
        {
            IEnumerable<Banner> Banner;
            if (searchText != null)
            {

                Banner = _ciPlatformDbContext.Banners.Where
                    (Banner=>Banner.Text.Contains((searchText)) && Banner.DeletedAt == null).ToList();
            }
            else
            {
                Banner = _ciPlatformDbContext.Banners.Where(Banner => Banner.DeletedAt == null).ToList();
            }
            var totalCounts = Banner.Count();
            var records = Banner.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new AdminPageList<Banner>(records, totalCounts);
        }
        void IAdminRepository.AddBannerAdmin(Banner banner1)
        {
            _ciPlatformDbContext.Add(banner1);
            _ciPlatformDbContext.SaveChanges();
        }
        Banner IAdminRepository.GetBanner(long bannerId)
        {
            return _ciPlatformDbContext.Banners.Where(banner=>banner.BannerId==bannerId).FirstOrDefault();   
        }
        void IAdminRepository.EditBannerAdmin(Banner banner1)
        {
            _ciPlatformDbContext.Update(banner1);
            _ciPlatformDbContext.SaveChanges();
        }
    }
}
