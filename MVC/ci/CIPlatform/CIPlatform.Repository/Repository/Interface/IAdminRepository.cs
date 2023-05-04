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

        public User findadmin(string adminemail);
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
        public void ApproveStory(Story story, long fromuserid);
        public void DeleteStory(Story story, long fromuserid);
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
        //========================
        //Banner crud
        //==========================
        public AdminPageList<Banner> GetBanner(string searchText, int pageNumber, int pageSize);
        public void AddBannerAdmin(Banner banner1);
        public Banner GetBanner(long bannerId);
        public void EditBannerAdmin(Banner banner1);
        public void DeleteBannerAdmin(Banner banner1);
        //========================
        //Mission crud
        //==========================
        public AdminPageList<Mission> GetMissionAdmin(string searchText, int pageNumber, int pageSize);
        public Mission GetMission(long missionId);
        public void DeleteMission(Mission mission);
        public long AddMission(Mission mission);
        public IEnumerable<MissionTheme> GetMissionTheme();
        public int GetSkillvianame(string skill);
        public void AddMissionSkill(MissionSkill mSkill);
        public void AddGoalMission(GoalMission goalMission);
        public void AddMissionMedia(MissionMedium media);
        public void AddMissionDocument(MissionDocument missionDocument);
        public void EditMission(Mission mission);
        public GoalMission GetGoalMission(long goalId);
        public MissionMedium GetMissionMedium(long missionId);
        public void EditMissionSkill(long skillId, long missionId,MissionSkill skill);
        public void UpdateMissionMedia(MissionMedium media);
        public MissionDocument GetMissionDocument(long missionId);
        public void UpdateMissionDocument(MissionDocument missionDocument);
        public List<MissionSkill> missionSkills(long missionId);
        public Skill GetSkillName(long skillId);
        public void RemoveMissionSkill(long missionid);
        public void GiveNotificationToUser(Notification notification);
    }
}
