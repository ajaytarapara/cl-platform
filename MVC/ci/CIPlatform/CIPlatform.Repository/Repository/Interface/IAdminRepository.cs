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
        //========================
        //Admin login
        //==========================
        public Boolean validateadmin(string adminemail);

        public Boolean validateadmincred(string adminemail,string adminpassword);

        public Admin findadmin(string adminemail);
        //========================
        //Admin user crud
        //==========================
        public List<User> GetUsers(string searchtext);

        public void AddUserAdmin(User user);

        public void RemoveUserAdmin(long userid);
        public User UpdateUserAdminget(long userid);

        public void UpdateneedUser(User useredit);
        //========================
        //Admin cms crud
        //==========================
        public List<CmsPage> GetCmspages(string searchText);
        public void AddCmsAdmin(CmsPage CMS);
        public void UpdateCmsAdmin(CmsPage cms);
        public CmsPage GetCmsAdmin(long cmsId);
        public void DeleteCmsAdmin(long cmsId);
        //========================
        //Admin story crud
        //==========================
        public List<Story> GetStoryAdmin(string searchText);
        public Story GetstoryForApprove(long storyId);
        public void ApproveStory(Story story);
        public void DeleteStory(Story story);
        //========================
        //Admin Application crud
        //==========================
        public List<MissionApplication> GetMissionApplicationAdmin(string searchText);
        public MissionApplication GetApplicationForApprove(long missionAppId);
        public void ApproveApplication(MissionApplication application);
        public void DeleteApplication(MissionApplication application);
    }
}
