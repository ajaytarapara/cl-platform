using CIPlatform.Entities.DataModels;
using CIPlatform.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository.Interface
{
    public interface IAdminRepository
    {
        //========================
        //Admin login
        //==========================
        public Boolean validateadmin(string adminemail);

        public Boolean validateadmincred(string adminemail,string adminpassword);

        public Admin findadmin(string adminemail);
        //========================
        //Admin user crud
        //==========================
        public AdminPageList<User> GetUsers(string searchtext, int pageNumber, int pageSize);

        public void AddUserAdmin(User user);

        public void RemoveUserAdmin(long userid);
        public User UpdateUserAdminget(long userid);

        public void UpdateneedUser(User useredit);
        //========================
        //Admin cms crud
        //==========================
        public AdminPageList<CmsPage> GetCmspages(string searchText, int pageNumber, int pageSize);
        public void AddCmsAdmin(CmsPage CMS);
        public void UpdateCmsAdmin(CmsPage cms);
        public CmsPage GetCmsAdmin(long cmsId);
        public void DeleteCmsAdmin(long cmsId);
        //========================
        //Admin story crud
        //==========================
        public AdminPageList<Story> GetStoryAdmin(string searchText, int pageNumber, int pageSize);
        public Story GetstoryForApprove(long storyId);
        public void ApproveStory(Story story);
        public void DeleteStory(Story story);
        //========================
        //Admin Application crud
        //==========================
        public AdminPageList<MissionApplication> GetMissionApplicationAdmin(string searchText, int pageNumber, int pageSize);
        public MissionApplication GetApplicationForApprove(long missionAppId);
        public void ApproveApplication(MissionApplication application);
        public void DeleteApplication(MissionApplication application);
        //========================
        //Admin mission theme crud
        //==========================
        public AdminPageList<MissionTheme> GetMissionThemeAdmin(string searchText, int pageNumber, int pageSize);
        public void AddThemeAdmin(MissionTheme theme);
        public MissionTheme GetThemeAdmin(long themeId);
        public void EditThemeAdmin(MissionTheme theme);
        public void DeleteThemeAdmin(MissionTheme theme);
        //========================
        //Admin skill crud
        //==========================
        public AdminPageList<Skill> GetSkillAdmin(string searchText, int pageNumber, int pageSize);
        public void AddSkillAdmin(Skill skill);
        public Skill GetSkill(long skillId);
        public void EditSkill(Skill skill);
        public void DeleteSkill(Skill skill);
    }
}
